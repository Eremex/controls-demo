using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

using Avalonia;
using Avalonia.Browser;
using Avalonia.Themes.Fluent;

using Xunit.Runner.Common;
using Xunit.Runner.InProc.SystemConsole;
using Xunit.Sdk;
using Xunit.v3;

[assembly: SupportedOSPlatform("browser")]

namespace DemoCenter.Wasm.TestHost;

/// <summary>
/// Entry point of the browser run of the demo tests.
/// </summary>
/// <remarks>
/// <para>
/// Built the same way as Tests.Wasm in the controls repository, and for the same reasons:
/// Microsoft.Testing.Platform does not work in browser-wasm (it looks for a JSON config on
/// the file system next to the assembly, testfx#2196), and the stock
/// <c>ProjectAssemblyRunner.Run</c> internally spins up an <c>InProcessFrontController</c>,
/// which derives the assembly name from <c>Assembly.Location</c> - empty under wasm. So the
/// run goes straight through <see cref="ITestFramework"/>, discoverer and executor.
/// </para>
/// <para>
/// The difference from controls: what is started here by the browser backend is the real
/// demo application (<c>DemoCenter.App</c>), not a test one. It is what creates the
/// <c>MainView</c> and the <c>MainViewModel</c> the tests then walk over.
/// </para>
/// </remarks>
public static partial class Program
{
	/// <summary>The markup element the demo is mounted into.</summary>
	private const string HostElementId = "out";

	private static string resultsXml = string.Empty;
	private static int failedCount = -1;

	public static async Task Main(string[] args)
	{
		_ = args;

		var testAssembly = typeof(global::DemoCenter.Wasm.Tests.DemoModulesTests).Assembly;

		Console.WriteLine("[emx] browser-wasm demo test host");
		Console.WriteLine($"[emx] runtime: {Environment.Version}, culture: {System.Globalization.CultureInfo.CurrentCulture.Name}");
		Console.WriteLine("[emx] starting DemoCenter (browser backend)...");

		await StartDemoAsync();

		var lifetimeName = Application.Current?.ApplicationLifetime?.GetType().Name;
		Console.WriteLine("[emx] lifetime: " + (lifetimeName ?? "<none>"));

		var report = new JUnitReportBuilder(testAssembly.GetName().Name ?? "DemoCenter.Wasm.Tests");

		try
		{
			await RunAssembly(testAssembly, report);
		}
		catch (Exception exception)
		{
			// From an async Main the exception reaches JS already wrapped in an
			// AggregateException with no usable stack, so the whole chain is printed here.
			Console.WriteLine("[emx] HOST CRASHED:");
			for (var current = exception; current is not null; current = current.InnerException)
			{
				Console.WriteLine($"[emx]   {current.GetType().FullName}: {current.Message}");
				Console.WriteLine(current.StackTrace);
			}

			throw;
		}

		resultsXml = JUnitReportBuilder.Combine([report]);
		failedCount = report.Failed;

		Console.WriteLine($"[emx] TOTAL: passed={report.Passed}, failed={report.Failed}, skipped={report.Skipped}");
	}

	/// <summary>
	/// Starts the demo on the real browser backend.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The application is composed word for word as in DemoCenter.Web/Program.cs, including
	/// WithInterFont and inserting FluentTheme as style zero. A divergence here would mean
	/// the stand checks a different application than the one that ships: the theme supplies
	/// the TopLevel template, and where popups are shown depends on it.
	/// </para>
	/// <para>
	/// Readiness is caught through <c>AfterSetup</c> rather than by awaiting
	/// <c>StartBrowserAppAsync</c> itself: that is the Task of a living application and
	/// cannot be expected to complete. Our handler is added last so that the theme is
	/// already in place by the time the signal fires.
	/// </para>
	/// </remarks>
	private static async Task StartDemoAsync()
	{
		var setupCompleted = new TaskCompletionSource();

		var builder = AppBuilder
			.Configure<global::DemoCenter.App>()
			.WithInterFont()
			.AfterSetup(x => x.Instance!.Styles.Insert(0, new FluentTheme()))
			.AfterSetup(_ => setupCompleted.TrySetResult());

		_ = builder.StartBrowserAppAsync(HostElementId);

		await setupCompleted.Task;
	}

