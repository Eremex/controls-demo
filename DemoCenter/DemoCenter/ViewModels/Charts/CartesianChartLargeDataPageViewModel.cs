using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.ViewModels.DataAdapters;
using Eremex.Avalonia.Charts;

namespace DemoCenter.ViewModels;

public partial class CartesianChartLargeDataPageViewModel : ChartsPageViewModel
{
    static double Formula(double argument, int index) => Math.Sin(argument) +
                                                         Math.Sin(argument / 100.0) +
                                                         30 * Math.Sin(argument / 1000.0) +
                                                         1000 * (1 + 0.1 * index % 10) *
                                                         Math.Sin(argument / 100000.0 + Math.PI / 6 * index);

    const int ItemsCount = 1_000_000;

    [ObservableProperty] FormulaDataAdapter data1 = new(0, 1, ItemsCount, arg => Formula(arg, 0));
    [ObservableProperty] FormulaDataAdapter data2 = new(0, 1, ItemsCount, arg => Formula(arg, 1));
}
