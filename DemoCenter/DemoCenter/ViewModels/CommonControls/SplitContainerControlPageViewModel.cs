using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Common;
using Eremex.AvaloniaUI.Controls.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoCenter.DemoData;

namespace DemoCenter.ViewModels
{
    public partial class SplitContainerControlPageViewModel : PageViewModelBase
    {
        public IList<CarInfo> Cars { get; } = CsvSources.Cars;
    }
}
