using System.Globalization;
using System.Numerics;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace DemoCenter.Views;

public partial class Graphics3DControlSimpleMaterialsView : UserControl
{
    public Graphics3DControlSimpleMaterialsView()
    {
        InitializeComponent();
    }
}

public class ColorToVector3Converter : IValueConverter
{
    static byte ToByte(float value) => (byte)(value * byte.MaxValue);
    static float ToFloat(byte value) => (float)value / byte.MaxValue;
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Vector3 vector && targetType == typeof(Color))
            return new Color(byte.MaxValue, ToByte(vector.X), ToByte(vector.Y), ToByte(vector.Z));
        return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Color color && targetType == typeof(Vector3))
            return new Vector3(ToFloat(color.R), ToFloat(color.G), ToFloat(color.B));
        return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
    }
}