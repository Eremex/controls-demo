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
                new PageInfo(name: "Grouping", title: "Grouping", viewModelGetter: () => new DataGridGroupingPageViewModel(), descriptionGetter: () => Resources.TheDataGridSGroupingFeatureMakesItEasyToSu),
                new PageInfo(name: "Data Editors", title: "Data Editors", viewModelGetter: () => new DataGridDataEditorsViewModel(), descriptionGetter: () => Resources.YouCanEmbedAnyControlInDataGridCellsToPres),
                new PageInfo(name: "Data Validation", title: "Data Validation", viewModelGetter: () => new DataGridValidationViewModel(), descriptionGetter: () => Resources.TheDataValidationMechanismAllowsYouToCheck, introduced: new VersionInfo(1, 0)),
                new PageInfo(name: "Filter & Search", title: "Filter & Search", viewModelGetter: () => new DataGridFilteringViewModel(), descriptionGetter: () => Resources.DataGridSupportsBuiltInDataSearchAndFiltra, introduced: new VersionInfo(1, 0)),
                new PageInfo(name: "Large Data", title: "Large Data", viewModelGetter: () => new DataGridLargeDataViewModel(), descriptionGetter: () => string.Format( Resources.RegardlessOfTheNumberOfColumnsAndRowsInThe), introduced: new VersionInfo(1, 0), updated: new VersionInfo(1, 1), showInWeb: false),
                new PageInfo(name: "Row Auto Height", title: "Row Auto Height", viewModelGetter: () => new DataGridRowAutoHeightViewModel(), descriptionGetter: () => Resources.DataGridAdaptsRowHeightToFitCellContentsTe, introduced: new VersionInfo(1, 0)),
                new PageInfo(name: "Live Data", title: "Live Data", viewModelGetter: () => new DataGridLiveDataPageViewModel(), descriptionGetter: () => Resources.InThisDemoDataGridEmulatesTheWindowsTaskMa, introduced: new VersionInfo(1, 0)),
                new PageInfo(name: "Multiple Row Selection", title: "Multiple Row Selection", viewModelGetter: () => new DataGridMultipleSelectionPageViewModel(), descriptionGetter: () => Resources.DataGridSupportsMultipleRowSelectionModeWh, introduced: new VersionInfo(1, 1)),
                new PageInfo(name: "Drag & Drop", title: "Drag & Drop", viewModelGetter: () => new DataGridDragDropPageViewModel(), descriptionGetter: () => string.Format( Resources.DataGridSupportsDragAndDropOperationsWithi, Environment.NewLine + Environment.NewLine, Environment.NewLine + Environment.NewLine), introduced: new VersionInfo(1, 1), showInWeb: false)
            };
        }
    }
}