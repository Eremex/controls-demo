using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class PolarEmptyPointsViewModel : ChartsPageViewModel
{
    [ObservableProperty] List<ViewViewModel> views;
    [ObservableProperty] SeriesViewBase selectedView;
    [ObservableProperty] ISeriesDataAdapter dataAdapter;

    public PolarEmptyPointsViewModel()
    {
        var color = Color.FromArgb(255, 189, 20, 54);
        var color2 = Color.FromArgb(255, 0, 120, 122);
        
        var simpleAdapter = new FormulaDataAdapter(0, 2, 181, d => d is > 100 and < 170 or > 280 and < 350 ? double.NaN : Math.Cos(4 * Math.PI * d / 180) + 1);
        views =
        [
            new("Line", simpleAdapter, new PolarLineSeriesView { Color = color, ShowMarkers = true }),
            new("Area", simpleAdapter, new PolarAreaSeriesView { Color = color, ShowMarkers = true }),
            new("Scatter Line", CreateScatterData(), new PolarScatterLineSeriesView { Color = color, ShowMarkers = true }),
            new("Range Area", CreateRangeData(), new PolarRangeAreaSeriesView { Color = Color.FromArgb(255, 67, 201, 39), Color1 = color, Color2 = color2, ShowMarkers1 = true, ShowMarkers2 = true })
        ];
        selectedView = views[0].View;
        dataAdapter = views[0].DataAdapter;
    }
    ScatterDataAdapter CreateScatterData()
    {
        var points = new List<(double, double)>();
        points.AddRange(CreateFoliumPart(120, 180));
        points.AddRange(CreateFoliumPart(0, 90));
        points.AddRange(CreateFoliumPart(270, 330));
        return new ScatterDataAdapter(points);

        IEnumerable<(double, double)> CreateFoliumPart(int minAngle, int maxAngle)
        {
            for (double i = minAngle; i <= maxAngle; i += 5)
            {
                double angleRad = i * Math.PI / 180;
                double sin = Math.Sin(angleRad);
                double cos = Math.Cos(angleRad);
                double r = 3.0 * sin * cos / (Math.Pow(sin, 3.0) + Math.Pow(cos, 3.0));
                if (r is >= 0 and <= 3)
                    yield return (i, i is > 30 and < 60 ? double.NaN : r);
            }
        }
    }
    NumericRangeDataAdapter CreateRangeData()
    {
        var rangeData = new List<(double, double, double)>();
        for (double i = 0; i <= 360; i += 5)
        {
            bool isEmpty = i is > 60 and < 120 or > 240 and < 300;
            double value1 = isEmpty ? double.NaN : Math.Sin(Math.PI * i / 720);
            double value2 = isEmpty ? double.NaN : Math.Cos(Math.PI * i / 720);
            rangeData.Add((i, value1, value2));
        }
        return new NumericRangeDataAdapter(rangeData);
    }
}
