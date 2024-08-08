using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class CartesianSideBySideBarSeriesViewViewModel : ChartsPageViewModel
{
    [ObservableProperty] private ObservableCollection<SeriesViewModel> series = new();

    public CartesianSideBySideBarSeriesViewViewModel()
    {
        var random = new Random(4);
        var start = DateTime.Now.AddMonths(-1);
        SortedDateTimeDataAdapter adapter1 = new();
        SortedDateTimeDataAdapter adapter2 = new();
        for (int i = 0; i < 12; i++)
        {
            var argument = start.AddDays(i);
            adapter1.Add(argument, random.NextDouble() * 100 - 30);
            adapter2.Add(argument, random.NextDouble() * 100 - 30);
        }
        
        series.Add(new SeriesViewModel { Color = Color.FromArgb(180, 189, 20, 54), DataAdapter = adapter1 });
        series.Add(new SeriesViewModel { Color = Color.FromArgb(180, 0, 120, 122), DataAdapter = adapter2 });
    }
}
