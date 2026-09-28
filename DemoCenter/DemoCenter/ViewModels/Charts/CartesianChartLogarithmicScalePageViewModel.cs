using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class CartesianChartLogarithmicScalePageViewModel : ChartsPageViewModel
{
    static readonly Color[] colors = new[]
    {
        Color.FromArgb(255, 168, 207, 213),
        Color.FromArgb(255, 163, 201, 207),
        Color.FromArgb(255, 138, 193, 201),
        Color.FromArgb(255, 107, 178, 188),
        Color.FromArgb(255, 103, 171, 181),
        Color.FromArgb(255, 31, 114, 115),
        Color.FromArgb(255, 0, 95, 96)
    };

    const double LogBase = 10;
    
    [ObservableProperty] LogarithmicAxisViewModel axisX = new() { LogarithmicBase = LogBase };
    [ObservableProperty] bool logarithmicX = true;
    [ObservableProperty] CustomLabelFormatter frequencyFormatter = new(o => $"{o} Hz");
    [ObservableProperty] ObservableCollection<SeriesViewModel> series = new();

    public CartesianChartLogarithmicScalePageViewModel()
    {
        var data = CsvSources.Logarithmic;
        for (int i = 0; i < 7; i++)
            series.Add(new SeriesViewModel { Color = colors[i], DataAdapter = CreateDataAdapter(data, i + 1) });
    }
    SortedNumericDataAdapter CreateDataAdapter(List<CsvDoubleColumn> data, int valueIndex)
    {
        var result = new SortedNumericDataAdapter();
        for (int i = 0; i < data[0].Data.Count; i++)
            result.Add(data[0].Data[i], data[valueIndex].Data[i]);
        return result;
    }
    partial void OnLogarithmicXChanged(bool value) => AxisX.LogarithmicBase = value ? LogBase : null;
}
