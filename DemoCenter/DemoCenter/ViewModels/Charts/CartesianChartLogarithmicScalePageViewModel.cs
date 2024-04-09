using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.ViewModels.DataAdapters;

namespace DemoCenter.ViewModels;

public partial class CartesianChartLogarithmicScalePageViewModel : ChartsPageViewModel
{
    const double LogBase = 10;
    
    [ObservableProperty] DistortionDataAdapter data1 = new(0);
    [ObservableProperty] DistortionDataAdapter data2 = new(1);
    [ObservableProperty] Color color1 = Colors.Red;
    [ObservableProperty] Color color2 = Colors.Blue;
    [ObservableProperty] LogarithmicAxisViewModel axisX = new() { LogarithmicBase = LogBase };
    [ObservableProperty] LogarithmicAxisViewModel axisY = new() { LogarithmicBase = LogBase };
    [ObservableProperty] bool logarithmicX = true;
    [ObservableProperty] bool logarithmicY = true;
    [ObservableProperty] CustomLabelFormatter frequencyFormatter = new(o => $"{o} Hz");
    [ObservableProperty] private CustomLabelFormatter percentFormatter = new(o => $"{o}%");

    partial void OnLogarithmicXChanged(bool value) => AxisX.LogarithmicBase = value ? LogBase : null;
    partial void OnLogarithmicYChanged(bool value) => AxisY.LogarithmicBase = value ? LogBase : null;
}
