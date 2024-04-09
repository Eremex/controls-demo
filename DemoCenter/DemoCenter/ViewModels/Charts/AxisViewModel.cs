using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.Avalonia.Charts;

namespace DemoCenter.ViewModels;

public partial class AxisViewModel : ObservableObject
{
    [ObservableProperty] AxisPosition position;
    [ObservableProperty] string title;
    [ObservableProperty] bool showTitle = true;
    [ObservableProperty] bool showLabels = true;
    [ObservableProperty] bool showInterlacing;
    [ObservableProperty] bool? showMajorGridlines = false;
    [ObservableProperty] bool? showMinorGridlines = false;
    [ObservableProperty] int minorCount = 3;

    public override string ToString() => Title;
}
