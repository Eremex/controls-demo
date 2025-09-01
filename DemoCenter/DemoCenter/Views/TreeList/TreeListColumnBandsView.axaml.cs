using Avalonia.Controls;
using Avalonia.Data.Converters;
using System.Globalization;

namespace DemoCenter.Views;

public partial class TreeListColumnBandsView : UserControl
{
    public TreeListColumnBandsView()
    {
        InitializeComponent();
    }
}

public class RowLevelToMaxValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is int level && level == 0)
            return 50000;
        return 10000;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}