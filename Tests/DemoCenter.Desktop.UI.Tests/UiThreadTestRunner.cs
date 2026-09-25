using Avalonia.Threading;

using Xunit.v3;

namespace DemoCenter.Desktop.UI.Tests;

/// <summary>
/// Runs every test - constructor, body and disposal - on the Avalonia UI thread.
/// </summary>
/// <remarks>
/// This is the xunit v3 replacement for the v2 <c>XunitTestAssemblyRunner.SetupSyncContext</c>
/// override: v3 installs its own synchronization context, so an ambient one is not enough and the
/// hop has to be explicit. <see cref="XunitTestRunner.Instance"/> is a writable static field, so
/// replacing it once covers every plain <c>[Fact]</c> and <c>[Theory]</c> in the assembly.
/// </remarks>
public sealed class UiThreadTestRunner : XunitTestRunner
{
	protected override ValueTask<TimeSpan> RunTest(XunitTestRunnerContext ctxt)
	{
		if (Dispatcher.UIThread.CheckAccess())
			return base.RunTest(ctxt);

		return new ValueTask<TimeSpan>(Dispatcher.UIThread.InvokeAsync(() => base.RunTest(ctxt).AsTask()));
	}
}
