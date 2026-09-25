// SelfRegisteredExtensions is generated into the project RootNamespace, which this
// project sets to Eremex.Avalonia.Controls.UITests.
namespace DemoCenter.Desktop.UI.Tests;

public static class Program
{
	public static int Main(string[] args) =>
		UiTestHost.Run(args, AvaloniaApp.BuildAvaloniaApp, global::Eremex.Avalonia.Controls.UITests.SelfRegisteredExtensions.AddSelfRegisteredExtensions);
}
