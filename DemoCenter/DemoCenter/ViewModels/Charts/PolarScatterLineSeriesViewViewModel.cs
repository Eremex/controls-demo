using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class PolarScatterLineSeriesViewViewModel : ChartsPageViewModel
{
    static IEnumerable<(double, double)> CreateFoliumPart(int minAngle, int maxAngle)
    {
        for (int i = minAngle; i <= maxAngle; i+=5) {
            double angleRad = i * Math.PI / 180;
            double sin = Math.Sin(angleRad);
            double cos = Math.Cos(angleRad);
            double r = 3.0 * sin * cos / (Math.Pow(sin, 3.0) + Math.Pow(cos, 3.0));
            if (r is >= 0 and <= 3)
                yield return (i, r);
        }
    }
    static ScatterDataAdapter CreateFolium()
    {
        var points = new List<(double, double)>();
        points.AddRange(CreateFoliumPart(120, 180));
        points.AddRange(CreateFoliumPart(0, 90));
        points.AddRange(CreateFoliumPart(270, 330));
        return new ScatterDataAdapter(points);
    }

    [ObservableProperty] ObservableCollection<SeriesViewModel> series = new()
    {
        new SeriesViewModel { Color = Color.FromArgb(255, 189, 20, 54), DataAdapter = CreateFolium() }
    };
}
