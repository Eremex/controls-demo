using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Eremex.AvaloniaUI.Controls.Common;
using Eremex.AvaloniaUI.Controls.Editors;

namespace DemoCenter.ViewModels
{
    public partial class TextEditingPageViewModel : PageViewModelBase
    {
        [ObservableProperty] string logContent = string.Empty;
        [ObservableProperty] string selectedString = string.Empty;
        [ObservableProperty, NotifyCanExecuteChangedFor(nameof(ShowPreviousCommand)), NotifyCanExecuteChangedFor(nameof(ShowNextCommand))] string selectedEditor;
        [ObservableProperty] bool showError;
        [ObservableProperty] ValidationInfo validationInfo;

        List<string> editors;

        public TextEditingPageViewModel()
        {
            editors = typeof(BaseEditor).Assembly
                .GetTypes()
                .Where(x => x.IsSubclassOf(typeof(BaseEditor)))
                .Select(x => x.Name)
                .ToList();
            SelectedEditor = "TextEditor";
            ShowError = true;
        }

        [RelayCommand]
        public void AddLogLine(string parameter)
        {
            LogContent += $"{parameter} button click!" + Environment.NewLine;
        }

        [RelayCommand(CanExecute = nameof(CanShowPrevious))]
        public void ShowPrevious(string parameter) => UpdateSelectedEditor(parameter, -1);

        public bool CanShowPrevious() => SelectedEditor != editors.First();

        [RelayCommand(CanExecute = nameof(CanShowNext))]
        public void ShowNext(string parameter) => UpdateSelectedEditor(parameter, 1);

        public bool CanShowNext() => SelectedEditor != editors.Last();

        void UpdateSelectedEditor(string parameter, int increment)
        {
            var index = editors.IndexOf(SelectedEditor);
            SelectedEditor = editors[index + increment];
            AddLogLine(parameter);
        }

        [RelayCommand]
        public void ClearLog() => LogContent = string.Empty;

        partial void OnShowErrorChanged(bool value)
        {
            ValidationInfo = value ? new ValidationInfo("Incorrect text!") : null;
        }
    }
}
