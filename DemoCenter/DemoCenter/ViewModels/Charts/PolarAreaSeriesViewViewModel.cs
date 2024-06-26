using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class PolarAreaSeriesViewViewModel : ChartsPageViewModel
{
    static double Cos(double argument) => Math.Cos(4 * Math.PI * argument / 180) + 1;

    const int ItemsCount = 180;
    const double Step = 360d / ItemsCount;

    [ObservableProperty] ObservableCollection<SeriesViewModel> series = new()
    {
        new SeriesViewModel { Color = Color.FromArgb(255, 189, 20, 54), DataAdapter = new FormulaDataAdapter(0, Step, ItemsCount + 1, Cos)},
    };
}
