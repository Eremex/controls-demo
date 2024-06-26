using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.ViewModels.DataAdapters;

namespace DemoCenter.ViewModels;

public partial class PolarScatterLineSeriesViewViewModel : ChartsPageViewModel
{
    static IEnumerable<Point> CreateFoliumPart(int minAngle, int maxAngle)
    {
        for (int i = minAngle; i <= maxAngle; i+=5) {
            double angleRad = i * Math.PI / 180;
            double sin = Math.Sin(angleRad);
            double cos = Math.Cos(angleRad);
            double r = 3.0 * sin * cos / (Math.Pow(sin, 3.0) + Math.Pow(cos, 3.0));
            if (r is >= 0 and <= 3)
                yield return new Point(i, r);
        }
    }
    static ScatterPointsDataAdapter CreateFolium()
    {
        var points = new List<Point>();
        points.AddRange(CreateFoliumPart(120, 180));
        points.AddRange(CreateFoliumPart(0, 90));
        points.AddRange(CreateFoliumPart(270, 330));
        return new ScatterPointsDataAdapter(points);
    }

    [ObservableProperty] ObservableCollection<SeriesViewModel> series = new()
    {
        new SeriesViewModel { Color = Color.FromArgb(255, 189, 20, 54), DataAdapter = CreateFolium() }
    };
}
