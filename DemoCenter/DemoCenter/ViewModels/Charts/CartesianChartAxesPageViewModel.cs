using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class CartesianChartAxesPageViewModel : ChartsPageViewModel
{
    static readonly Color Color1 = Color.FromArgb(255, 189, 20, 54);
    static readonly Color Color2 = Color.FromArgb(255, 0, 120, 122);
    
    static double Formula1(double argument) => Math.Pow(argument, 2);
    static double Formula2(double argument) => Math.Pow(argument, 1.7);

    [ObservableProperty] AxisViewModel selectedAxis;
    [ObservableProperty] ObservableCollection<AxisViewModel> axesX = new()
    {
        new AxisViewModel { Key = "1", Title = "First Axis X", Color = Color1, ShowInterlacing = true, ShowMajorGridlines = true, ShowMinorGridlines = true },
        new AxisViewModel { Key = "2", Title = "Second Axis X", Color = Color2, Position = AxisPosition.Far }
    };
    [ObservableProperty] ObservableCollection<AxisViewModel> axesY = new()
    {
        new AxisViewModel { Key = "1", Title = "First Axis Y", Color = Color1, ShowInterlacing = true, ShowMajorGridlines = true, ShowMinorGridlines = true, Reverse = true },
        new AxisViewModel { Key = "2", Title = "Second Axis Y", Color = Color2, Position = AxisPosition.Far }
    };
    [ObservableProperty] ObservableCollection<SeriesViewModel> series = new()
    {
        new SeriesViewModel { Color = Color1, AxisXKey = "1", AxisYKey = "1", DataAdapter = new FormulaDataAdapter(0, 1, 100, Formula1)},
        new SeriesViewModel { Color = Color2, AxisXKey = "2", AxisYKey = "2", DataAdapter = new FormulaDataAdapter(0, 1, 200, Formula2)},
    };

    public IEnumerable<AxisViewModel> Axes
    {
        get
        {
            foreach (var axis in AxesX)
                yield return axis;
            foreach (var axis in AxesY)
                yield return axis;
        }
    }

    public CartesianChartAxesPageViewModel()
    {
        SelectedAxis = AxesX.First();
    }
}
