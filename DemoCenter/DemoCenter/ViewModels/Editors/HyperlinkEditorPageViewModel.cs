using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DemoCenter.DemoData;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Common;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DemoCenter.ViewModels
{
    public partial class HyperlinkEditorPageViewModel : PageViewModelBase
    {
        [ObservableProperty] IEnumerable<YachtInfo> yachts;

        public HyperlinkEditorPageViewModel()
        {
            Yachts = CsvSources.Yachts.Take(new Range(4, 9));
        }

        public event Action<string> SelectDemo;

        [RelayCommand]
        public void ShowWebPage(string parameter)
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    Process.Start(new ProcessStartInfo(parameter) { UseShellExecute = true });
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    Process.Start("xdg-open", parameter);
            }
            catch { };
        }

        [RelayCommand]
        public void ShowDemo(string moduleName)
        {
            SelectDemo?.Invoke(moduleName);
        }
    }
}
