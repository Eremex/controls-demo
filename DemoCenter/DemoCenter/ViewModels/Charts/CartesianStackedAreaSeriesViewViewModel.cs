using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class CartesianStackedAreaSeriesViewViewModel : ChartsPageViewModel
{
    [ObservableProperty] ObservableCollection<SeriesViewModel> series = new()
    {
        new SeriesViewModel { Color = Color.FromUInt32(0xFF9B59B6), DataAdapter = new SortedNumericDataAdapter()},
        new SeriesViewModel { Color = Color.FromUInt32(0xFFE74C3C), DataAdapter = new SortedNumericDataAdapter()},
        new SeriesViewModel { Color = Color.FromUInt32(0xFFF39C12), DataAdapter = new SortedNumericDataAdapter()}
    };

    public CartesianStackedAreaSeriesViewViewModel()
    {
        const int pointCount = 15;
        const int step = 1;
        var random = new Random();
        for (int i = 0; i < pointCount; i++)
        {
            ((SortedNumericDataAdapter)series[0].DataAdapter).Add(i * step, 20 + 10 * Math.Cos(i * 0.1) + random.NextDouble() * 5);
            ((SortedNumericDataAdapter)series[1].DataAdapter).Add(i * step, 15 + 8 * Math.Sin(i * 0.6) + random.NextDouble() * 4);
            ((SortedNumericDataAdapter)series[2].DataAdapter).Add(i * step, 8 + 8 * Math.Cos(i * 0.1) + random.NextDouble() * 4);
        }
    }
}
