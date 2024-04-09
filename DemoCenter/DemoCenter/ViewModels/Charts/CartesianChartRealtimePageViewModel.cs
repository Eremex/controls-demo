using Avalonia.Threading;
using DemoCenter.ViewModels.DataAdapters;
using Eremex.Avalonia.Charts;

namespace DemoCenter.ViewModels;

public class CartesianChartRealtimePageViewModel : ChartsPageViewModel
{
    readonly DispatcherTimer timer = new(DispatcherPriority.Background);
    readonly RealtimeDataGenerator generator = new(6, 2000, 15);

    public SortedTimeSpanDataAdapter Data1 => generator.Adapters[0];
    public SortedTimeSpanDataAdapter Data2 => generator.Adapters[1];
    public SortedTimeSpanDataAdapter Data3 => generator.Adapters[2];
    public SortedTimeSpanDataAdapter Data4 => generator.Adapters[3];
    public SortedTimeSpanDataAdapter Data5 => generator.Adapters[4];
    public SortedTimeSpanDataAdapter Data6 => generator.Adapters[5];
    
    public CartesianChartRealtimePageViewModel()
    {
        generator.GenerateInitialData();
        timer.Tick += (_, _) => generator.UpdateAdapters(); 
        timer.Interval = TimeSpan.FromMilliseconds(15);
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
