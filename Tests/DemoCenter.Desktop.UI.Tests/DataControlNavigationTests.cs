using DemoCenter.ProductsData;

using Eremex.AvaloniaUI.Controls.DataControl;

namespace DemoCenter.Desktop.UI.Tests;

/// <summary>
/// Drives the data grid and the tree list of every demo page that has one: row navigation,
/// scrolling and multiple selection.
/// </summary>
/// <remarks>
/// These are the gestures a visitor makes first, and none of them is covered by opening the page:
/// a grid renders its first screen fine and still throws when asked for the next one, or leaves
/// selection behind when the mode changes. Only the public API is used - the demo assembly does
/// not see the internals the control's own tests rely on.
/// </remarks>
public class DataControlNavigationTests : IClassFixture<DemoWindowFixture>
{
    private readonly DemoWindowFixture demo;

    public DataControlNavigationTests(DemoWindowFixture demo) => this.demo = demo;

    /// <summary>The two products whose pages are built around a data control.</summary>
    public static IEnumerable<object[]> DataProductNames() =>
        new[] { new object[] { "Data Grid" }, new object[] { "Tree List" } };

    [Theory]
    [MemberData(nameof(DataProductNames))]
    public void RowsCanBeNavigatedScrolledAndSelected(string productName)
    {
        var exercised = 0;

        foreach (var module in ProductModulesTests.ModulesOf(productName))
        {
            if (module is GroupInfo)
                continue;

            demo.Open(module);

            var dataControl = demo.FindDataControl();

            // Not every page of these products is built around a data control - the export and
            // drag-and-drop pages, for instance, show something else next to it or nothing at all.
            if (dataControl == null)
                continue;

            exercised++;
            NavigateRows(module, dataControl);
            Scroll(module, dataControl);
            SelectMultipleRows(module, dataControl);
        }

        Assert.True(exercised > 0, $"{productName}: no page turned out to have a data control.");
    }

    /// <summary>Moves the focused row a page down and back.</summary>
    /// <remarks>
    /// MoveNextPage is the widest gesture available publicly: it walks the virtualized rows, so it
    /// exercises materialization of rows that were never realized, which plain focus assignment
    /// does not.
    /// </remarks>
    private void NavigateRows(ProductInfoBase module, DataControlBase dataControl)
    {
        using var errors = new AvaloniaErrorCollector();

        var start = dataControl.FocusedItem;

        dataControl.MoveNextPage();
        demo.Settle();

        dataControl.MovePrevPage();
        demo.Settle();

        errors.AssertQuiet($"{module.Name}: paging through rows");

        // Restored so that the next check starts from where the page opened.
        dataControl.FocusedItem = start;
        demo.Settle();
    }

    /// <summary>Scrolls to the end and back, when there is anything to scroll.</summary>
    private void Scroll(ProductInfoBase module, DataControlBase dataControl)
    {
        var scrollViewer = DemoWindowFixture.FindScrollViewer(dataControl);
        if (scrollViewer == null || scrollViewer.Extent.Height <= scrollViewer.Viewport.Height)
            return;

        using var errors = new AvaloniaErrorCollector();

        scrollViewer.ScrollToEnd();
        demo.Settle();

        Assert.True(
            scrollViewer.Offset.Y > 0,
            $"{module.Name}: scrolling to the end left the offset at zero.");

        scrollViewer.ScrollToHome();
        demo.Settle();

        Assert.True(
            scrollViewer.Offset.Y == 0,
            $"{module.Name}: scrolling back home left the offset at {scrollViewer.Offset.Y}.");

        errors.AssertQuiet($"{module.Name}: scrolling");
    }

    /// <summary>Selects every row at once and clears the selection again.</summary>
    /// <remarks>
    /// The selection mode is put back afterwards: the window is shared by the whole class, and a
    /// page left in Multiple would change what the next test sees.
    /// </remarks>
    private void SelectMultipleRows(ProductInfoBase module, DataControlBase dataControl)
    {
        var originalMode = dataControl.SelectionMode;

        try
        {
            using var errors = new AvaloniaErrorCollector();

            dataControl.SelectionMode = RowSelectionMode.Multiple;
            demo.Settle();

            dataControl.SelectAll();
            demo.Settle();

            Assert.True(
                dataControl.SelectedItems != null && dataControl.SelectedItems.Count > 1,
                $"{module.Name}: SelectAll in Multiple mode selected {dataControl.SelectedItems?.Count ?? 0} rows.");

            dataControl.SelectedItems.Clear();
            demo.Settle();

            errors.AssertQuiet($"{module.Name}: selecting rows");
        }
        finally
        {
            dataControl.SelectionMode = originalMode;
            demo.Settle();
        }
    }
}
