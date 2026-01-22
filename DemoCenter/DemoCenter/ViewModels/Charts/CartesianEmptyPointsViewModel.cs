using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class CartesianEmptyPointsViewModel : ChartsPageViewModel
{
    const int PointsCount = 100;

    static bool IsEmpty(double v) => v is > 30 and < 40 or > 60 and < 70;
    
    [ObservableProperty] List<ViewViewModel> views;
    [ObservableProperty] SeriesViewBase selectedView;
    [ObservableProperty] ISeriesDataAdapter dataAdapter;

    public CartesianEmptyPointsViewModel()
    {
        var color = Color.FromArgb(255, 189, 20, 54);
        var color2 = Color.FromArgb(255, 0, 120, 122);
        
        var simpleAdapter = new FormulaDataAdapter(0, 1, PointsCount, d => IsEmpty(d) ? double.NaN : Math.Sin(d / 10));
        views =
        [
            new("Line", simpleAdapter, new CartesianLineSeriesView { Color = color, ShowMarkers = true }),
            new("Area", simpleAdapter, new CartesianAreaSeriesView { Color = color, ShowMarkers = true }),
            new("Scatter Line", CreateScatterData(), new CartesianScatterLineSeriesView { Color = color, ShowMarkers = true }),
            new("Step Line", simpleAdapter, new CartesianStepLineSeriesView { Color = color, ShowMarkers = true }),
            new("Step Area", simpleAdapter, new CartesianStepAreaSeriesView { Color = color, ShowMarkers = true }),
            new("Range Area", CreateRangeData(), new CartesianRangeAreaSeriesView { Color = Color.FromArgb(255, 67, 201, 39), Color1 = color, Color2 = color2, ShowMarkers1 = true, ShowMarkers2 = true })
        ];
        selectedView = views[0].View;
        dataAdapter = views[0].DataAdapter;
    }

    ScatterDataAdapter CreateScatterData()
    {
        var scatterData = new List<(double, double)>(PointsCount);
        for (int i = 0; i < PointsCount; i++)
        {
            double factor = 0.1 * i;
            double v = IsEmpty(i) ? double.NaN : factor * Math.Sin(factor);
            scatterData.Add((factor * Math.Cos(factor), v));
        }
        return new ScatterDataAdapter(scatterData);
    }
    TimeSpanRangeDataAdapter CreateRangeData()
    {
        var rangeData = new List<(TimeSpan, double, double)>(PointsCount);
        for (double i = 0; i < PointsCount; i++)
        {
            double v = IsEmpty(i) ? double.NaN : Math.Sin(i / 10);
            rangeData.Add((TimeSpan.FromSeconds(i), v, v - 1));
        }
        return new TimeSpanRangeDataAdapter(rangeData);
    }
}
