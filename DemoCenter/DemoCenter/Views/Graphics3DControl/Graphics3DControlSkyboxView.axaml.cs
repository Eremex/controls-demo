using Avalonia.Controls;
using Avalonia.Interactivity;
using Eremex.AvaloniaUI.Controls3D;

namespace DemoCenter.Views;

public partial class Graphics3DControlSkyboxView : UserControl
{
    public Graphics3DControlSkyboxView()
    {
        InitializeComponent();
    }
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        DemoControl.LookCameraAtScene(CameraPosition.Front);
    }
}
