using System.Globalization;
using System.Numerics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.Editors;
using Eremex.AvaloniaUI.Controls3D;
using Vector = Avalonia.Vector;

namespace DemoCenter.Views;

public partial class Graphics3DControlSimpleMaterialsView : UserControl
{
    Graphics3DControlSimpleMaterialsViewModel ViewModel => DataContext as Graphics3DControlSimpleMaterialsViewModel;
    
    public Graphics3DControlSimpleMaterialsView()
    {
        InitializeComponent();
    }
    void SelectingItemsControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SkyboxSelector.SelectedIndex == 0)
        {
            ViewModel.Skybox = null;
            AmbientColorEditor.IsEnabled = false;
        }
        else
        {
            ViewModel.Skybox = new Skybox { IsVisible = false };
            AmbientColorEditor.IsEnabled = true;
            UpdateSkyboxColor();
        }
    }
    void AmbientColorEditor_OnEditorValueChanged(object sender, EditorValueChangedEventArgs e) => UpdateSkyboxColor();
    void UpdateSkyboxColor()
    {
        if (ViewModel.Skybox is not null && AmbientColorEditor.Color.HasValue)
        {
            var image = CreateImage(AmbientColorEditor.Color.Value);
            ViewModel.Skybox.Left = image;
            ViewModel.Skybox.Right = image;
            ViewModel.Skybox.Top = image;
            ViewModel.Skybox.Bottom = image;
            ViewModel.Skybox.Front = image;
            ViewModel.Skybox.Rear = image;
        }
    }
    Bitmap CreateImage(Color color)
    {
        var bitmap = new RenderTargetBitmap(new PixelSize(100, 100), new Vector(96, 96));
        using var context = bitmap.CreateDrawingContext();
        context.FillRectangle(new SolidColorBrush(color), new Rect(0, 0, 100, 100));
        return bitmap;
    }
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        SkyboxSelector.SelectedIndex = 0;
    }
}

public class Vector3ToColorConverter : IValueConverter
{
    static byte ToByte(float value) => (byte)(value * byte.MaxValue);
    static float ToFloat(byte value) => (float)value / byte.MaxValue;
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Vector3 vector)
            return new Color(byte.MaxValue, ToByte(vector.X), ToByte(vector.Y), ToByte(vector.Z));
        return null;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Color color)
            return new Vector3(ToFloat(color.R), ToFloat(color.G), ToFloat(color.B));
        return null;
    }
}