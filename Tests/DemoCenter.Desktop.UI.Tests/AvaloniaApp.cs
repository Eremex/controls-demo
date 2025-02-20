using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Headless;
using Avalonia.Threading;

using DemoCenter.Desktop.UI.Tests;

namespace DemoCenter.Desktop.UI.Tests
{
	public static class AvaloniaApp
	{
		public static void Stop()
		{
			var app = GetApp();
			if (app is IDisposable disposable)
			{
				Dispatcher.UIThread.Post(disposable.Dispose);
			}

			if (app != null)
				Dispatcher.UIThread.Post(() => app.Shutdown());
		}

		public static Window? GetMainWindow() => GetApp()?.MainWindow;

		public static IClassicDesktopStyleApplicationLifetime? GetApp() =>
			(IClassicDesktopStyleApplicationLifetime?)Application.Current?.ApplicationLifetime;

		public static AppBuilder BuildAvaloniaApp() =>
			AppBuilder
				.Configure<App>()
				.UsePlatformDetect()
				.WithInterFont();
	}
}
