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
                new PageInfo(name: "Data Validation", title: "Data Validation", description: "The data validation mechanism allows you to check cell values and show errors in cells that contain invalid data. You can use DataAnnotation attributes and IDataErrorInfo/INotifyDataErrorInfo interface to validate data at the ItemsSource level. Toggle the 'Show ItemsSource Errors' checkbox in this demo to see data validation errors from DataAnnotation attributes. Data Grid also allows you to use the ValidateCellValue event to implement custom rules to validate user input.", viewModelGetter: () => new DataGridValidationViewModel(), new VersionInfo(1, 0)),
                new PageInfo(name: "Filter & Search", title: "Filter & Search", description: "Data Grid supports built-in data search and filtration. The Search Panel allows you to locate and highlight row cells that contain specific text. You can make the Search Panel always visible, or activate it with the Ctrl+F keyboard shortcut. The Auto Filter Row (displayed above all data rows) is used to filter data against individual columns. In code, you can customize filter conditions used in the auto-filter row for individual columns.", viewModelGetter: () => new DataGridFilteringViewModel(), new VersionInfo(1, 0)),
                new PageInfo(name: "Large Data", title: "Large Data", description: $"Regardless of the number of columns and rows in the control, Data Grid remains responsive as you scroll the control horizontally or vertically, or sort/group its data. The built-in data virtualization mechanism updates only cells within the viewport to maintain high-performance scrolling", viewModelGetter: () => new DataGridLargeDataViewModel(), new VersionInfo(1, 0), new VersionInfo(1, 1)),
                new PageInfo(name: "Row Auto Height", title: "Row Auto Height", description: "Data Grid adapts row height to fit cell contents. Text columns can display data in a single or multiple lines. Assign a TextEditor in-place editor (or its descendant) to a column, and enable text wrapping to display text in multiple lines.", viewModelGetter: () => new DataGridRowAutoHeightViewModel(), new VersionInfo(1, 0)),
                new PageInfo(name: "Live Data", title: "Live Data", description: "In this demo, Data Grid emulates the Windows Task Manager by showing frequently updated fake processes. Data Grid supports automatic data updates if a bound item source implements the INotifyPropertyChanged interface. In this example, change notifications are supported for the underlying business object using the ObservableObject class (implements the INotifyPropertyChanged interface) and ObservableProperty attributes defined in the CommunityToolkit.Mvvm library.", viewModelGetter: () => new DataGridLiveDataPageViewModel(), new VersionInfo(1, 0)),
                new PageInfo(name: "Multiple Row Selection", title: "Multiple Row Selection", description: "DataGrid supports multiple row selection mode, which allows you and your user to select (highlight) multiple rows at one time. Users can select multiple rows with the mouse and keyboard. Click rows while holding the CTRL and/or SHIFT key down for row selection.", viewModelGetter: () => new DataGridMultipleSelectionPageViewModel(), new VersionInfo(1, 1)),
                new PageInfo(name: "Drag & Drop", title: "Drag & Drop", description: $"Data Grid supports drag-and-drop operations within the control and to external controls (for instance, Tree List or another Data Grid).This example demonstrates drag-and-drop functionality within and between Data Grids. The AllowDragDrop option activates the drag-and-drop feature. { Environment.NewLine + Environment.NewLine }The AllowDragDropSortedRows option must be enabled to allow drag-and-drop operations for sorted/grouped Data Grids. When data is sorted or grouped, a drag-and-drop operation modifies values of a dragged row in sort columns. { Environment.NewLine + Environment.NewLine }In this example, the controls are bound to collections that contain the same business objects. This ensures automatic row movement during drag-and-drop operations from the source to the target grid. When you move a row from one grid to another, a corresponding item is moved between item sources of the two grid controls.", viewModelGetter: () => new DataGridDragDropPageViewModel(), new VersionInfo(1, 1))
            };
        }
    }
}