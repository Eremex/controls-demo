using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Eremex.AvaloniaUI.Controls.ApplicationServices;

namespace DemoCenter.ViewModels.ApplicationServices;

/// <summary>
/// <see cref="IWindowService"/>: non-modal windows, and <see cref="IWindowsManager"/>,
/// which tells every other service which window is currently active.
/// </summary>
public partial class WindowServicePageViewModel : ApplicationServicesPageViewModelBase
{
    [ObservableProperty]
    private string windowTitle = "Non-modal window";

    /// <summary>
    /// The window stays open and the demo remains usable.
    /// </summary>
    [RelayCommand]
    private void ShowWindow()
    {
        Service<IWindowService>().Show(
            new WindowServiceWindowViewModel(WindowTitle, Service<IDialogService>()));

        Report("Show", "window opened, the demo stays usable");
    }

    /// <summary>
    /// The caption passed to the service wins over the one on the view model.
    /// </summary>
    [RelayCommand]
    private void ShowWindowWithCaption()
    {
        Service<IWindowService>().Show(
            new WindowServiceWindowViewModel(WindowTitle, Service<IDialogService>()),
            "Caption supplied by the service");

        Report("Show with caption", "the service caption overrides the view model title");
    }

    /// <summary>
    /// Two windows at once: opening a modal dialog from one of them blocks that window only.
    /// </summary>
    [RelayCommand]
    private void ShowTwoWindows()
    {
        var windowService = Service<IWindowService>();
        var dialogService = Service<IDialogService>();

        windowService.Show(new WindowServiceWindowViewModel($"{WindowTitle} 1", dialogService));
        windowService.Show(new WindowServiceWindowViewModel($"{WindowTitle} 2", dialogService));

        Report("Two windows", "open a dialog from either one to see which window it blocks");
    }

    /// <summary>
    /// IWindowsManager tracks the active window; every service falls back to it
    /// when no owner is given.
    /// </summary>
    [RelayCommand]
    private void ReportActiveWindow()
    {
        var active = Service<IWindowsManager>().ActiveWindow;
        Report("Active window", active?.Title ?? "none");
    }
}
