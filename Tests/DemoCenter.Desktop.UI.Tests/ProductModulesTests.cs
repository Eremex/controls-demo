using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Avalonia.VisualTree;

using DemoCenter.ProductsData;
using DemoCenter.ViewModels;
using DemoCenter.Views;

using Eremex.AvaloniaUI.Controls.DataControl;
using GroupBox = Eremex.AvaloniaUI.Controls.Editors.GroupBox;

namespace DemoCenter.Desktop.UI.Tests;

/// <summary>
/// The demo window shared by every test that opens demo modules.
/// </summary>
/// <remarks>
/// Started on first use rather than in the constructor: xunit builds a class fixture on the
/// runner thread, while <see cref="UiThreadTestRunner"/> marshals only the test itself onto the
/// Avalonia thread, and a window cannot be created anywhere else. macOS is the strictest about
/// this - AppKit will not make an NSWindow off the process main thread.
/// </remarks>
public sealed class DemoWindowFixture : IDisposable
{
    private MainWindow window;

    public MainViewModel ViewModel { get; private set; }

    /// <summary>Shows the demo window once for the whole class.</summary>
    public void EnsureStarted()
    {
        if (window != null)
            return;

        ViewModel = new MainViewModel();
        window = new MainWindow { DataContext = ViewModel };
        window.Show();
        window.WindowState = WindowState.Maximized;
        Settle();
    }

    /// <summary>Switches the demo to this module and drives the switch through to a layout.</summary>
    public void Open(ProductInfoBase module)
    {
        EnsureStarted();
        ViewModel.SelectProduct(module);
        Settle();
    }

    /// <summary>Runs the dispatcher queue and recomputes layout.</summary>
    public void Settle()
    {
        Dispatcher.UIThread.RunJobs();
        window?.UpdateLayout();
    }

    /// <summary>The whole visual tree below the window, depth first.</summary>
    public IEnumerable<Visual> Descendants() => Descendants(window);

    /// <summary>
    /// The settings panel every demo page shows on the right, or null when the page has none.
    /// </summary>
    /// <remarks>
    /// Found by the style class the pages put on it rather than by position: the layout of a page
    /// is its own business, the class is what all of them share.
    /// </remarks>
    public GroupBox FindSettingsPanel() =>
        Descendants().OfType<GroupBox>().FirstOrDefault(x => x.Classes.Contains("PropertiesGroup"));

    /// <summary>The data grid or tree list of the page, or null when the page has neither.</summary>
    public DataControlBase FindDataControl() => Descendants().OfType<DataControlBase>().FirstOrDefault();

    /// <summary>
    /// The scroll viewer of a data control.
    /// </summary>
    /// <remarks>
    /// The control exposes it as protected internal, which the demo assembly cannot see, so it is
    /// taken from the template by the name the control gives it.
    /// </remarks>
    public static ScrollViewer FindScrollViewer(DataControlBase dataControl) =>
        Descendants(dataControl).OfType<ScrollViewer>().FirstOrDefault(x => x.Name == "PART_ScrollViewer");

    public static IEnumerable<Visual> Descendants(Visual root)
    {
        if (root == null)
            yield break;

        foreach (var child in root.GetVisualChildren())
        {
            yield return child;

            foreach (var nested in Descendants(child))
                yield return nested;
        }
    }

    /// <summary>
    /// Where the page screenshots go. Under TestResults so CI collects them as artifacts.
    /// </summary>
    public static string ScreenshotDirectory { get; } =
        Path.Combine(Environment.CurrentDirectory, "TestResults", "Screenshots");

    /// <summary>
    /// Saves what the demo currently shows as a PNG under the module it belongs to.
    /// </summary>
    /// <remarks>
    /// A failing page reports a message; a page that merely looks wrong reports nothing, and the
    /// farm has no screen to look at. The screenshot is the only way to see what CI saw.
    /// </remarks>
    public void Capture(string productName, string moduleName, string shot = null)
    {
        var area = FindDemoArea();
        if (area == null)
            return;

        var size = new PixelSize((int)area.Bounds.Width, (int)area.Bounds.Height);
        if (size.Width <= 0 || size.Height <= 0)
            return;

        var directory = Path.Combine(ScreenshotDirectory, FileName(productName));
        var name = FileName(moduleName);

        // A shot of a particular setting goes into a folder of its own, so the module keeps one
        // picture of how it opens and the settings walk does not bury it.
        if (shot != null)
        {
            directory = Path.Combine(directory, name);
            name = FileName(shot);
        }

        Directory.CreateDirectory(directory);

        using var bitmap = new RenderTargetBitmap(size, new Vector(96, 96));
        bitmap.Render(area);

        bitmap.Save(Path.Combine(directory, name + ".png"), new PngBitmapEncoderOptions());
    }

