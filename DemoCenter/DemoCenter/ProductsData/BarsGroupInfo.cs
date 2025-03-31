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
                    viewModelGetter: () => new IdeLayoutPageViewModel(),
                    descriptionGetter: () => Resources.TheDockManagerComponentAllowsYouToImplemen, showInWeb: false),

                new PageInfo(name: "Toolbar & Menu", title: "Toolbar & Menu",
                viewModelGetter: () => new ToolbarAndMenuPageViewModel(), descriptionGetter: () => Resources.TheToolbarManagerComponentAllowsYouToImple),

                //new PageInfo(name: "Bar Items", title: "Bar Items",
                //description : "",
                //viewModelGetter: () => new BarItemsPageViewModel()),

                new PageInfo(name: "Context Menu", title: "Context Menu",
                viewModelGetter: () => new ContextMenuPageViewModel(), descriptionGetter: () => Resources.TheToolbarsMenuLibraryContainsAPopupMenuCo),
            };
        }
    }
}