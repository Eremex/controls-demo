using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.Common;

namespace DemoCenter.ProductsData
{
    public static class BarsGroupInfo
    {
        internal static List<PageInfo> Create()
        {
            return new List<PageInfo>()
            {
                //new PageInfo(name: "Overview", title: "Overview",
                //description: "Bars overview sample description",
                //viewModelGetter: () => new BarsOverviewPageViewModel()),

                new PageInfo(name: "IDE Layout", title: "IDE Layout",
                    description: "The Dock Manager component allows you to implement the classic docking UI found in popular IDEs. You can create tool panels that support dock, auto-hide, and float operations. Special Document containers are designed to display the main content of your window. You can create multiple Documents and organize them into a tabbed UI.",
                    viewModelGetter: () => new IdeLayoutPageViewModel()),

                new PageInfo(name: "Toolbar & Menu", title: "Toolbar & Menu",
                description: string.Empty,
                viewModelGetter: () => new ToolbarAndMenuPageViewModel()),

                //new PageInfo(name: "Bar Items", title: "Bar Items",
                //description : "",
                //viewModelGetter: () => new BarItemsPageViewModel()),

                new PageInfo(name: "Context Menu", title: "Context Menu",
                description : string.Empty,
                viewModelGetter: () => new ContextMenuPageViewModel()),
            };
        }
    }
}