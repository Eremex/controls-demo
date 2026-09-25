using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using DemoCenter.DemoData;

using Eremex.AvaloniaUI.Controls.DataControl;
using Eremex.AvaloniaUI.Controls.Editors;
using Eremex.AvaloniaUI.Controls.TreeList;

namespace DemoCenter.Views;

public partial class TreeListTotalSummariesView : UserControl
{
    SummaryItem customSummaryItem;

    public TreeListTotalSummariesView()
    {
        InitializeComponent();
    }

    private void OnTreeListInitialized(object sender, EventArgs e)
    {
        treeList.Nodes[0].IsExpanded = true;
        treeList.Nodes[0].Nodes[0].IsExpanded = true;
        treeList.Nodes[0].Nodes[1].IsExpanded = true;
    }

    void OnCustomSummaryCheckChanged(object sender, EditorValueChangedEventArgs e)
    {
        if (customSummaryItem != null && treeList.TotalSummaries.Contains(customSummaryItem))
            treeList.TotalSummaries.Remove(customSummaryItem);
        if (e.NewValue is true)
        {
            customSummaryItem = new SummaryItem() { FieldName = "Status", SummaryType = SummaryItemType.Custom, Label = "Needs Attention" };
            treeList.TotalSummaries.Add(customSummaryItem);
        }
    }

    void OnCustomSummary(object sender, TreeListCustomSummaryEventArgs e)
    {
        int assetsRequiringAttention = 0;
        foreach (var node in e.Nodes)
        {
            var status = (AssetStatus)treeList.GetCellValue(node, e.SummaryItem.FieldName);
            if (status is AssetStatus.Maintenance or AssetStatus.Degraded or AssetStatus.Offline or AssetStatus.Decommissioned)
                assetsRequiringAttention++;
        }
        e.SummaryValue = assetsRequiringAttention;
    }
}

public class StatusToVisibilityConverter : IValueConverter
{
    public static StatusToVisibilityConverter Instance = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is AssetStatus status
            && (status is AssetStatus.Maintenance or AssetStatus.Degraded or AssetStatus.Offline or AssetStatus.Decommissioned);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
