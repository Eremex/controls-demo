using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;

using Microsoft.Testing.Platform.Builder;

using Xunit.Runner.InProc.SystemConsole;
using Xunit.v3;

namespace DemoCenter.Desktop.UI.Tests;

/// <summary>
/// Entry point for the UI test assembly.
/// </summary>
/// <remarks>
/// Avalonia is set up on the process main thread and the dispatcher loop runs there, because
/// AppKit refuses to create an NSWindow anywhere else. The xunit runner is pushed onto a worker
/// thread and marshals each test back through <see cref="UiThreadTestRunner"/>. The same code
/// path is used on Windows, Linux and macOS.
/// </remarks>
public static class UiTestHost
{
	public static int Run(
		string[] args,
		Func<AppBuilder> buildAvaloniaApp,
		Action<ITestApplicationBuilder, string[]> registerExtensions)
	{
		XunitTestRunner.Instance = new UiThreadTestRunner();

		var lifetime = new ClassicDesktopStyleApplicationLifetime
		{
			ShutdownMode = ShutdownMode.OnExplicitShutdown
		};

		buildAvaloniaApp().SetupWithLifetime(lifetime);

		// An exception from a render or layout callback does not belong to whichever test happens
		// to be running, and letting one reach the runtime tears down the whole process.
		Dispatcher.UIThread.UnhandledException += (_, e) =>
		{
			Console.Error.WriteLine("[UiTestHost] unhandled dispatcher exception: " + e.Exception);
			e.Handled = true;
		};

		var runFinished = new CancellationTokenSource();
		int exitCode = 1;

		var runner = new Thread(() =>
		{
			try
			{
				exitCode = RunTests(args, registerExtensions);
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(e);
				exitCode = 1;
			}
			finally
			{
				runFinished.Cancel();
			}
		})
		{
			IsBackground = true,
			Name = "xunit runner"
		};

		runner.Start();

		Dispatcher.UIThread.MainLoop(runFinished.Token);

		return exitCode;
	}

	private static int RunTests(string[] args, Action<ITestApplicationBuilder, string[]> registerExtensions) =>
		args.Any(arg => arg is "-automated" or "@@")
			? ConsoleRunner.Run(args).GetAwaiter().GetResult()
			: Xunit.MicrosoftTestingPlatform.TestPlatformTestFramework
				.RunAsync(args, registerExtensions)
				.GetAwaiter().GetResult();
}
