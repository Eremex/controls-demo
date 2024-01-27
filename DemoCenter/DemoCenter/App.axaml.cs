using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using Avalonia.Threading;
using DemoCenter.ViewModels;
using DemoCenter.Views;
using Eremex.AvaloniaUI.Themes;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using Avalonia.Controls;

namespace DemoCenter;

public partial class App : Application
{
    private IStyle lightTheme;
    private IStyle darkTheme;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        lightTheme = (IStyle)Resources["EremexLightTheme"];
        darkTheme = (IStyle)Resources["EremexDarkTheme"];

        UpdatePalette(PaletteType.White);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    public void UpdatePalette(PaletteType newPalette)
    {
        if (newPalette == PaletteType.White) 
        { 
            RequestedThemeVariant = ThemeVariant.Light;
            SetPaletteStyle(darkTheme, lightTheme);
        }
        else if(newPalette == PaletteType.Black)
        {
            RequestedThemeVariant = ThemeVariant.Dark;
            SetPaletteStyle(lightTheme, darkTheme);
        }            
    }

    private static string[] embeddedResources;

    public static string[] EmbeddedResources => embeddedResources ??= Assembly.GetAssembly(typeof(App)).GetManifestResourceNames();

    private void SetPaletteStyle(IStyle oldStyle, IStyle newStyle)
    {
        if (oldStyle != null && newStyle != null && !Styles.Contains(newStyle))
        {
            if (Styles.Contains(oldStyle))
                Styles[Styles.IndexOf(oldStyle)] = newStyle;
            else
                Styles.Add(newStyle);
        }
    }

    static App()
    {
        SetCultureInfo();
    }
    private static void SetCultureInfo()
    {
        var cultureInfo = CultureInfo.GetCultureInfo("en-US");
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
        Thread.CurrentThread.CurrentCulture = cultureInfo;
    }
}