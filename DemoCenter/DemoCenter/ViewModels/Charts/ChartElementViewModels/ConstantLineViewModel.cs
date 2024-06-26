using CommunityToolkit.Mvvm.ComponentModel;

namespace DemoCenter.ViewModels;

public partial class ConstantLineViewModel : ObservableObject
{
    [ObservableProperty] string title;
    [ObservableProperty] object axisValue;
}