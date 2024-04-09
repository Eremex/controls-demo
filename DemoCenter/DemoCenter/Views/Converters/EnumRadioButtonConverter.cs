using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Xml;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using AvaloniaEdit.Highlighting;
using DemoCenter.Helpers;
using DemoCenter.ProductsData;
using DemoCenter.ViewModels;

using Eremex.AvaloniaUI.Controls.TreeList;
using Eremex.AvaloniaUI.Themes;

namespace DemoCenter.Views;

public class EnumRadioButtonConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var currentValue = Enum.Parse(value.GetType(), (string)parameter);
        return value.Equals(currentValue);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var currentValue = Enum.Parse(targetType, (string)parameter);
        return (bool)value ? currentValue : BindingOperations.DoNothing;
    }
}

public class NullValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
        return DefaultValueConverter.Instance.Convert(value, targetType, parameter, culture);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
