using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class CartesianSideBySideRangeBarSeriesViewViewModel : ChartsPageViewModel
{
    [ObservableProperty] private ObservableCollection<SeriesViewModel> series = new();

    public CartesianSideBySideRangeBarSeriesViewViewModel()
    {
        var random = new Random(10);
        NumericRangeDataAdapter adapter1 = new();
        NumericRangeDataAdapter adapter2 = new();
        for (int i = 0; i < 12; i++)
        {
            adapter1.Add(i, random.NextDouble() * 100, random.NextDouble() * 100);
            adapter2.Add(i, random.NextDouble() * 100, random.NextDouble() * 100);
        }
        
        series.Add(new SeriesViewModel { Color = Color.FromArgb(180, 189, 20, 54), DataAdapter = adapter1 });
        series.Add(new SeriesViewModel { Color = Color.FromArgb(180, 0, 120, 122), DataAdapter = adapter2 });
    }
}
