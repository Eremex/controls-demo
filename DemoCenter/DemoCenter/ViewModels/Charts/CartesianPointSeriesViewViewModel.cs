using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.ViewModels.DataAdapters;

namespace DemoCenter.ViewModels;

public partial class CartesianPointSeriesViewViewModel : ChartsPageViewModel
{
    [ObservableProperty] private ObservableCollection<SeriesViewModel> series = new()
    {
        new SeriesViewModel { Color = Color.FromArgb(64, 189, 20, 54), DataAdapter = new ClusterDataAdapter(2000, -1000, 2000, -1000, 10000) },
        new SeriesViewModel { Color = Color.FromArgb(64, 0, 120, 122), DataAdapter = new ClusterDataAdapter(1000, -2000, 1000, -2000, 10000) }
    };
}