using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Eremex.AvaloniaUI.Controls.ApplicationServices;

namespace DemoCenter.ViewModels.ApplicationServices;

/// <summary>
/// <see cref="IOpenFileDialogService"/> and <see cref="ISaveFileDialogService"/>.
/// </summary>
/// <remarks>
/// Both services expose a synchronous and an asynchronous variant. The synchronous one
/// holds the calling thread on a nested message loop and exists for legacy code;
/// new code should prefer ShowAsync.
/// </remarks>
public partial class FileDialogServicePageViewModel : ApplicationServicesPageViewModelBase
{
    private const string TextFilter = "Text files|*.txt|All files|*.*";

    [ObservableProperty]
    private string dialogTitle = "Application Services";

    /// <summary>
    /// Selected paths from the last open dialog.
    /// </summary>
    public ObservableCollection<string> SelectedFiles { get; } = new();

    /// <summary>
    /// The recommended way to show an open dialog.
    /// </summary>
    [RelayCommand]
    private async Task OpenFileAsync()
    {
        var files = await Service<IOpenFileDialogService>().ShowAsync(options: new OpenFileDialogOptions
        {
            Title = DialogTitle,
            AllowMultiple = false,
            Filter = TextFilter,
        });

        UpdateSelection("Open (async)", files);
    }

    /// <summary>
    /// AllowMultiple lets the user pick more than one file.
    /// </summary>
    [RelayCommand]
    private async Task OpenMultipleFilesAsync()
    {
        var files = await Service<IOpenFileDialogService>().ShowAsync(options: new OpenFileDialogOptions
        {
            Title = DialogTitle,
            AllowMultiple = true,
            Filter = TextFilter,
        });

        UpdateSelection("Open multiple (async)", files);
    }

    /// <summary>
    /// The synchronous variant returns the result directly, without awaiting.
    /// </summary>
    [RelayCommand]
    private void OpenFileSync()
    {
        var files = Service<IOpenFileDialogService>().Show(options: new OpenFileDialogOptions
        {
            Title = DialogTitle,
            Filter = TextFilter,
        });

        UpdateSelection("Open (sync)", files);
    }

    /// <summary>
    /// A save dialog with a suggested name, a default extension and the overwrite prompt.
    /// </summary>
    [RelayCommand]
    private async Task SaveFileAsync()
    {
        var path = await Service<ISaveFileDialogService>().ShowAsync(options: new SaveFileDialogOptions
        {
            Title = DialogTitle,
            Filter = TextFilter,
            DefaultExtension = "txt",
            InitialFileName = "document.txt",
            ShowOverwritePrompt = true,
        });

        Report("Save (async)", path ?? "cancelled");
    }

    /// <summary>
    /// The dialog can start in a well-known folder instead of a hard-coded path.
    /// </summary>
    [RelayCommand]
    private async Task SaveToDocumentsAsync()
    {
        var path = await Service<ISaveFileDialogService>().ShowAsync(options: new SaveFileDialogOptions
        {
            Title = DialogTitle,
            DefaultWellKnownFolder = FileDialogFolder.Documents,
            DefaultExtension = "txt",
            InitialFileName = "report.txt",
        });

        Report("Save to Documents", path ?? "cancelled");
    }

    private void UpdateSelection(string scenario, string[] files)
    {
        SelectedFiles.Clear();
        foreach (var file in files)
            SelectedFiles.Add(file);

        Report(scenario, files.Length == 0 ? "cancelled" : $"{files.Length} file(s) selected");
    }
}
