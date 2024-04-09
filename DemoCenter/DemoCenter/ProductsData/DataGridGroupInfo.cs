using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.ProductsData
{
    public static class DataGridGroupInfo
    {
        internal static List<PageInfo> Create()
        {
            return new List<PageInfo>()
            {
                new PageInfo(name: "Grouping", title: "Grouping", description: "The Data Grid's grouping feature makes it easy to summarize information for users. They can group data by an unlimited number of columns by dragging columns onto the Group Panel.", viewModelGetter: () => new DataGridGroupingPageViewModel()),
                new PageInfo(name: "Data Editors", title: "Data Editors", description: "You can embed any control in Data Grid cells to present and edit cell data in the way you want. This demo demonstrates Eremex in-place editors: DateEditor, SpinEditor, and ComboBoxEditor. Data Grid boasts enhanced performance when you use Eremex editors in cells, particularly for large data sources.", viewModelGetter: () => new DataGridDataEditorsViewModel()),
                new PageInfo(name: "Large Data", title: "Large Data", description: "This example demonstrates the Data Grid control bound to a large data source.", viewModelGetter: () => new DataGridLargeDataViewModel(), bageType: ProductBageType.New)
            };
        }
    }
}