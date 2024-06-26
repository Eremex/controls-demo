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
                new PageInfo(name: "Data Validation", title: "Data Validation", description: "The data validation mechanism allows you to check cell values and show errors in cells that contain invalid data. You can use DataAnnotation attributes and IDataErrorInfo/INotifyDataErrorInfo interface to validate data at the ItemsSource level. Toggle the 'Show ItemsSource Errors' checkbox in this demo to see data validation errors from DataAnnotation attributes. Data Grid also allows you to use the ValidateCellValue event to implement custom rules to validate user input.", viewModelGetter: () => new DataGridValidationViewModel(), badgeType: ProductBadgeType.New),
                new PageInfo(name: "Filter & Search", title: "Filter & Search", description: "Data Grid supports built-in data search and filtration. The Search Panel allows you to locate and highlight row cells that contain specific text. You can make the Search Panel always visible, or activate it with the Ctrl+F keyboard shortcut. The Auto Filter Row (displayed above all data rows) is used to filter data against individual columns. In code, you can customize filter conditions used in the auto-filter row for individual columns.", viewModelGetter: () => new DataGridFilteringViewModel(), badgeType: ProductBadgeType.New),
                new PageInfo(name: "Large Data", title: "Large Data", description: "This example demonstrates the Data Grid control bound to a large data source.", viewModelGetter: () => new DataGridLargeDataViewModel(), badgeType: ProductBadgeType.New),
                new PageInfo(name: "Row Auto Height", title: "Row Auto Height", description: "Data Grid adapts row height to fit cell contents. Text columns can display data in a single or multiple lines. Assign a TextEditor in-place editor (or its descendant) to a column, and enable text wrapping to display text in multiple lines.", viewModelGetter: () => new DataGridRowAutoHeightViewModel(), badgeType: ProductBadgeType.New),
                new PageInfo(name: "Live Data", title: "Live Data", description: "In this demo, Data Grid emulates the Windows Task Manager by showing frequently updated fake processes. Data Grid supports automatic data updates if a bound item source implements the INotifyPropertyChanged interface. In this example, change notifications are supported for the underlying business object using the ObservableObject class (implements the INotifyPropertyChanged interface) and ObservableProperty attributes defined in the CommunityToolkit.Mvvm library.", viewModelGetter: () => new DataGridLiveDataPageViewModel(), badgeType: ProductBadgeType.New)
            };
        }
    }
}