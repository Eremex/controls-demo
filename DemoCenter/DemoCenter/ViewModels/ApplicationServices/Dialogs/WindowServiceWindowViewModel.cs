using CommunityToolkit.Mvvm.Input;

using DemoCenter.Views.ApplicationServices;

using Eremex.AvaloniaUI.Controls.ApplicationServices;

namespace DemoCenter.ViewModels.ApplicationServices;

/// <summary>
/// Content of a non-modal window shown by IWindowService.
/// A modal dialog opened from here is owned by this window, not by the main one.
/// </summary>
[ViewLocator(typeof(WindowServiceWindowView))]
public class WindowServiceWindowViewModel : WindowAwareViewModel
{
    private readonly IDialogService dialogService;
    private string statusText;

    public WindowServiceWindowViewModel(string title, IDialogService dialogService) : base(title)
    {
        this.dialogService = dialogService;
        ShowDialogCommand = new RelayCommand(ShowDialog);
    }

    public RelayCommand ShowDialogCommand { get; }

    public string StatusText
    {
        get => statusText;
        private set
        {
            if (statusText == value)
                return;

            statusText = value;
            OnPropertyChanged(nameof(StatusText));
        }
    }

    private void ShowDialog()
    {
        var result = dialogService.ShowDialog(
            new DialogServiceOkCancelViewModel("Modal over non-modal", "This window owns the dialog."),
            caption: null);

        StatusText = $"Dialog result: {result}";
    }
}
