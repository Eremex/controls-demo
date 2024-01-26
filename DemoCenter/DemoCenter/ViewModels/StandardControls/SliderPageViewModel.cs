using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.ViewModels
{
    public partial class SliderPageViewModel : PageViewModelBase
    {
        [ObservableProperty] TickPlacement tickPlacement;

        public SliderPageViewModel()
        {

        }
    }
}
