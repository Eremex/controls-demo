using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class CartesianLollipopSeriesViewViewModel : ChartsPageViewModel
{
    [ObservableProperty] SortedNumericDataAdapter dataAdapter = new();
    [ObservableProperty] CartesianLollipopOrientation lineOrientation = CartesianLollipopOrientation.Vertical;
    [ObservableProperty] double lineThickness = 1;
    [ObservableProperty] double markerSize = 12;
    
    public CartesianLollipopSeriesViewViewModel()
    {
        const int step = 30; 
        for (int i = -step; i < step; i++)
        {
            double x = 0.5 * Math.PI * i / step;
            double y = Math.Sin(x);
            DataAdapter.Add(x, y);
        }
    }
}