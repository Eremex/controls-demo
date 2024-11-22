using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Layout;
using Eremex.AvaloniaUI.Controls;

namespace DemoCenter.ViewModels
{
    public partial class MessageBoxPageViewModel : PageViewModelBase
    {
        [ObservableProperty] private string title = "MxMessageBox Title";
        [ObservableProperty] private string text = "MxMessageBox can display a text message, an icon and a set of standard buttons.";
        [ObservableProperty] private MessageBoxButtons buttons = MessageBoxButtons.OkCancel;
        [ObservableProperty] private MessageBoxIcon icon = MessageBoxIcon.Information;
        [ObservableProperty] private MessageBoxResult result = MessageBoxResult.Ok;
        [ObservableProperty] private HorizontalAlignment buttonAlignment = HorizontalAlignment.Right;
    }
}