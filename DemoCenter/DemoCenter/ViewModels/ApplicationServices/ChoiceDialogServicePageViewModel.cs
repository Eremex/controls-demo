using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Eremex.AvaloniaUI.Controls.ApplicationServices;

namespace DemoCenter.ViewModels.ApplicationServices;

/// <summary>
/// <see cref="IChoiceDialogService"/>: a dialog whose buttons are defined by application code.
/// </summary>
/// <remarks>
/// IMessageBoxService only offers the fixed sets (Ok, OkCancel, YesNo, AbortRetryIgnore,
/// RetryCancel) and always returns DialogResult. This service takes arbitrary captions and
/// returns whatever type the caller chose.
/// </remarks>
public partial class ChoiceDialogServicePageViewModel : ApplicationServicesPageViewModelBase
{
    [ObservableProperty]
    private string dialogTitle = "Activation failed";

    [ObservableProperty]
    private string dialogMessage = "The license server did not respond.";

    /// <summary>
    /// The classic case this service exists for: captions that no standard set provides.
    /// </summary>
    [RelayCommand]
    private void ShowCustomCaptions()
    {
        var choice = Service<IChoiceDialogService>().Show(
            DialogMessage,
            DialogTitle,
            new[]
            {
                new DialogChoice<string>("Show log", "log") { IsDefault = true },
                new DialogChoice<string>("Retry", "retry"),
                new DialogChoice<string>("Close", "close") { IsCancel = true },
            });

        Report("Custom captions", choice is null ? "closed without a choice" : choice);
    }

    /// <summary>
    /// The result type is up to the caller: here the answer is a plain bool.
    /// </summary>
    [RelayCommand]
    private void ShowBooleanResult()
    {
        var openLog = Service<IChoiceDialogService>().Show(
            DialogMessage,
            DialogTitle,
            new[]
            {
                new DialogChoice<bool>("Show log", true) { IsDefault = true },
                new DialogChoice<bool>("Close", false) { IsCancel = true },
            });

        Report("Boolean result", openLog ? "user asked for the log" : "user closed the dialog");
    }

    /// <summary>
    /// Any struct works, including an enum.
    /// </summary>
    [RelayCommand]
    private void ShowEnumResult()
    {
        var choice = Service<IChoiceDialogService>().Show(
            DialogMessage,
            DialogTitle,
            new[]
            {
                new DialogChoice<DialogResult>("Abort", DialogResult.Abort) { IsCancel = true },
                new DialogChoice<DialogResult>("Retry", DialogResult.Retry) { IsDefault = true },
                new DialogChoice<DialogResult>("Ignore", DialogResult.Ignore),
            });

        Report("Enum result", choice.ToString());
    }

    /// <summary>
    /// Placement is per choice, so secondary actions can sit apart from the main ones.
    /// </summary>
    [RelayCommand]
    private void ShowDockedChoices()
    {
        var choice = Service<IChoiceDialogService>().Show(
            DialogMessage,
            DialogTitle,
            new[]
            {
                new DialogChoice<string>("Help", "help") { Dock = DockType.Left },
                new DialogChoice<string>("Retry", "retry") { IsDefault = true },
                new DialogChoice<string>("Cancel", "cancel") { IsCancel = true },
            });

        Report("Docked choices", choice is null ? "closed without a choice" : choice);
    }
}
