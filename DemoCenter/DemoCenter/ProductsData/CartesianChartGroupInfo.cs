using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class CartesianChartGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Real-Time Data", title: "Real-Time Data",
                description: "The Chart Control supports instant display of rapidly changing real-time data. This example shows how to use a special data adapter to implement a moving viewport.",
                viewModelGetter: () => new CartesianChartRealtimePageViewModel(),
                ProductBageType.New, showInWeb: false),
            
            new (name: "Large Data", title: "Large Data",
                description: "The Chart Control's graphics rendering is optimized to display large data. The control provides high performance even when series contain millions of points",
                viewModelGetter: () => new CartesianChartLargeDataPageViewModel(),
                ProductBageType.New, showInWeb: false),

            new (name: "Multiple Axes", title: "Multiple Axes",
            description: "This example demonstrates a CartesianChart control that has multiple data series and axes. You can use the mouse to scroll and zoom the entire chart area or individual axes. Use the options panel to change the main axis settings.",
            viewModelGetter: () => new CartesianChartAxesPageViewModel(),
            ProductBageType.New),
            
            new (name: "Logarithmic Scale", title: "Logarithmic Scale",
                description: "",
                viewModelGetter: () => new CartesianChartLogarithmicScalePageViewModel(),
                ProductBageType.New)
        };
    }
}
