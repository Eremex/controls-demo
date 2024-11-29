using Avalonia.Controls;
using Avalonia.Interactivity;
using Eremex.AvaloniaUI.Controls3D;

namespace DemoCenter.Views;

public partial class Graphics3DControlCameraView : UserControl
{
    public Graphics3DControlCameraView()
    {
        InitializeComponent();
    }
    void Button_OnClick(object sender, RoutedEventArgs e) => DemoControl.LookCameraAtScene((CameraPosition)CameraPosition.EditorValue!);
}
