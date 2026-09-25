using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

using Microsoft.Playwright;

namespace DemoCenter.Wasm.TestRunner;

/// <summary>
/// The external driver of the wasm run: serves the published static files, drives a
/// headless browser, collects the JUnit report and returns an exit code based on the number
/// of failed tests.
/// </summary>
internal static class Program
{
	/// <summary>The single test assembly of the demo.</summary>
	private const string TestAssemblyName = "DemoCenter.Wasm.Tests";

	private static readonly TimeSpan DefaultTimeout = TimeSpan.FromMinutes(10);

	private static async Task<int> Main(string[] args)
	{
		var options = RunnerOptions.Parse(args);
		if (!Directory.Exists(options.WwwRoot))
		{
			Console.Error.WriteLine($"Directory not found: {options.WwwRoot}");
			Console.Error.WriteLine("Run this first: dotnet publish Tests.Wasm/DemoCenter.Wasm.TestHost");
			return 2;
		}

		await using var server = StartServer(options);
		await server.StartAsync();
		var address = $"http://127.0.0.1:{options.Port}/";
		Console.WriteLine($"Static:   {options.WwwRoot}");
		Console.WriteLine($"Address:  {address}");

		try
		{
			return await RunInBrowser(address, options);
		}
		finally
		{
			await server.StopAsync();
		}
	}

	private static WebApplication StartServer(RunnerOptions options)
	{
		var builder = WebApplication.CreateBuilder();
		builder.Logging.ClearProviders();
		builder.WebHost.UseUrls($"http://127.0.0.1:{options.Port}");

		var application = builder.Build();

		// The browser will not accept wasm without the right MIME type, and the .NET runtime
		// serves .dat/.blat as binary blobs.
		var contentTypeProvider = new FileExtensionContentTypeProvider();
		contentTypeProvider.Mappings[".wasm"] = "application/wasm";
		contentTypeProvider.Mappings[".dat"] = "application/octet-stream";
		contentTypeProvider.Mappings[".blat"] = "application/octet-stream";
		contentTypeProvider.Mappings[".pdb"] = "application/octet-stream";

		var fileOptions = new StaticFileOptions
		{
			FileProvider = new PhysicalFileProvider(Path.GetFullPath(options.WwwRoot)),
			ContentTypeProvider = contentTypeProvider,
			ServeUnknownFileTypes = true,
			OnPrepareResponse = context =>
			{
				// Headers for multi-threaded wasm: needed if WasmEnableThreads ever has to be
				// turned on. Harmless for a single-threaded build.
				context.Context.Response.Headers["Cross-Origin-Opener-Policy"] = "same-origin";
				context.Context.Response.Headers["Cross-Origin-Embedder-Policy"] = "require-corp";
				context.Context.Response.Headers["Cache-Control"] = "no-store";
			},
		};

		application.UseDefaultFiles(new DefaultFilesOptions { FileProvider = fileOptions.FileProvider });
		application.UseStaticFiles(fileOptions);
		return application;
	}

	/// <summary>
	/// Runs the demo test assembly in a single page load.
	/// </summary>
	/// <remarks>
	/// Unlike the controls run, where there are eight assemblies and each needs its own page
	/// (one runtime is not enough for all of them - Mono aborts in <c>gmem.c</c>), here
	/// there is a single assembly and nothing to iterate over.
	/// </remarks>
	private static async Task<int> RunInBrowser(string address, RunnerOptions options)
	{
		using var playwright = await Playwright.CreateAsync();
		var launchOptions = new BrowserTypeLaunchOptions { Headless = options.Headless };

		// The browser comes from the chromium.<rid> nuget package (see the csproj), not from
		// Playwright's own CDN download on every CI run. Chromium.Path locates the native
		// binary for the current platform among the referenced runtime packages.
		if (Chromium.Path is { } bundledChromePath)
		{
			Console.WriteLine($"Chromium: {bundledChromePath}");
			launchOptions.ExecutablePath = bundledChromePath;
		}
		else
		{
			Console.WriteLine("Chromium: bundled binary not found for this runtime, falling back to Playwright's own install");
		}

		await using var browser = await playwright.Chromium.LaunchAsync(launchOptions);

		var stopwatch = Stopwatch.StartNew();

		var run = await RunPage(browser, address, options.Assembly ?? TestAssemblyName, options);
		if (run.Outcome != PageOutcome.Completed)
		{
			return (int)run.Outcome;
		}

		WriteReport([.. ParseSuites(run.Xml)], options);

		Console.WriteLine();
		Console.WriteLine(string.Format(
			CultureInfo.InvariantCulture, @"Done in {0:mm\:ss}, failed: {1}", stopwatch.Elapsed, run.Failed));

		return run.Failed == 0 ? 0 : Math.Clamp(run.Failed, 1, 252);
	}

