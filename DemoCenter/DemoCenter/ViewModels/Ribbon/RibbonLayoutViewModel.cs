using CommunityToolkit.Mvvm.ComponentModel;

namespace DemoCenter.ViewModels;

public partial class RibbonLayoutViewModel : PageViewModelBase
{
    [ObservableProperty]
    double ribbonWidth = 900;
}
