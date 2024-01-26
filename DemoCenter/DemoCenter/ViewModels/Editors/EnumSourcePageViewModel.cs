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
    public partial class EnumSourcePageViewModel : PageViewModelBase
    {
        [ObservableProperty] GraphicPosition graphicPosition;
        [ObservableProperty] GraphicView graphicView;
        [ObservableProperty] EnumMembersSortMode sortMode;

        //public event Action UpdateEnumSources;

        public EnumSourcePageViewModel()
        {
            SortMode = EnumMembersSortMode.Default;
            GraphicPosition = GraphicPosition.Maximum;
            GraphicView = GraphicView.Top;
        }
    }
}
