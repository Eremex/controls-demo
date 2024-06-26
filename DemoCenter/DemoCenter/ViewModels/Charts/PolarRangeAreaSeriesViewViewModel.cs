using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class PolarRangeAreaSeriesViewViewModel : ChartsPageViewModel
{
    [ObservableProperty] Color color = Color.FromArgb(255, 67, 201, 39);
    [ObservableProperty] Color color1 = Color.FromArgb(255, 189, 20, 54);
    [ObservableProperty] Color color2 = Color.FromArgb(255, 0, 120, 122);
    [ObservableProperty] NumericRangeDataAdapter dataAdapter = new();

    public PolarRangeAreaSeriesViewViewModel()
    {
        for (int i = 0; i <= 360; i += 5)
        {
            double value1 = Math.Sin(Math.PI * i / 720);
            double value2 = Math.Cos(Math.PI * i / 720);
            DataAdapter.Add(i, value1, value2);
        }
    }
}
