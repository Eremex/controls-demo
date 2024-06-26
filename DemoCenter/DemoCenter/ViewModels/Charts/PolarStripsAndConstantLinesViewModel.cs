using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class PolarStripsAndConstantLinesViewModel : ChartsPageViewModel
{
    [ObservableProperty] SeriesViewModel series = new() { Color = Color.FromArgb(255, 189, 20, 54), DataAdapter = CreateAdapter() };
    [ObservableProperty] ObservableCollection<ConstantLineViewModel> constantLinesX = new();
    [ObservableProperty] ObservableCollection<ConstantLineViewModel> constantLinesY = new();
    
    static ISeriesDataAdapter CreateAdapter()
    {
        var values = new List<(double, double)>
        {
            (0, 16), (5, 15.7), (10, 15), (16, 13), (20, 10), (22, 5), (26, 0), (28, -5), (29, 0), (30, -10), (32, -5),
            (34, 0), (39, 3), (44, 0), (45, -5), (48, -10), (50, -5), (52, 0), (55, 2), (58, 0), (60, -3), (62, -5),
            (65, -2), (68, 0), (70, 1), (72, 0), (76, -6), (80, -3), (85, -2), (88, -5), (91, -10), (98, -6), (100, -7),
            (102, -10), (104, -16), (106, -12), (110, -10), (114, -8), (118, -10), (120, -15), (125, -18), (127, -10),
            (130, -7), (137, -6), (138, -7), (140, -9), (146, -5), (150, -3), (154, -2), (160, -4), (165, -5),
            (170, -8), (180, -9)
        };
        int count = values.Count;
        for (int i = count - 1; i >= 0; i--)
            values.Add((360 - values[i].Item1, values[i].Item2));
        return new SortedNumericDataAdapter(values);
    }

    [RelayCommand]
    void ClearConstantLines()
    {
        ConstantLinesX.Clear();
        ConstantLinesY.Clear();
    }
}
