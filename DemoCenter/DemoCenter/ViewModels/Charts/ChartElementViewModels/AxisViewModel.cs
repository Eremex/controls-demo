using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class AxisViewModel : ObservableObject
{
    [ObservableProperty] AxisPosition position;
    [ObservableProperty] string title;
    [ObservableProperty] string key;
    [ObservableProperty] bool showTitle = true;
    [ObservableProperty] bool showLabels = true;
    [ObservableProperty] bool showInterlacing;
    [ObservableProperty] bool? showMajorGridlines = false;
    [ObservableProperty] bool? showMinorGridlines = false;
    [ObservableProperty] int minorCount = 3;
    [ObservableProperty] Color color;

    public override string ToString() => Title;
}
