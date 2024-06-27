using Avalonia.Controls;
using Avalonia.Input;
using DemoCenter.ViewModels;

namespace DemoCenter.Views;

public partial class CartesianStripsAndConstantLinesView : UserControl
{
    CartesianStripsAndConstantLinesViewModel ViewModel => DataContext as CartesianStripsAndConstantLinesViewModel;
    
    public CartesianStripsAndConstantLinesView()
    {
        InitializeComponent();
    }
    void DemoControl_OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        var point = e.GetCurrentPoint(DemoControl);
        if (point.Properties.IsRightButtonPressed && ViewModel is not null)
        {
            var coords = DemoControl.ScreenPointToDiagramPoint(point.Position);
            if (coords.InsideViewport)
            {
                object argument = coords.AxesX.First().Value;
                ViewModel.ConstantLines.Add(new ConstantLineViewModel { AxisValue = argument, Title = argument.ToString() });
            }
        }
    }
}