	private static async Task<PageRun> RunPage(IBrowser browser, string address, string assembly, RunnerOptions options)
	{
		// A page of its own per assembly: closing it releases the runtime and all its memory.
		var page = await browser.NewPageAsync();

		try
		{
			// A dead runtime is a separate case: the page outlives it, the Avalonia timer
			// keeps ticking and flooding the console, but no result will ever arrive.
			// Without this signal the run simply hung until the timeout.
			var runtimeDied = new TaskCompletionSource<string>();

			page.Console += (_, message) =>
			{
				var text = message.Text;

				if (IsRuntimeDeath(text))
				{
					runtimeDied.TrySetResult(text);
					return;
				}

				if (!IsRuntimeExitNoise(text))
				{
					Console.WriteLine(text);
				}
			};
			page.PageError += (_, error) => Console.Error.WriteLine("[page-error] " + error);

			var url = address + "?assembly=" + Uri.EscapeDataString(assembly);
			if (options.Skip > 0 || options.Take >= 0)
			{
				url += $"&skip={options.Skip}&take={options.Take}";
			}

			await page.GotoAsync(url, new PageGotoOptions { Timeout = (float)options.Timeout.TotalMilliseconds });

			var completion = page.WaitForFunctionAsync(
				"() => typeof globalThis.__emxTestFailed === 'number'",
				null,
				new PageWaitForFunctionOptions { Timeout = (float)options.Timeout.TotalMilliseconds });

			if (await Task.WhenAny(completion, runtimeDied.Task) == runtimeDied.Task)
			{
				Console.Error.WriteLine("The .NET runtime died mid-run, the results are incomplete:");
				Console.Error.WriteLine("  " + runtimeDied.Task.Result);
				Console.Error.WriteLine("A Mono abort in gmem.c means the wasm heap ran out.");
				return new PageRun(PageOutcome.RuntimeDied, -1, string.Empty, string.Empty);
			}

			try
			{
				await completion;
			}
			catch (TimeoutException)
			{
				Console.Error.WriteLine($"The run did not finish within {options.Timeout}. Possible deadlock: in single-threaded wasm any .Result / .Wait() inside a test blocks the only thread.");
				return new PageRun(PageOutcome.TimedOut, -1, string.Empty, string.Empty);
			}

			var failed = await page.EvaluateAsync<int>("() => globalThis.__emxTestFailed");
			var xml = await page.EvaluateAsync<string>("() => globalThis.__emxTestResults ?? ''");
			var names = await page.EvaluateAsync<string>("() => globalThis.__emxAssemblies ?? ''");
			var hostError = await page.EvaluateAsync<string?>("() => globalThis.__emxHostError ?? null");

			if (hostError is not null)
			{
				Console.Error.WriteLine("The host crashed - this is not failing tests, the run itself did not work:");
				Console.Error.WriteLine(hostError);
				return new PageRun(PageOutcome.HostCrashed, -1, string.Empty, string.Empty);
			}

			return new PageRun(PageOutcome.Completed, failed, xml, names);
		}
		finally
		{
			await page.CloseAsync();
		}
	}

	/// <summary>Extracts the testsuite elements from one assembly report.</summary>
	private static IEnumerable<XElement> ParseSuites(string xml)
	{
		if (string.IsNullOrWhiteSpace(xml))
		{
			return [];
		}

		try
		{
			return XDocument.Parse(xml).Root?.Elements("testsuite").ToList() ?? [];
		}
		catch (System.Xml.XmlException exception)
		{
			Console.Error.WriteLine("Could not parse the report of this assembly: " + exception.Message);
			return [];
		}
	}

