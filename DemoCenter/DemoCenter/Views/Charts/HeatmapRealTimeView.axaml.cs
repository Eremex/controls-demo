#nullable enable
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using DemoCenter.ViewModels;

namespace DemoCenter.Views;

public partial class HeatmapRealTimeView : UserControl
{
    readonly Cursor moveCursor = new(StandardCursorType.SizeWestEast);
    bool isChartSelected;
    bool isHeatmapSelected;
    
    HeatmapRealTimeViewModel? ViewModel => DataContext as HeatmapRealTimeViewModel;
    
    public HeatmapRealTimeView()
    {
        InitializeComponent();
    }
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        ViewModel?.Start();
    }
    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
        ViewModel?.Stop();
    }
    void DemoChart_OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (Chart.ScreenPointToDiagramPoint(e.GetPosition(Chart)).InsideViewport)
            isChartSelected = true;
    }
    void DemoChart_OnPointerMoved(object sender, PointerEventArgs e)
    {
        if (isChartSelected)
        {
            Chart.Cursor = moveCursor;
            var diagramPoint = Chart.ScreenPointToDiagramPoint(e.GetPosition(Chart));
            if (diagramPoint.AxesX.TryGetValue(AxisX, out var val) && val is string str)
                ViewModel?.UpdateFrequency(str);
        }
    }
    void DemoChart_OnPointerReleased(object sender, PointerReleasedEventArgs e)
    {
        var diagramPoint = Chart.ScreenPointToDiagramPoint(e.GetPosition(Chart));
        if (diagramPoint.InsideViewport && diagramPoint.AxesX.TryGetValue(AxisX, out var val) && val is string str)
            ViewModel?.UpdateFrequency(str);
        isChartSelected = false;
        Chart.Cursor = null;
    }
    void DemoControl_OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (DemoControl.ScreenPointToDiagramPoint(e.GetPosition(DemoControl)).InsideViewport)
            isHeatmapSelected = true;
    }
    void DemoControl_OnPointerMoved(object sender, PointerEventArgs e)
    {
        if (isHeatmapSelected)
        {
            DemoControl.Cursor = moveCursor;
            var diagramPoint = DemoControl.ScreenPointToDiagramPoint(e.GetPosition(DemoControl));
            if (diagramPoint.ArgumentX is not null)
                ViewModel?.UpdateFrequency(diagramPoint.ArgumentX);
        }
    }
    void DemoControl_OnPointerReleased(object sender, PointerReleasedEventArgs e)
    {
        var diagramPoint = DemoControl.ScreenPointToDiagramPoint(e.GetPosition(DemoControl));
        if (diagramPoint is { InsideViewport: true, ArgumentX: not null })
            ViewModel?.UpdateFrequency(diagramPoint.ArgumentX);
        isHeatmapSelected = false;
        DemoControl.Cursor = null;
    }
}