using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class CartesianRangeAreaSeriesViewViewModel : ChartsPageViewModel
{
    readonly Random random = new();

    [ObservableProperty] Color color = Color.FromArgb(255, 67, 201, 39);
    [ObservableProperty] Color color1 = Color.FromArgb(255, 189, 20, 54);
    [ObservableProperty] Color color2 = Color.FromArgb(255, 0, 120, 122);
    [ObservableProperty] NumericRangeDataAdapter dataAdapter = new();

    public CartesianRangeAreaSeriesViewViewModel()
    {
        for (int i = 0; i < 20; i++)
        {
            double argument = i;
            double value1 = random.NextDouble() + 1.5;
            double value2 = random.NextDouble() + 0.5;
            if (i is > 6 and < 13)
                DataAdapter.Add(argument, value2, value1);
            else
                DataAdapter.Add(argument, value1, value2);
        }
    }
}
