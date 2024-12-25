using Avalonia.Controls;
using Avalonia.Interactivity;
using DemoCenter.ViewModels;

namespace DemoCenter.Views;

public partial class Graphics3DControlTransformationView : UserControl
{
    private Graphics3DControlTransformationViewModel ViewModel => DataContext as Graphics3DControlTransformationViewModel;
        
    public Graphics3DControlTransformationView()
    {
        InitializeComponent();
    }
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        ViewModel?.Start();
    }
    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
        ViewModel?.Stop();
    }
}
