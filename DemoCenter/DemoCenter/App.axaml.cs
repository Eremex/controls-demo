using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using System.Globalization;
using System.Reflection;
using DemoCenter.ViewModels;
using DemoCenter.Views;
using DemoCenter.ProductsData;
using Eremex.AvaloniaUI.Controls.Common;

namespace DemoCenter;

public class App : Application
{
    static string[] embeddedResources;

    public static bool IsWebApp { get; private set; }
    public static VersionInfo Version { get; }
    public static string[] EmbeddedResources => embeddedResources ??= Assembly.GetAssembly(typeof(App)).GetManifestResourceNames();

    static App()
    {
        SetCultureInfo();
        Version = new VersionInfo(Assembly.GetAssembly(typeof(MxWindow)));
    }
    void SetPaletteStyle(IStyle oldStyle, IStyle newStyle)
    {
        if (oldStyle != null && newStyle != null && !Styles.Contains(newStyle))
        {
            if (Styles.Contains(oldStyle))
                Styles[Styles.IndexOf(oldStyle)] = newStyle;
            else
                Styles.Add(newStyle);
        }
    }
    static void SetCultureInfo()
    {
        var cultureInfo = CultureInfo.GetCultureInfo("en-US");
        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
    }
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(RequestedThemeVariant)
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            IsWebApp = true;
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel(RequestedThemeVariant)
            };
        }
        
        base.OnFrameworkInitializationCompleted();
    }
}
