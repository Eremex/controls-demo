using System.Globalization;

using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Eremex.AvaloniaUI.Icons;

namespace DemoCenter.Views
{
    public partial class TextEditingPageView : UserControl
    {
        public TextEditingPageView()
        {
            InitializeComponent();
        }
    }

    public class LockButtonImageConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            return (bool)value ? Basic.Lock_True : Basic.Lock_False;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
