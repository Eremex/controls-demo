using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using DemoCenter.ProductsData;
using DemoCenter.ViewModels;
using DemoCenter.Views;
using Eremex.AvaloniaUI.Controls.ApplicationServices;
using Eremex.AvaloniaUI.Controls.Common;
using Eremex.AvaloniaUI.Themes.DeltaDesign;
using System.Globalization;
using System.Reflection;

namespace DemoCenter;

public class App : Application
{
    static string[] embeddedResources;

    ResourceDictionary densityResources;

    public static bool IsWebApp { get; private set; }
    public static VersionInfo Version { get; }
    public static string[] EmbeddedResources => embeddedResources ??= Assembly.GetAssembly(typeof(App)).GetManifestResourceNames();

    static App()
    {
        SetCultureInfo();
        Version = new VersionInfo(Assembly.GetAssembly(typeof(MxWindow)));
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
        LoadDensityResources();
        AddGraphics3DStyles();
    }

    /// <summary>
    /// Adds the theme and styles of the 3D controls.
    /// </summary>
    /// <remarks>
    /// Done in code rather than in App.axaml because XAML has no conditional compilation:
    /// a single xmlns pointing at Eremex.Avalonia.Controls3D made that assembly mandatory
    /// for every head, including the browser one, where 3D is not shown anyway. Going
    /// through code lets the web build drop it entirely - together with Silk.NET.Vulkan,
    /// SharpDX and Assimp.
    /// </remarks>
    static void AddGraphics3DStyles()
    {
#if !DEMO_WEB
        Current!.Styles.Add(new Eremex.AvaloniaUI.Themes.Controls3D.Controls3DTheme());
        Current.Styles.Add(new Avalonia.Markup.Xaml.Styling.StyleInclude(new Uri("avares://DemoCenter/Resources/"))
        {
            Source = new Uri("avares://DemoCenter/Resources/Graphics3DStyles.axaml"),
        });
#endif
    }
    /// <summary>
    /// Registers the application services used by the Application Services demo pages.
    /// </summary>
    /// <remarks>
    /// The library only describes the service composition; the host chooses the container.
    /// Here the built-in <see cref="SimpleServiceProvider"/> is used so the demo does not
    /// depend on any particular DI container. Nothing is instantiated until first use,
    /// so the call is safe on platforms without desktop windows.
    /// </remarks>
    static void RegisterApplicationServices()
    {
        var serviceProvider = new SimpleServiceProvider();
        ApplicationServicesContext.RegisterApplicationServices(serviceProvider.AddSingleton);
        ApplicationServicesContext.SetCurrent(serviceProvider);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        RegisterApplicationServices();

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

    void DeltaDesignTheme_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
    {
        LoadDensityResources();
    }

    void LoadDensityResources()
    {
        var deltaDesignTheme = Styles.OfType<DeltaDesignTheme>().FirstOrDefault();
        if (deltaDesignTheme == null)
            return;
        int index = Resources.MergedDictionaries.IndexOf(densityResources);
        var uri = new Uri(string.Format("avares://DemoCenter/Resources/DensityResources/{0}.axaml", deltaDesignTheme.Density));
        densityResources = (ResourceDictionary)AvaloniaXamlLoader.Load(uri);
        if (index == -1)
            Resources.MergedDictionaries.Insert(0, densityResources);
        else
            Resources.MergedDictionaries[index] = densityResources;
    }
}
