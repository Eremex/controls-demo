using CommunityToolkit.Mvvm.Input;

using Eremex.AvaloniaUI.Controls.ApplicationServices;

namespace DemoCenter.ViewModels.ApplicationServices;

/// <summary>
/// IDialogService with a custom button set: Apply takes the place of Ok.
/// </summary>
public partial class DialogServiceApplyViewModel : OkCancelDialogAwareViewModel
{
    public DialogServiceApplyViewModel(string title, string message) : base(title)
    {
        Content = message;
    }

    [RelayCommand]
    public void Apply(object parameter) => CloseWindow(DialogResult.Ok);

    protected override IDialogButtonViewModel[] CreateButtons() => new[]
    {
        DialogButtonViewModel.CreateApply(ApplyCommand),
        DialogButtonViewModel.CreateCancel(CancelCommand),
    };
}
