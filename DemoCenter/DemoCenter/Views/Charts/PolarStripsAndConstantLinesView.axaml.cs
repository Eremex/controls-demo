using Avalonia.Controls;
using Avalonia.Input;
using DemoCenter.ViewModels;

namespace DemoCenter.Views;

public partial class PolarStripsAndConstantLinesView : UserControl
{
    PolarStripsAndConstantLinesViewModel ViewModel => DataContext as PolarStripsAndConstantLinesViewModel;
    
    public PolarStripsAndConstantLinesView()
    {
        InitializeComponent();
    }
    void DemoControl_OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if(ViewModel is null)
            return;
        
        var point = e.GetCurrentPoint(DemoControl);
        var coords = DemoControl.ScreenPointToDiagramPoint(point.Position);
        if (coords.InsideViewport && coords.Argument.HasValue && coords.Value is not null)
        {
            if (point.Properties.IsLeftButtonPressed)
                ViewModel.ConstantLinesX.Add(new ConstantLineViewModel { AxisValue = coords.Argument, Title = coords.Argument.ToString() });
            if (point.Properties.IsRightButtonPressed)
                ViewModel.ConstantLinesY.Add(new ConstantLineViewModel { AxisValue = coords.Value, Title = coords.Value.ToString() });
        }
    }
}