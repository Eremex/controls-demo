using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using DemoCenter.DemoData;
using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DemoCenter.Views
{
    public partial class EnumSourcePageView : UserControl
    {
        public EnumSourcePageView()
        {
            InitializeComponent();
        }
    }

    public class EnumDescriptionConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var type = value?.GetType();
            var text = value?.ToString();
            if (type == null || !type.IsEnum || string.IsNullOrEmpty(text))
                return null;
            return $"{type.Name}_{text}";
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
