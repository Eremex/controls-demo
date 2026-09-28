using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class CartesianChartLargeDataPageViewModel : ChartsPageViewModel
{
    static double Formula(double argument, int index) => Math.Sin(argument) +
                                                         Math.Sin(argument / 100.0) +
                                                         30 * Math.Sin(argument / 1000.0) +
                                                         1000 * (1 + 0.1 * index % 10) *
                                                         Math.Sin(argument / 100000.0 + Math.PI / 6 * index);

    const int ItemsCount = 1_000_000;

    [ObservableProperty] ObservableCollection<SeriesViewModel> series = new()
    {
        new SeriesViewModel { Color = Color.FromArgb(255, 189, 20, 54), DataAdapter = new FormulaDataAdapter(0, 1, ItemsCount, arg => Formula(arg, 0))},
        new SeriesViewModel { Color = Color.FromArgb(255, 0, 120, 122), DataAdapter = new FormulaDataAdapter(0, 1, ItemsCount, arg => Formula(arg, 1))},
    };
}
