using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;
using Eremex.Avalonia.Charts;

namespace DemoCenter.ViewModels;

public partial class CartesianChartLogarithmicScalePageViewModel : ChartsPageViewModel
{
    const double LogBase = 10;
    
    [ObservableProperty] SortedNumericDataAdapter data1;
    [ObservableProperty] SortedNumericDataAdapter data2;
    [ObservableProperty] SortedNumericDataAdapter data3;
    [ObservableProperty] SortedNumericDataAdapter data4;
    [ObservableProperty] SortedNumericDataAdapter data5;
    [ObservableProperty] SortedNumericDataAdapter data6;
    [ObservableProperty] SortedNumericDataAdapter data7;
    [ObservableProperty] LogarithmicAxisViewModel axisX = new() { LogarithmicBase = LogBase };
    [ObservableProperty] bool logarithmicX = true;
    [ObservableProperty] CustomLabelFormatter frequencyFormatter = new(o => $"{o} Hz");

    public CartesianChartLogarithmicScalePageViewModel()
    {
        var data = CsvSources.Logarithmic;
        data1 = CreateDataAdapter(data, 1);
        data2 = CreateDataAdapter(data, 2);
        data3 = CreateDataAdapter(data, 3);
        data4 = CreateDataAdapter(data, 4);
        data5 = CreateDataAdapter(data, 5);
        data6 = CreateDataAdapter(data, 6);
        data7 = CreateDataAdapter(data, 7);
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
