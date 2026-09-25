using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

using Avalonia;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.VisualTree;

using AvaloniaEdit;

using Eremex.AvaloniaUI.Controls.ApplicationServices;

namespace DemoCenter.Desktop.UI.Tests;

public partial class CodeReadabilityTests : IClassFixture<DemoWindowFixture>
{
    // WCAG AA for body text. Syntax colours are read the same way prose is.
    private const double MinimumContrast = 4.5;

    private readonly DemoWindowFixture demo;

    public CodeReadabilityTests(DemoWindowFixture demo) => this.demo = demo;

    [Fact]
    public void SyntaxColoursStayLegibleInEveryThemeAndPalette()
    {
        demo.EnsureStarted();
        ShowCodeTab();

        var editor = demo.Descendants().OfType<TextEditor>().FirstOrDefault();
        Assert.True(editor != null, "The code view is not in the visual tree.");

        var appearance = demo.ViewModel.Appearance;
        var palettes = appearance.Palettes.Count > 0
            ? appearance.Palettes.ToList()
            : new List<IAppearanceOption> { null };

        var originalTheme = appearance.SelectedTheme;
        var originalPalette = appearance.SelectedPalette;
        var failures = new List<string>();

        try
        {
            foreach (var theme in appearance.Themes)
            {
                foreach (var palette in palettes)
                {
                    appearance.SelectedTheme = theme;
                    appearance.SelectedPalette = palette;
                    demo.Settle();

                    var background = ResolveBackground(editor);
                    if (background == null)
                        continue;

                    var variant = editor.ActualThemeVariant == ThemeVariant.Dark ? "Dark" : "Light";

                    foreach (var (file, name, colour) in HighlightColours(variant))
                    {
                        var contrast = Contrast(colour, background.Value);
                        if (contrast < MinimumContrast)
                        {
                            failures.Add(
                                $"{theme.Header}/{palette?.Header ?? "none"}: {file} '{name}' " +
                                $"{ToHex(colour)} on {ToHex(background.Value)} is {contrast:F2}:1");
                        }
                    }
                }
            }
        }
        finally
        {
            appearance.SelectedTheme = originalTheme;
            appearance.SelectedPalette = originalPalette;
            demo.ViewModel.IsDemoSelected = true;
            demo.Settle();
        }

        Assert.True(
            failures.Count == 0,
            $"{failures.Count} syntax colour(s) fall below {MinimumContrast}:1:"
                + Environment.NewLine + string.Join(Environment.NewLine, failures.Distinct().Order()));
    }

    private void ShowCodeTab()
    {
        demo.ViewModel.IsDemoSelected = false;
        demo.Settle();

        var tabs = demo.Descendants().OfType<Avalonia.Controls.Primitives.SelectingItemsControl>()
            .FirstOrDefault(x => x.GetType().Name == "MxTabControl");

        if (tabs != null && tabs.ItemCount > 1)
        {
            tabs.SelectedIndex = 1;
            demo.Settle();
        }

        demo.Settle();
    }

    private static Color? ResolveBackground(Visual editor)
    {
        for (Visual visual = editor; visual != null; visual = visual.GetVisualParent())
        {
            var brush = visual switch
            {
                TextEditor text => text.Background,
                Avalonia.Controls.Border border => border.Background,
                Avalonia.Controls.Panel panel => panel.Background,
                Avalonia.Controls.Primitives.TemplatedControl control => control.Background,
                _ => null,
            };

            if (brush is ISolidColorBrush solid && solid.Color.A > 0)
                return solid.Color;
        }

        return null;
    }

    private static IEnumerable<(string File, string Name, Color Colour)> HighlightColours(string variant)
    {
        foreach (var language in new[] { "CSharp", "Axaml" })
        {
            var resource = $"DemoCenter.Resources.Highlighters.{language}-Highlight-{variant}.xshd";
            using var stream = typeof(App).Assembly.GetManifestResourceStream(resource);
            Assert.True(stream != null, $"{resource} is missing.");

            foreach (var element in XDocument.Load(stream).Descendants().Where(x => x.Name.LocalName == "Color"))
            {
                var foreground = element.Attribute("foreground")?.Value;
                if (foreground == null || !TryParse(foreground, out var colour))
                    continue;

                yield return ($"{language}-{variant}", element.Attribute("name")?.Value ?? foreground, colour);
            }
        }
    }

    private static bool TryParse(string value, out Color colour) => Color.TryParse(value, out colour);

    private static string ToHex(Color colour) => $"#{colour.R:X2}{colour.G:X2}{colour.B:X2}";

    private static double Contrast(Color foreground, Color background)
    {
        var a = Luminance(foreground) + 0.05;
        var b = Luminance(background) + 0.05;
        return a > b ? a / b : b / a;
    }

    private static double Luminance(Color colour) =>
        0.2126 * Channel(colour.R) + 0.7152 * Channel(colour.G) + 0.0722 * Channel(colour.B);

    private static double Channel(byte value)
    {
        var c = value / 255.0;
        return c <= 0.03928 ? c / 12.92 : Math.Pow((c + 0.055) / 1.055, 2.4);
    }
}
