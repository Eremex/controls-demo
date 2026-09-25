using CommunityToolkit.Mvvm.Input;

using Eremex.AvaloniaUI.Controls.ApplicationServices;

namespace DemoCenter.ViewModels.ApplicationServices;

/// <summary>
/// IDialogService with custom captions and explicit placement:
/// No is docked to the left, Yes stays on the right.
/// </summary>
public partial class DialogServiceYesNoViewModel : OkCancelDialogAwareViewModel
{
    public DialogServiceYesNoViewModel(string title, string message) : base(title)
    {
        Content = message;
    }

    [RelayCommand]
    public void Yes(object parameter) => CloseWindow(DialogResult.Yes);

    [RelayCommand]
    public void No(object parameter) => CloseWindow(DialogResult.No);

    protected override IDialogButtonViewModel[] CreateButtons() => new[]
    {
        DialogButtonViewModel.CreateCustom(NoCommand, "No", dockType: DockType.Left),
        DialogButtonViewModel.CreateCustom(YesCommand, "Yes"),
    };
}
