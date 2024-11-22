using CommunityToolkit.Mvvm.ComponentModel;

namespace DemoCenter.ViewModels;

public partial class RibbonGroupViewModel : PageViewModelBase
{
    [ObservableProperty] string message;

    public RibbonGroupViewModel()
    {
        Message = GetType().FullName;
    }
}