	/// <summary>Merges the reports of all assemblies into a single JUnit document.</summary>
	private static void WriteReport(IReadOnlyCollection<XElement> suites, RunnerOptions options)
	{
		if (suites.Count == 0)
		{
			return;
		}

		Directory.CreateDirectory(options.ResultsDirectory);
		var reportPath = Path.Combine(options.ResultsDirectory, "DemoCenter.Wasm-test-result.xml");
		var document = new XDocument(new XElement("testsuites", suites));

		using var writer = new StreamWriter(reportPath, append: false, new System.Text.UTF8Encoding(false));
		writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");

		// OmitXmlDeclaration is required: XDocument.Save into a TextWriter writes a prolog of
		// its own, and the file ended up with two XML declarations - such a report does not
		// parse at all.
		using var xmlWriter = System.Xml.XmlWriter.Create(
			writer,
			new System.Xml.XmlWriterSettings { Indent = true, OmitXmlDeclaration = true });

		document.Save(xmlWriter);

		Console.WriteLine($"Report:   {reportPath}");
	}

	/// <summary>
	/// Exit codes for the special outcomes.
	/// </summary>
	/// <remarks>
	/// Taken from the top of the range on purpose: the ordinary exit code is the number of
	/// failed tests, and low values collide with it. A run with four failures returned 4 and
	/// looked like "the runtime died".
	/// </remarks>
	private enum PageOutcome
	{
		Completed = 0,
		TimedOut = 253,
		RuntimeDied = 254,
		HostCrashed = 255,
	}

	private sealed record PageRun(PageOutcome Outcome, int Failed, string Xml, string Assemblies);

	/// <summary>
	/// Filters out the runtime noise produced by an attempt to exit the process.
	/// </summary>
	/// <remarks>
	/// The Eremex.Avalonia.Controls.UI.Tests tests call application shutdown at the end
	/// (Shutdown / Environment.Exit). There is nowhere to exit to in a browser, and dotnet
	/// prints a warning for every such call together with a long JS stack over wasm
	/// functions. In one run that produced about 95,000 lines against a hundred and fifty
	/// real failures - nothing can be found in a log like that.
	/// </remarks>
	/// <summary>
	/// The signs that the .NET runtime has died and no result will arrive.
	/// </summary>
	/// <remarks>
	/// Observed on a full run: Mono aborts in <c>gmem.c</c>, which means the wasm heap ran
	/// out of memory. The page stays alive through it, so waiting any longer is pointless.
	/// </remarks>
	private static bool IsRuntimeDeath(string text) =>
		text.Contains("runtime already exited", StringComparison.Ordinal)
		|| text.Contains("mono/eglib/gmem.c", StringComparison.Ordinal);

	private static bool IsRuntimeExitNoise(string text) =>
		text.Contains("doesn't exit the runtime", StringComparison.Ordinal)
		|| text.Contains("/_framework/dotnet.native.", StringComparison.Ordinal)
		|| text.Contains("/_framework/dotnet.runtime.", StringComparison.Ordinal);

	private sealed record RunnerOptions(
		string WwwRoot,
		int Port,
		bool Headless,
		TimeSpan Timeout,
		string ResultsDirectory,
		string? Assembly,
		int Skip,
		int Take)
	{
		public static RunnerOptions Parse(string[] args)
		{
			var wwwRoot = GetValue(args, "--wwwroot") ?? DefaultWwwRoot();
			var port = int.Parse(GetValue(args, "--port") ?? "5055", CultureInfo.InvariantCulture);
			var headless = GetValue(args, "--headed") is null;
			var timeout = TimeSpan.FromSeconds(
				double.Parse(GetValue(args, "--timeout-seconds") ?? DefaultTimeout.TotalSeconds.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture));
			var results = GetValue(args, "--results") ?? Path.Combine(Directory.GetCurrentDirectory(), "TestResults");

			// --assembly / --skip / --take: run one assembly, or a slice of it.
			var assembly = GetValue(args, "--assembly");
			assembly = string.IsNullOrEmpty(assembly) ? null : assembly;
			var skip = int.Parse(GetValue(args, "--skip") ?? "0", CultureInfo.InvariantCulture);
			var take = int.Parse(GetValue(args, "--take") ?? "-1", CultureInfo.InvariantCulture);

			return new RunnerOptions(wwwRoot, port, headless, timeout, results, assembly, skip, take);
		}

		private static string DefaultWwwRoot() =>
			Path.Combine(
				AppContext.BaseDirectory, "..", "..", "..", "..",
				"DemoCenter.Wasm.TestHost", "bin", "Release_WASM", "net10.0-browser", "publish", "wwwroot");

		private static string? GetValue(string[] args, string name)
		{
			var index = Array.IndexOf(args, name);
			if (index < 0)
			{
				return null;
			}

			return index + 1 < args.Length && !args[index + 1].StartsWith("--", StringComparison.Ordinal)
				? args[index + 1]
				: string.Empty;
		}
	}
}
