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
                new PageInfo(name: "Grouping", title: "Grouping", description: Resources.TheDataGridSGroupingFeatureMakesItEasyToSu, viewModelGetter: () => new DataGridGroupingPageViewModel()),
                new PageInfo(name: "Data Editors", title: "Data Editors", description: Resources.YouCanEmbedAnyControlInDataGridCellsToPres, viewModelGetter: () => new DataGridDataEditorsViewModel()),
                new PageInfo(name: "Data Validation", title: "Data Validation", description: Resources.TheDataValidationMechanismAllowsYouToCheck, viewModelGetter: () => new DataGridValidationViewModel(), new VersionInfo(1, 0)),
                new PageInfo(name: "Filter & Search", title: "Filter & Search", description: Resources.DataGridSupportsBuiltInDataSearchAndFiltra, viewModelGetter: () => new DataGridFilteringViewModel(), new VersionInfo(1, 0)),
                new PageInfo(name: "Large Data", title: "Large Data", description: string.Format(Resources.RegardlessOfTheNumberOfColumnsAndRowsInThe), viewModelGetter: () => new DataGridLargeDataViewModel(), new VersionInfo(1, 0), new VersionInfo(1, 1), showInWeb:false),
                new PageInfo(name: "Row Auto Height", title: "Row Auto Height", description: Resources.DataGridAdaptsRowHeightToFitCellContentsTe, viewModelGetter: () => new DataGridRowAutoHeightViewModel(), new VersionInfo(1, 0)),
                new PageInfo(name: "Live Data", title: "Live Data", description: Resources.InThisDemoDataGridEmulatesTheWindowsTaskMa, viewModelGetter: () => new DataGridLiveDataPageViewModel(), new VersionInfo(1, 0)),
                new PageInfo(name: "Multiple Row Selection", title: "Multiple Row Selection", description: Resources.DataGridSupportsMultipleRowSelectionModeWh, viewModelGetter: () => new DataGridMultipleSelectionPageViewModel(), new VersionInfo(1, 1)),
                new PageInfo(name: "Drag & Drop", title: "Drag & Drop", description: string.Format(Resources.DataGridSupportsDragAndDropOperationsWithi, Environment.NewLine + Environment.NewLine, Environment.NewLine + Environment.NewLine), viewModelGetter: () => new DataGridDragDropPageViewModel(), new VersionInfo(1, 1), showInWeb:false)
            };
        }
    }
}