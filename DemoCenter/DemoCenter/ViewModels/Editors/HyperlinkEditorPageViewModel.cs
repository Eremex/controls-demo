using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DemoCenter.DemoData;

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
        public void ShowDemo(string moduleName)
        {
            SelectDemo?.Invoke(moduleName);
        }
    }
}
