using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;
using Eremex.AvaloniaUI.Controls.Common;

namespace DemoCenter.ViewModels;

public partial class ViewViewModel : ViewModelBase
{
    [ObservableProperty] string name;
    [ObservableProperty] SeriesViewBase view;
    [ObservableProperty] ISeriesDataAdapter dataAdapter;

    public ViewViewModel(string name, ISeriesDataAdapter adapter, SeriesViewBase view)
    {
        Name = name;
        View = view;
        DataAdapter = adapter;
    }
}
