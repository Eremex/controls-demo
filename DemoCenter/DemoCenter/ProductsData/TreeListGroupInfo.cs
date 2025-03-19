using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.ProductsData
{
    public static class TreeListGroupInfo
    {
        internal static List<PageInfo> Create()
        {
            return new List<PageInfo>()
            {
                new PageInfo(name: "Filter & Search", title: "Filter & Search",
                description: Resources.TheTreeListSAutoFilterRowDisplayedAtTheTop,
                viewModelGetter: () => new TreeListFilteringPageViewModel(),
                showInWeb: false),

                new PageInfo(name: "Data Editors", title: "Data Editors",
                description: Resources.EremexEditorsAreUsedInTreeListCellsByDefau,
                viewModelGetter: () => new TreeListDataEditorsPageViewModel(),
                showInWeb: false),

                new PageInfo(name: "Folder Browser", title: "Folder Browser",
                description: Resources.YouCanBindTreeListToAHierarchicalDataSourc,
                viewModelGetter: () => new FolderBrowserPageViewModel()),

                new PageInfo(name: "Multiple Node Selection", title: "Multiple Node Selection",
                description: Resources.TheTreeListAndTreeViewControlsSupportMulti,
                viewModelGetter: () => new TreeListMultipleSelectionPageViewModel(), new VersionInfo(1, 0)),                
            };
        }
    }
}