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
                descriptionGetter: () => Resources.TheTreeListSAutoFilterRowDisplayedAtTheTop, showInWeb: false, updated: new VersionInfo(1, 3)),

                new PageInfo(name: "Data Editors", title: "Data Editors",
                viewModelGetter: () => new TreeListDataEditorsPageViewModel(),
                descriptionGetter: () => Resources.EremexEditorsAreUsedInTreeListCellsByDefau, showInWeb: false),

                new PageInfo(name: "Folder Browser", title: "Folder Browser",
                viewModelGetter: () => new FolderBrowserPageViewModel(), descriptionGetter: () => Resources.YouCanBindTreeListToAHierarchicalDataSourc),

                new PageInfo(name: "Multiple Node Selection", title: "Multiple Node Selection",
                viewModelGetter: () => new TreeListMultipleSelectionPageViewModel(), descriptionGetter: () => Resources.TheTreeListAndTreeViewControlsSupportMulti, introduced: new VersionInfo(1, 0)),

                new PageInfo(name: "Column Bands", title: "Column Bands",
                viewModelGetter: () => new TreeListColumnBandsViewModel(), descriptionGetter: () => Resources.TreeListColumnBandsDescription, introduced: new VersionInfo(1, 2)),

                new PageInfo(name: "Export", title: "Export", viewModelGetter: () => new TreeListExportViewModel(), descriptionGetter: () => Resources.TreeListExportDescription, introduced: new VersionInfo(1, 2))
            };
        }
    }
}