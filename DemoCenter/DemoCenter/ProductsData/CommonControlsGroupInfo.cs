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
                    viewModelGetter: () => new TabControlPageViewModel(), descriptionGetter: () => Resources.EremexTabControlCanOrganizeTheContentsOfAB),
                
                new PageInfo(name: "MessageBox", title: "MessageBox",
                    viewModelGetter: () => new MessageBoxPageViewModel(),
                    descriptionGetter: () => Resources.TheMxMessageBoxDialogAllowsYouToDisplayMes,
                    introduced: new VersionInfo(1, 1), showInWeb: false),

                new PageInfo(name: "SplitContainerControl", title: "SplitContainerControl",
                    viewModelGetter: () => new SplitContainerControlPageViewModel(), descriptionGetter: () => Resources.SplitContainerControlAllowsYouToPlaceConte),
            };
        }
    }
}