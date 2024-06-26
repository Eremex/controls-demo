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
                ProductBadgeType.Updated, showInWeb: false),
            
            new (name: "Large Data", title: "Large Data",
                description: "The Chart Control's graphics rendering is optimized to display large data. The control provides high performance even when series contain millions of points",
                viewModelGetter: () => new CartesianChartLargeDataPageViewModel(),
                ProductBadgeType.Updated, showInWeb: false),

            new (name: "Multiple Axes", title: "Multiple Axes",
                description: "This example demonstrates a CartesianChart control that has multiple data series and axes. You can use the mouse to scroll and zoom the entire chart area or individual axes. Use the options panel to change the main axis settings.",
                viewModelGetter: () => new CartesianChartAxesPageViewModel(),
                ProductBadgeType.Updated),
            
            new (name: "Logarithmic Scale", title: "Logarithmic Scale",
                description: "Chart Control supports logarithmic scales for any of its numeric axes. Log base 10 is default. You can specify a custom log base to change data scaling.",
                viewModelGetter: () => new CartesianChartLogarithmicScalePageViewModel(),
                ProductBadgeType.Updated),
            
            new (name: "Strips and Constant Lines", title: "Strips and Constant Lines",
                description: "TODO RMB to add new constant line to axis X",
                viewModelGetter: () => new CartesianStripsAndConstantLinesViewModel(),
                ProductBadgeType.New),
            
            new (name: "Point", title: "Point Series View",
                description: "TODO",
                viewModelGetter: () => new CartesianPointSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Line", title: "Line Series View",
                description: "TODO",
                viewModelGetter: () => new CartesianLineSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Area", title: "Area Series View",
                description: "TODO",
                viewModelGetter: () => new CartesianAreaSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Scatter Line", title: "Scatter Line Series View",
                description: "TODO",
                viewModelGetter: () => new CartesianScatterLineSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Step Line", title: "Step Line Series View",
                description: "TODO",
                viewModelGetter: () => new CartesianStepLineSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Step Area", title: "Step Area Series View",
                description: "TODO",
                viewModelGetter: () => new CartesianStepAreaSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Range Area", title: "Range Area Series View",
                description: "TODO",
                viewModelGetter: () => new CartesianRangeAreaSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Side-by-Side Bar", title: "Side-by-Side Bar Series View",
                description: "TODO",
                viewModelGetter: () => new CartesianSideBySideBarSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Side-by-Side Range Bar", title: "Side-by-Side Range Bar Series View",
                description: "TODO",
                viewModelGetter: () => new CartesianSideBySideRangeBarSeriesViewViewModel(),
                ProductBadgeType.New)
        };
    }
}
