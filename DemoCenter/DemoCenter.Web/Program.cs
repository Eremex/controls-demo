using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;
using Avalonia.Themes.Fluent;
using DemoCenter;

[assembly: SupportedOSPlatform("browser")]

internal partial class Program {

	private static Task Main(string[] args)
		=> BuildAvaloniaApp()
			.WithInterFont()
            .WithFluent()
            .StartBrowserAppAsync("out");

	public static AppBuilder BuildAvaloniaApp()
		=> AppBuilder.Configure<App>();

}

internal static class AppBuilderExtension
{
    public static AppBuilder WithFluent(this AppBuilder appBuilder) 
        => appBuilder.AfterSetup(builder => builder.Instance.Styles.Insert(0, new FluentTheme()));
}