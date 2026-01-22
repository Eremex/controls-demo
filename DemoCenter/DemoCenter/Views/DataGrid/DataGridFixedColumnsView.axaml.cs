using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using System.Globalization;

namespace DemoCenter.Views;

public partial class DataGridFixedColumnsView : UserControl
{
    public DataGridFixedColumnsView()
    {
        InitializeComponent();
    }
}

public class SeparatorWidthConverter : MarkupExtension, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {   
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Math.Round((double)value);
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}
