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
                    description : "Eremex Tab Control can organize the contents of a bound item source into a tabbed UI. It supports tab re-ordering, multiple tab layout modes, and built-in buttons to add and close tabs.",
                    viewModelGetter: () => new TabControlPageViewModel()),
                
                new PageInfo(name: "SplitContainerControl", title: "SplitContainerControl",
                description : "Split Container Control allows you to place content onto two panels, and separate the panels with a splitter that a user can drag to resize the panels. With the panel collapse feature enabled, the user can click the splitter to collapse and restore a panel.",
                viewModelGetter: () => new SplitContainerControlPageViewModel()),
            };
        }
    }
}