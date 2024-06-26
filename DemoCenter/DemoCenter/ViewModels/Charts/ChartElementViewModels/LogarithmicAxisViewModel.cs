using CommunityToolkit.Mvvm.ComponentModel;

namespace DemoCenter.ViewModels;

public partial class LogarithmicAxisViewModel : ObservableObject
{
    [ObservableProperty] double? logarithmicBase;
}
