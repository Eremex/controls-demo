using Avalonia.Controls;

using Eremex.AvaloniaUI.Controls.PropertyGrid;

namespace DemoCenter.Views;

public partial class CarComparisonView : UserControl
{
    public CarComparisonView()
    {
        InitializeComponent();

        leftGrid.UsingComplexDataContext += OnUsingComplexDataContext;
        rightGrid.UsingComplexDataContext += OnUsingComplexDataContext;
    }

    // Every row here shows a value of its own - a picture or a cell carrying its verdict - so none
    // of them is expanded into sub-rows.
    private void OnUsingComplexDataContext(object sender, UsingComplexDataContextEventArgs e)
    {
        e.Cancel = true;
    }
}
