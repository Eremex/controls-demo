using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Eremex.AvaloniaUI.Controls.DataControl;
using Eremex.AvaloniaUI.Controls.DataGrid;
using Eremex.AvaloniaUI.Controls.Editors;

namespace DemoCenter.Views;

public partial class DataGridTotalSummariesView : UserControl
{
    const decimal LargeOrderThreshold = 1000m;

    SummaryItem customSummaryItem;

    public DataGridTotalSummariesView()
    {
        InitializeComponent();
    }

    void OnCustomSummaryCheckChanged(object sender, EditorValueChangedEventArgs e)
    {
        if (customSummaryItem != null && dataGrid.TotalSummaries.Contains(customSummaryItem))
            dataGrid.TotalSummaries.Remove(customSummaryItem);
        if (e.NewValue is true)
        {
            customSummaryItem = new SummaryItem() { FieldName = "Total", SummaryType = SummaryItemType.Custom, Label = "Orders > 1000" };
            dataGrid.TotalSummaries.Add(customSummaryItem);
        }
    }

    void OnCustomSummary(object sender, DataGridCustomSummaryEventArgs e)
    {
        int largeOrdersCount = 0;
        foreach (int rowIndex in e.RowIndexes)
        {
            if ((decimal)dataGrid.GetCellValue(rowIndex, e.SummaryItem.FieldName) > LargeOrderThreshold)
                largeOrdersCount++;
        }
        e.SummaryValue = largeOrdersCount.ToString();
    }
}

public class DataGridSummaryTypeToForegroundConverter : MarkupExtension, IMultiValueConverter
{
    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Count != 4)
            return null;

        if (values[0] is string fieldName && fieldName == "Price" && values[1] is SummaryItemType sumType)
        {
            if (sumType == SummaryItemType.Min)
                return values[2];
            else if (sumType == SummaryItemType.Max)
                return values[3];
        }

        return null;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}
