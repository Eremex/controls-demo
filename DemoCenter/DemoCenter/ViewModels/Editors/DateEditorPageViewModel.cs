using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;
using Eremex.AvaloniaUI.Controls.Editors;

namespace DemoCenter.ViewModels
{
    public partial class DateEditorPageViewModel : PageViewModelBase
    {
        [ObservableProperty] bool showNullButton;
        [ObservableProperty] ComponentPlacement nullValueButtonPosition;

        [ObservableProperty] DateTime? current;
        [ObservableProperty] DateTime? minimum;
        [ObservableProperty] DateTime? maximum;

        [ObservableProperty] string[] formats = new[] { "d", "D", "MMMM dd" };
        [ObservableProperty] string selectedTextFormat;

        public DateEditorPageViewModel()
        {

            ShowNullButton = true;
            Current = DateTime.Now.AddDays(4);
            Maximum = DateTime.Now.AddMonths(4);
            SelectedTextFormat = formats[0];
        }

        partial void OnShowNullButtonChanged(bool value) => NullValueButtonPosition = value ? ComponentPlacement.Popup : ComponentPlacement.None;
    }
}
