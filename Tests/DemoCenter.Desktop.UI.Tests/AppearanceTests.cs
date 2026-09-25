using DemoCenter.ViewModels;

using Eremex.AvaloniaUI.Controls.ApplicationServices;

namespace DemoCenter.Desktop.UI.Tests;

/// <summary>
/// Walks every theme against every palette on the page the demo opens with.
/// </summary>
/// <remarks>
/// Recolouring the running application re-evaluates every dynamic resource in the tree, and a key
/// that one palette defines and another does not shows up nowhere but here. One page is enough:
/// the failure is in the theme, not in the page, and covering the combinations matters more than
/// covering the pages.
/// </remarks>
public class AppearanceTests : IClassFixture<DemoWindowFixture>
{
    private readonly DemoWindowFixture demo;

    public AppearanceTests(DemoWindowFixture demo) => this.demo = demo;

    [Fact]
    public void EveryThemeWorksWithEveryPalette()
    {
        demo.EnsureStarted();

        var appearance = demo.ViewModel.Appearance;
        Assert.True(appearance.Themes.Count > 0, "The appearance service offers no themes.");

        var originalTheme = appearance.SelectedTheme;
        var originalPalette = appearance.SelectedPalette;

        // A theme without palettes still has to be visited, so an empty list becomes one null entry.
        var palettes = appearance.Palettes.Count > 0
            ? appearance.Palettes.ToList()
            : new List<IAppearanceOption> { null };

        try
        {
            foreach (var theme in appearance.Themes)
            {
                foreach (var palette in palettes)
                {
                    using var errors = new AvaloniaErrorCollector();

                    appearance.SelectedTheme = theme;
                    appearance.SelectedPalette = palette;
                    demo.Settle();

                    Assert.Same(theme, appearance.SelectedTheme);
                    if (palette != null)
                        Assert.Same(palette, appearance.SelectedPalette);

                    errors.AssertQuiet($"theme '{theme.Header}' with palette '{palette?.Header ?? "none"}'");
                }
            }
        }
        finally
        {
            appearance.SelectedTheme = originalTheme;
            appearance.SelectedPalette = originalPalette;
            demo.Settle();
        }
    }
}
