using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Common;

namespace DemoCenter.ViewModels
{
    public partial class ProgressBarPageViewModel : PageViewModelBase
    {
        [ObservableProperty] string [] formats = new[] { "{1:0}%", "{1:0}", "{1:N2}" } ;
        [ObservableProperty] string selectedTextFormat;

        public ProgressBarPageViewModel()
        {
            SelectedTextFormat = formats[0];
        }
    }
}
