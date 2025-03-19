using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.ProductsData
{
    public static class CommonControlsGroupInfo
    {
        internal static List<PageInfo> Create()
        {
            return new List<PageInfo>()
            {
                new PageInfo(name: "TabControl", title: "TabControl",
                    description: Resources.EremexTabControlCanOrganizeTheContentsOfAB,
                    viewModelGetter: () => new TabControlPageViewModel()),
                
                new PageInfo(name: "MessageBox", title: "MessageBox",
                    description: Resources.TheMxMessageBoxDialogAllowsYouToDisplayMes,
                    viewModelGetter: () => new MessageBoxPageViewModel(),
                    introduced: new VersionInfo(1, 1),
                    showInWeb: false),

                new PageInfo(name: "SplitContainerControl", title: "SplitContainerControl",
                    description: Resources.SplitContainerControlAllowsYouToPlaceConte,
                    viewModelGetter: () => new SplitContainerControlPageViewModel()),
            };
        }
    }
}