using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoCenter.DemoData;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Common;

namespace DemoCenter.ViewModels
{
    public partial class SpinEditorPageViewModel : PageViewModelBase
    {
        [ObservableProperty] IEnumerable<YachtInfo> yachts;
        public SpinEditorPageViewModel()
        {
            Yachts = CsvSources.Yachts.Take(4);
        }
    }
}
