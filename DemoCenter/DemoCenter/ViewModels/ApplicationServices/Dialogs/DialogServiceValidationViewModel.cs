using DemoCenter.Views.ApplicationServices;

using Eremex.AvaloniaUI.Controls.ApplicationServices;

namespace DemoCenter.ViewModels.ApplicationServices;

/// <summary>
/// IDialogService with validation: Ok stays disabled until the field is filled in.
/// The view is picked by <see cref="ViewLocatorAttribute"/>, so the view model does not
/// need to know how it is displayed.
/// </summary>
/// <remarks>
/// The property is written by hand: dialog view models derive from the Eremex base class
/// rather than the CommunityToolkit ObservableObject, so the [ObservableProperty] generator
/// does not apply to them.
/// </remarks>
[ViewLocator(typeof(DialogServiceValidationView))]
public class DialogServiceValidationViewModel : OkCancelDialogAwareViewModel
{
    private string userName = string.Empty;

    public DialogServiceValidationViewModel(string title) : base(title)
    {
    }

    public string UserName
    {
        get => userName;
        set
        {
            if (userName == value)
                return;

            userName = value;
            OnPropertyChanged(nameof(UserName));
            OkCommand.NotifyCanExecuteChanged();
        }
    }

    public override bool CanOk(object parameter) => !string.IsNullOrWhiteSpace(UserName);
}
