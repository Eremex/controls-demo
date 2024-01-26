using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;
using Eremex.AvaloniaUI.Controls.Common;
using Eremex.AvaloniaUI.Controls.DataControl;

namespace DemoCenter.ViewModels
{
    public partial class SegmentedEditorPageViewModel : PageViewModelBase
    {
        [ObservableProperty] GraphicView graphicView;
        [ObservableProperty] string[] viewTypes;
        [ObservableProperty] string selectedViewType;

        public SegmentedEditorPageViewModel()
        {
            GraphicView = GraphicView.Top;
            ViewTypes = new[] { "Primary", "Secondary" };
            SelectedViewType = ViewTypes[0];
        }
    }
}