    /// <summary>
    /// The part of the window the current demo page occupies, or null when there is no page.
    /// </summary>
    /// <remarks>
    /// Found by the view model it holds rather than by a name: the host is an anonymous
    /// ContentControl in MainView, and the demo is not going to grow a name just for the tests.
    /// Cropping to it keeps the products tree and the header out of every screenshot.
    /// </remarks>
    private Visual FindDemoArea()
    {
        var pageViewModel = ViewModel?.CurrentProductItemViewModel;
        if (window == null || pageViewModel == null)
            return null;

        return Descendants()
            .OfType<ContentControl>()
            .FirstOrDefault(x => ReferenceEquals(x.Content, pageViewModel));
    }

    private static string FileName(string name) =>
        string.Join("_", name.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));

    public void Dispose()
    {
        if (window == null)
            return;

        var toClose = window;
        window = null;

        // Disposal runs on the runner thread, and macOS will not close an NSWindow anywhere but
        // the main one. CheckAccess keeps this correct if xunit ever disposes on the UI thread.
        if (Dispatcher.UIThread.CheckAccess())
            toClose.Close();
        else
            Dispatcher.UIThread.Invoke(toClose.Close);
    }
}

/// <summary>
/// Opens every module of one demo product and checks that none of them fails.
/// </summary>
/// <remarks>
/// One test per product rather than one for everything: the previous single ShowAllModules ran
/// for seven and a half minutes reporting nothing, and a break in the first module hid every
/// module after it. Now the counter advances per product, a slow product is named, and the rest
/// are still checked.
/// </remarks>
public class ProductModulesTests : IClassFixture<DemoWindowFixture>
{
    private readonly DemoWindowFixture demo;

    public ProductModulesTests(DemoWindowFixture demo) => this.demo = demo;

    /// <summary>
    /// The demo products. Taken from the registry the application itself uses, so the list cannot
    /// drift away from the demo.
    /// </summary>
    public static IEnumerable<object[]> ProductNames() =>
        Products.GetOrCreate().Select(product => new object[] { product.Name });

    /// <summary>Every module of one product: the group itself and each of its pages.</summary>
    public static IEnumerable<ProductInfoBase> ModulesOf(string productName)
    {
        var product = Products.GetOrCreate().FirstOrDefault(x => x.Name == productName);
        Assert.True(product != null, $"There is no demo product named {productName}.");

        yield return product;

        if (product is GroupInfo group)
        {
            foreach (var page in group.Pages)
                yield return page;
        }
    }

    [Theory]
    [MemberData(nameof(ProductNames))]
    public void ProductModulesOpen(string productName)
    {
        foreach (var module in ModulesOf(productName))
        {
            using var errors = new AvaloniaErrorCollector();

            // The switch itself is the check: the view model is created, the ViewLocator finds
            // the view, bindings are applied, layout is computed. Anything that throws surfaces
            // here.
            demo.Open(module);

            // A group is a tree node without a page of its own - OnCurrentProductItemChanged
            // returns immediately for it, so there is nothing to check beyond the absence of a
            // failure.
            if (module is not GroupInfo)
            {
                var pageViewModel = demo.ViewModel.CurrentProductItemViewModel;
                Assert.True(pageViewModel != null, $"{module.Name}: no view model was created.");

                // The view model alone proves nothing - it is a plain assignment. The real check
                // is that the ViewLocator found the view and built it into the tree; without it
                // the test would pass on a page that shows nothing but blank space.
                var expectedViewName = pageViewModel.GetType().Name.Replace("ViewModel", "View", StringComparison.Ordinal);
                Assert.True(
                    demo.Descendants().Any(x => x.GetType().Name == expectedViewName),
                    $"{module.Name}: {expectedViewName} was not built into the visual tree.");
            }

            demo.Capture(productName, module.Name);

            errors.AssertQuiet(module.Name);
        }
    }
}
