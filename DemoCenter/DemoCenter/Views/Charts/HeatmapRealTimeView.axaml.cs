using Avalonia.Controls;
using Avalonia.Interactivity;
using DemoCenter.ViewModels;

namespace DemoCenter.Views;

public partial class HeatmapRealTimeView : UserControl
{
    HeatmapRealTimeViewModel ViewModel => DataContext as HeatmapRealTimeViewModel;

    
    public HeatmapRealTimeView()
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