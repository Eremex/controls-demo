using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Eremex.AvaloniaUI.Controls.ApplicationServices;

namespace DemoCenter.ViewModels.ApplicationServices;

/// <summary>
/// <see cref="IDialogService"/>: modal dialogs driven by a view model.
/// </summary>
public partial class DialogServicePageViewModel : ApplicationServicesPageViewModelBase
{
    [ObservableProperty]
    private string dialogTitle = "Application Services";

    [ObservableProperty]
    private string dialogMessage = "A dialog view model supplies the title, the content and the buttons.";

    /// <summary>
    /// The base class creates Ok and Cancel; the view model adds nothing.
    /// </summary>
    [RelayCommand]
    private void ShowOkCancel()
        => Show("Ok / Cancel", new DialogServiceOkCancelViewModel(DialogTitle, DialogMessage));

    /// <summary>
    /// Overriding CreateButtons replaces the standard set.
    /// </summary>
    [RelayCommand]
    private void ShowApplyCancel()
        => Show("Apply / Cancel", new DialogServiceApplyViewModel(DialogTitle, DialogMessage));

    /// <summary>
    /// Buttons can carry custom captions and be docked to either side.
    /// </summary>
    [RelayCommand]
    private void ShowYesNo()
        => Show("Yes / No with Dock", new DialogServiceYesNoViewModel(DialogTitle, DialogMessage));

    /// <summary>
    /// CanOk keeps the default button disabled until the input is valid.
    /// </summary>
    [RelayCommand]
    private void ShowValidation()
        => Show("Validation (CanOk)", new DialogServiceValidationViewModel(DialogTitle));

    /// <summary>
    /// A modal dialog opened from another modal dialog.
    /// </summary>
    [RelayCommand]
    private void ShowNested()
        => Show("Nested modal", new DialogServiceNestedViewModel(DialogTitle, Service<IDialogService>()));

    private void Show<T>(string scenario, T viewModel)
        where T : IDialogAwareViewModel
    {
        var result = Service<IDialogService>().ShowDialog(viewModel, caption: null);
        Report(scenario, result.ToString());
    }
}
