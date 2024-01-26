using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Common;
using Eremex.AvaloniaUI.Controls.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.ViewModels
{
    public partial class SplitContainerControlPageViewModel : PageViewModelBase
    {
        [ObservableProperty] SplitContainerControlCollapsePanel collapsedPanel;

        public SplitContainerControlPageViewModel()
        {
            CollapsedPanel = SplitContainerControlCollapsePanel.Panel2;
        }
    }
}
