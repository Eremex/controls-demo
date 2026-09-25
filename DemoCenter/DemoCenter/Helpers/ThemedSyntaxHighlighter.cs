using System.Reflection;
using System.Xml;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using AvaloniaEdit.Highlighting;
using AvaloniaEdit.Highlighting.Xshd;

namespace DemoCenter.Helpers;

public class ThemedSyntaxHighlighter : MarkupExtension {
    public ThemedSyntaxHighlighter(string highlightName) {
        // ActualThemeVariant, because there is no highlighting file for the system variant.
        var themeName = App.Current.ActualThemeVariant == ThemeVariant.Dark ? "Dark" : "Light";
        var resourceName = $"DemoCenter.Resources.Highlighters.{highlightName}-{themeName}.xshd";

        using var stream = Assembly.GetExecutingAssembly()?.GetManifestResourceStream(resourceName);
        if (stream == null)
            return;

        HighlightingDefinition = HighlightingLoader.Load(new XmlTextReader(stream), null);
    }

    public IHighlightingDefinition HighlightingDefinition { get; set; }
    public override object ProvideValue(IServiceProvider serviceProvider) {
        return this;
    }
}
