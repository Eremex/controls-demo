using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class CartesianScatterLineSeriesViewViewModel : ChartsPageViewModel
{
    const int ItemsCount = 500;

    [ObservableProperty] SeriesViewModel series;

    public CartesianScatterLineSeriesViewViewModel()
    {
        var data = new List<(double, double)>(ItemsCount);
        for (int i = 0; i < ItemsCount; i++)
        {
            double factor = 0.1 * i;
            data.Add((factor * Math.Cos(factor), factor * Math.Sin(factor)));
        }

        Series = new() { Color = Color.FromArgb(255, 189, 20, 54), DataAdapter = new ScatterDataAdapter(data) };
    }
}
