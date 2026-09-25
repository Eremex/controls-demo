using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Eremex.AvaloniaUI.Controls;
using Eremex.AvaloniaUI.Controls.ApplicationServices;

namespace DemoCenter.ViewModels.ApplicationServices;

/// <summary>
/// <see cref="IMessageBoxService"/>: standard message boxes with a fixed set of buttons.
/// </summary>
public partial class MessageBoxServicePageViewModel : ApplicationServicesPageViewModelBase
{
    [ObservableProperty]
    private string messageText = "The document has unsaved changes.";

    [ObservableProperty]
    private string captionText = "Application Services";

    /// <summary>
    /// The simplest case: a message and a single Ok button.
    /// </summary>
    [RelayCommand]
    private void ShowInformation()
    {
        var result = Service<IMessageBoxService>()
            .Show(MessageText, CaptionText, MessageBoxButtons.Ok, MessageBoxIcon.Information);

        Report("Ok / Information", result.ToString());
    }

    /// <summary>
    /// A question the user can decline: the result tells the two apart.
    /// </summary>
    [RelayCommand]
    private void ShowConfirmation()
    {
        var result = Service<IMessageBoxService>()
            .Show(MessageText, CaptionText, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        Report("Yes / No", result.ToString());
    }

    /// <summary>
    /// Three-way choice, still from the fixed set of standard buttons.
    /// </summary>
    [RelayCommand]
    private void ShowThreeWay()
    {
        var result = Service<IMessageBoxService>()
            .Show(MessageText, CaptionText, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

        Report("Yes / No / Cancel", result.ToString());
    }

    /// <summary>
    /// An error report with retry semantics.
    /// </summary>
    [RelayCommand]
    private void ShowError()
    {
        var result = Service<IMessageBoxService>()
            .Show(MessageText, CaptionText, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

        Report("Retry / Cancel", result.ToString());
    }

    /// <summary>
    /// The owner window can be passed explicitly. When it is omitted, the service asks
    /// IWindowsManager for the active window, which is what every other button here does.
    /// </summary>
    [RelayCommand]
    private void ShowOverActiveWindow()
    {
        var owner = Service<IWindowsManager>().ActiveWindow;
        var result = Service<IMessageBoxService>()
            .Show(owner, MessageText, CaptionText, MessageBoxButtons.OkCancel, MessageBoxIcon.None);

        Report("Explicit owner", $"{result} (owner: {owner?.Title ?? "none"})");
    }
}
