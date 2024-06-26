using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class CartesianStepLineSeriesViewViewModel : ChartsPageViewModel
{
    static Random Random = new(1);
    static double Formula1(double argument) => Random.NextDouble() * 20;
    static double Formula2(double argument) => Random.NextDouble() * 20 - 10;

    const int ItemsCount = 10;

    [ObservableProperty] ObservableCollection<SeriesViewModel> series = new()
    {
        new SeriesViewModel { Color = Color.FromArgb(255, 189, 20, 54), DataAdapter = new FormulaDataAdapter(0, 1, ItemsCount, Formula1)},
        new SeriesViewModel { Color = Color.FromArgb(255, 0, 120, 122), DataAdapter = new FormulaDataAdapter(0, 1, ItemsCount, Formula2)},
    };
    
    public CartesianStepLineSeriesViewViewModel()
    {
        Random = new Random(1);
    }
}
