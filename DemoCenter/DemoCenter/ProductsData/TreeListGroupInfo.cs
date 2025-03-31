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
                viewModelGetter: () => new TreeListFilteringPageViewModel(),
                descriptionGetter: () => Resources.TheTreeListSAutoFilterRowDisplayedAtTheTop, showInWeb: false),

                new PageInfo(name: "Data Editors", title: "Data Editors",
                viewModelGetter: () => new TreeListDataEditorsPageViewModel(),
                descriptionGetter: () => Resources.EremexEditorsAreUsedInTreeListCellsByDefau, showInWeb: false),

                new PageInfo(name: "Folder Browser", title: "Folder Browser",
                viewModelGetter: () => new FolderBrowserPageViewModel(), descriptionGetter: () => Resources.YouCanBindTreeListToAHierarchicalDataSourc),

                new PageInfo(name: "Multiple Node Selection", title: "Multiple Node Selection",
                viewModelGetter: () => new TreeListMultipleSelectionPageViewModel(), descriptionGetter: () => Resources.TheTreeListAndTreeViewControlsSupportMulti, introduced: new VersionInfo(1, 0)),                
            };
        }
    }
}