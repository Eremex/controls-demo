using System;
using System.Globalization;

using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Avalonia.Svg.Skia;

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
            var uri = $"avares://DemoCenter/Images/Group=PCB, Icon=Lock {((bool)value ? "True" : "False")}.svg";
            return SvgImageExtension.ProvideValue(uri, null!, null!);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
