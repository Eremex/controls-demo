using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.ViewModels.DataAdapters;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class SmithLineSeriesViewViewModel : ChartsPageViewModel
{
    [ObservableProperty] ObservableCollection<SeriesViewModel> series = new()
    {
        new SeriesViewModel { Color = Color.FromArgb(255, 189, 20, 54), DataAdapter = new SmithSampleDataAdapter()},
    };
}
