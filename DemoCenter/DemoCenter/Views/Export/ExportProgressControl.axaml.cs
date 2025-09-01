using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json.Linq;

namespace DemoCenter;

public partial class ExportProgressControl : UserControl
{
    public static readonly StyledProperty<double> ValueProperty = AvaloniaProperty.Register<ExportProgressControl, double>(nameof(Value));

    public double Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public ExportProgressControl()
    {
        InitializeComponent();
    }
}