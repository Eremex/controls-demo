using System.Collections;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Eremex.AvaloniaUI.Controls.TreeList;
using Eremex.AvaloniaUI.Controls3D;

namespace DemoCenter.Views;

public partial class Graphics3DControlHighlightingView : UserControl
{
    public Graphics3DControlHighlightingView()
    {
        InitializeComponent();
    }
}

public class MeshSelector : ITreeListChildrenSelector
{
    public IEnumerable SelectChildren(object item)
    {
        if (item is GeometryModel3D model)
            return model.Meshes;
        return null;
    }
    public bool HasChildren(object item) => item is GeometryModel3D;
}

public class ModelFontWeightConverter : MarkupExtension, IMultiValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider) => this;
    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Count == 3 && values[0] is SelectableElement element && (element is GeometryModel3D || element == values[1] || element == values[2]))
                return FontWeight.Bold;
        return FontWeight.Normal;
    }
}

public class ModelHighlightConverter : MarkupExtension, IMultiValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider) => this;
    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Count == 5 && values[0] is SelectableElement element)
        {
            if (Equals(values[3], element) && values[4] is Color color2)
                return new SolidColorBrush(color2);
            if (Equals(values[1], element) && values[2] is Color color1)
                return new SolidColorBrush(color1);
        }
        return null;
    }
}
