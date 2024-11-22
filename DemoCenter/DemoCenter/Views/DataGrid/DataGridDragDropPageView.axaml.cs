using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using DemoCenter.DemoData.CsvClasses;
using Eremex.AvaloniaUI.Controls.DataControl;
using Eremex.AvaloniaUI.Controls.DataGrid;
using System.Globalization;

namespace DemoCenter.Views
{
    public partial class DataGridDragDropPageView : UserControl
    {
        public DataGridDragDropPageView()
        {
            InitializeComponent();            
        }        
    }

    public class SmallQuantityToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && (int)value < 15)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SmallQuantityToColorConverter : IMultiValueConverter
    {
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Count != 3)
                return Brushes.Transparent;

            if (values[0] is int quantity && quantity < 15 && values[2] is SolidColorBrush brush)
            {
                return new SolidColorBrush(brush.Color, 0.3);
            }
            return values[1];
        } 
    }
}
