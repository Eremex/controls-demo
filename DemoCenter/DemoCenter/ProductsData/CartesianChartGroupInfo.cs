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
                description: "This demo shows constant lines and strips used to highlight specific values and value ranges in the CartesianChart control.\nRight-click within the diagram to create a custom constant line for the X-axis.",
                viewModelGetter: () => new CartesianStripsAndConstantLinesViewModel(),
                ProductBadgeType.New),
            
            new (name: "Point", title: "Point Series View",
                description: "This example demonstrates the Point series view, which allows you to plot individual points.",
                viewModelGetter: () => new CartesianPointSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Line", title: "Line Series View",
                description: "The Line series view shown in this example allows you to draw a chart by connecting points with lines.",
                viewModelGetter: () => new CartesianLineSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Area", title: "Area Series View",
                description: "The Area series view allows you display filled areas. This view is helpful when you need to visually compare two or more data series.",
                viewModelGetter: () => new CartesianAreaSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Scatter Line", title: "Scatter Line Series View",
                description: "The Scatter Line series view is useful when you need to connect points in the order in which they appear in the data series.",
                viewModelGetter: () => new CartesianScatterLineSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Step Line", title: "Step Line Series View",
                description: "This example demonstrates the Step Line series view, which connects points with horizontal and vertical line segments.",
                viewModelGetter: () => new CartesianStepLineSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Step Area", title: "Step Area Series View",
                description: "The Step Area series view connects points with horizontal and vertical line segments, and fills the area between the lines and the X-axis with a specified color.",
                viewModelGetter: () => new CartesianStepAreaSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Range Area", title: "Range Area Series View",
                description: "A data series in this example contains two Y-values for each data point. The Range Area view fills the area between these Y-values.",
                viewModelGetter: () => new CartesianRangeAreaSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Side-by-Side Bar", title: "Side-by-Side Bar Series View",
                description: "This example demonstrates the Side-by-side Bar series view which visualizes data as a set of rectangular bars.",
                viewModelGetter: () => new CartesianSideBySideBarSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Side-by-Side Range Bar", title: "Side-by-Side Range Bar Series View",
                description: "A data series in this example contains two Y-values for each data point. The Side-by-side Range Bar view draws rectangular bars between these Y-values.",
                viewModelGetter: () => new CartesianSideBySideRangeBarSeriesViewViewModel(),
                ProductBadgeType.New)
        };
    }
}