	private static async Task RunAssembly(Assembly assembly, JUnitReportBuilder report)
	{
		var assemblyName = assembly.GetName().Name ?? "UnknownAssembly";
		Console.WriteLine($"[emx] === {assemblyName} ===");

		var project = new XunitProject();
		var metadata = new AssemblyMetadata(
			xunitVersion: 3,
			assembly.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName ?? ".NETCoreApp,Version=v10.0");

		var projectAssembly = new XunitProjectAssembly(project, assemblyName + ".dll", metadata)
		{
			Assembly = assembly,
		};
		project.Add(projectAssembly);

		var configuration = projectAssembly.Configuration;
		var diagnosticSink = new ConsoleDiagnosticMessageSink();
		using var cancellationTokenSource = new CancellationTokenSource();

		// Required before the framework is created: otherwise TestContext stays idle and the
		// very first SetForTestAssembly fails with "Cannot get KeyValueStorage on the idle
		// test context".
		Xunit.TestContext.SetForInitialization(
			diagnosticSink,
			configuration.DiagnosticMessagesOrDefault,
			configuration.InternalDiagnosticMessagesOrDefault);

		var testFramework = CreateTestFramework(assembly);
		var pipelineStartup = await ProjectAssemblyRunner.InvokePipelineStartup(assembly, diagnosticSink);
		if (pipelineStartup is not null)
			testFramework.SetTestPipelineStartup(pipelineStartup);

		try
		{
			var discoverer = testFramework.GetDiscoverer(assembly);
			var discoveryOptions = TestFrameworkOptions.ForDiscovery(configuration);
			var testCases = new List<ITestCase>();

			await discoverer.Find(
				testCase =>
				{
					testCases.Add(testCase);
					return new ValueTask<bool>(true);
				},
				discoveryOptions,
				types: null,
				cancellationTokenSource.Token);

			Console.WriteLine($"[emx] test cases to run: {testCases.Count}");

			var executor = testFramework.GetExecutor(assembly);
			var executionOptions = TestFrameworkOptions.ForExecution(configuration);

			// Required in the browser: without it xunit starts a separate thread to deliver
			// messages (MessageBus..ctor -> Thread.Start), and in single-threaded wasm that
			// is a PlatformNotSupportedException before the first test. With the flag set,
			// SynchronousMessageBus is used instead. In the controls run the same thing is
			// done by Eremex.XUnit.Execution.TestFrameworkExecutor.
			executionOptions.SetValue(TestOptionsNames.Execution.SynchronousMessageReporting, true);
			await executor.RunTestCases(testCases, report, executionOptions, cancellationTokenSource.Token);
		}
		finally
		{
			if (pipelineStartup is not null)
				await pipelineStartup.StopAsync();
		}
	}

	private static ITestFramework CreateTestFramework(Assembly assembly)
	{
		var attribute = assembly.GetCustomAttributes().OfType<ITestFrameworkAttribute>().FirstOrDefault();
		if (attribute is null)
			return new XunitTestFramework();

		return Activator.CreateInstance(attribute.FrameworkType) as ITestFramework
			?? throw new InvalidOperationException($"{attribute.FrameworkType.FullName} does not implement ITestFramework");
	}

	[JSExport]
	internal static string GetResultsXml() => resultsXml;

	/// <summary>The number of failed tests; -1 means the run did not reach the end.</summary>
	[JSExport]
	internal static int GetFailedCount() => failedCount;

	/// <summary>For compatibility with the driver, which can handle several assemblies.</summary>
	[JSExport]
	internal static string GetAssemblyNames() => "DemoCenter.Wasm.Tests";

	private sealed class ConsoleDiagnosticMessageSink : IMessageSink
	{
		public bool OnMessage(IMessageSinkMessage message)
		{
			if (message is IDiagnosticMessage diagnostic)
				Console.WriteLine($"[emx][diag] {diagnostic.Message}");
			else if (message is IErrorMessage error)
				Console.WriteLine($"[emx][error] {string.Join(" | ", error.Messages)}");

			return true;
		}
	}
}
