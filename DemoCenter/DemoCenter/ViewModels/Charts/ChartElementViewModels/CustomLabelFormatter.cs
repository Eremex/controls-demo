using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public class CustomLabelFormatter : IAxisLabelFormatter
{
    readonly Func<object, string> formatFunc;

    public CustomLabelFormatter(Func<object, string> formatFunc)
    {
        this.formatFunc = formatFunc;
    }
    public string Format(object value) => formatFunc(value);
}
