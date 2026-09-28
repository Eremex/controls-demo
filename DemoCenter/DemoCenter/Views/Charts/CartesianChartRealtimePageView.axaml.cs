using Avalonia.Controls;
using Avalonia.Interactivity;
using DemoCenter.ViewModels;

namespace DemoCenter.Views;

public partial class CartesianChartRealtimePageView : UserControl
{
    CartesianChartRealtimePageViewModel ViewModel => DataContext as CartesianChartRealtimePageViewModel;
    
    public CartesianChartRealtimePageView()
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
