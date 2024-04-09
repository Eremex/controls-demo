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
                description: "The Tree List's Auto-Filter Row displayed at the top of the control allows you to quickly find rows that contain specific values. Start typing in the Auto-Filter Row in one or multiple columns to filter data. The Search Panel is another way to locate information in the Tree List. Press CTRL+F to activate the Search Panel, and then type text to search through the control's data.",
                viewModelGetter: () => new TreeListFilteringPageViewModel(),
                showInWeb: false),

                new PageInfo(name: "Data Editors", title: "Data Editors",
                description: "Eremex editors are used in Tree List cells by default to display and edit cell values of common data types (Boolean, integer, enumerations, etc.). This demo shows implicitly assigned in-place editors, and demonstrates how you can explicitly specify an editor for a Tree List column.",
                viewModelGetter: () => new TreeListDataEditorsPageViewModel(),
                showInWeb: false),

                new PageInfo(name: "Folder Browser", title: "Folder Browser",
                description: "You can bind Tree List to a hierarchical data source, which returns child data through a collection property or special data selector. This demo shows how to create a selector that supplies the hierarchical structure of folders on your disk.",
                viewModelGetter: () => new FolderBrowserPageViewModel()),
            };
        }
    }
}