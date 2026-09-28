using System.Collections.ObjectModel;
using Avalonia.Media;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.ViewModels.DataAdapters;

namespace DemoCenter.ViewModels;

public partial class CartesianChartRealtimePageViewModel : ChartsPageViewModel
{
    static readonly Color[] colors = new[]
    {
        Color.FromArgb(255, 204, 55, 28),
        Color.FromArgb(255, 255, 106, 0),
        Color.FromArgb(255, 0, 148, 255),
        Color.FromArgb(255, 119, 133, 255),
        Color.FromArgb(255, 0, 127, 128),
        Color.FromArgb(255, 91, 171, 171)
    };
    
    readonly DispatcherTimer timer = new(DispatcherPriority.Background);
    readonly RealtimeDataGenerator generator = new(6, 500, 35);

    [ObservableProperty] ObservableCollection<SeriesViewModel> series = new();
    
    public CartesianChartRealtimePageViewModel()
    {
        generator.GenerateInitialData();
        timer.Tick += (_, _) => generator.UpdateAdapters(); 
        timer.Interval = TimeSpan.FromMilliseconds(2);

        for (int i = 0; i < generator.Adapters.Length; i++)
            series.Add(new SeriesViewModel { Color = colors[i], DataAdapter = generator.Adapters[i] });
    }
    public void Start()
    {
        generator.Start();
        timer.Start();
    }
    public void Stop()
    {
        generator.Stop();
        timer.Stop();
    }
}
