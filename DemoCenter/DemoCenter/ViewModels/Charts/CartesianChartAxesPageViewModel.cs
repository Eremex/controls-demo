using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.ViewModels.DataAdapters;
using Eremex.Avalonia.Charts;

namespace DemoCenter.ViewModels;

public partial class CartesianChartAxesPageViewModel : ChartsPageViewModel
{
    static double Formula1(double argument) => Math.Pow(argument, 2);
    static double Formula2(double argument) => Math.Pow(argument, 1.7);

    [ObservableProperty] FormulaDataAdapter data1 = new(0, 1, 100, Formula1);
    [ObservableProperty] FormulaDataAdapter data2 = new(0, 1, 200, Formula2);
    [ObservableProperty] private Color color1 = Color.FromArgb(255, 189, 20, 54);
    [ObservableProperty] Color color2 = Color.FromArgb(255, 0, 120, 122);
    [ObservableProperty] AxisViewModel axisX1 = new() { Title = "First Axis X", ShowInterlacing = true, ShowMajorGridlines = true, ShowMinorGridlines = true };
    [ObservableProperty] AxisViewModel axisX2 = new() { Title = "Second Axis X", Position = AxisPosition.Far };
    [ObservableProperty] AxisViewModel axisY1 = new() { Title = "First Axis Y", ShowInterlacing = true, ShowMajorGridlines = true, ShowMinorGridlines = true };
    [ObservableProperty] AxisViewModel axisY2 = new() { Title = "Second Axis Y", Position = AxisPosition.Far };
    [ObservableProperty] AxisViewModel selectedAxis;
    [ObservableProperty] ObservableCollection<AxisViewModel> axes;

    public CartesianChartAxesPageViewModel()
    {
        SelectedAxis = AxisX1;
        Axes = new ObservableCollection<AxisViewModel>() { AxisX1, AxisX2, AxisY1, AxisY2 };
    }
}
