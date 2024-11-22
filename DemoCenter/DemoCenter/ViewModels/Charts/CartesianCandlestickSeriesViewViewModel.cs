using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class CartesianCandlestickSeriesViewViewModel : ChartsPageViewModel
{
    [ObservableProperty] CandlestickDataAdapter stockData;
    [ObservableProperty] SortedDateTimeDataAdapter volumeData;
    
    public CartesianCandlestickSeriesViewViewModel()
    {
        var data = CsvSources.Stock;
        var arguments = new List<DateTime>(data.Count);
        var open = new List<double>(data.Count);
        var high = new List<double>(data.Count);
        var low = new List<double>(data.Count);
        var close = new List<double>(data.Count);
        var volume = new List<double>(data.Count);
        for (var i = data.Count - 1; i >= 0; i--)
        {
            arguments.Add(data[i].Date);
            open.Add(data[i].Open);
            high.Add(data[i].High);
            low.Add(data[i].Low);
            close.Add(data[i].Close);
            volume.Add(data[i].Volume);
        }
        StockData = new CandlestickDataAdapter(arguments, open, high, low, close);
        volumeData = new SortedDateTimeDataAdapter(arguments, volume);
    }
}
