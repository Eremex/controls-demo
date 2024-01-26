using System.Reflection;
using System.Xml;
using Avalonia.Markup.Xaml;
using AvaloniaEdit.Highlighting;
using AvaloniaEdit.Highlighting.Xshd;

namespace DemoCenter.Helpers; 

public class ThemedSyntaxHighlighter : MarkupExtension {
    public ThemedSyntaxHighlighter(string highlightName) {
        string themeName = App.Current.GetValue(App.RequestedThemeVariantProperty).ToString();
        string resourceName = $"DemoCenter.Resources.Highlighters.{highlightName}-{themeName}.xshd";
        using(var stream = Assembly.GetExecutingAssembly()
                  ?.GetManifestResourceStream(resourceName)) {
            HighlightingDefinition = HighlightingLoader.Load(new XmlTextReader(stream), null);
        }
    }

    public IHighlightingDefinition HighlightingDefinition { get; set; }
    public override object ProvideValue(IServiceProvider serviceProvider) {
        return this;
    }
}