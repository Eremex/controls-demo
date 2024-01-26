using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Common;

namespace DemoCenter.ViewModels
{
    public partial class ToolbarAndMenuPageViewModel : PageViewModelBase
    {
        [ObservableProperty] string message;

        public ToolbarAndMenuPageViewModel()
        {
            Message = GetType().FullName;
        }
    }
}
