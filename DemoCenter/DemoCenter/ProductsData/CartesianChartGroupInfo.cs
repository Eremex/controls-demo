using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class CartesianChartGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new(name: "Real-Time Data", title: "Real-Time Data",
                description:
                "The Chart Control supports instant display of rapidly changing real-time data. This example shows how to use a special data adapter to implement a moving viewport.",
                viewModelGetter: () => new CartesianChartRealtimePageViewModel(),
                new VersionInfo(1, 0), new VersionInfo(1, 0), showInWeb: false),

            new(name: "Large Data", title: "Large Data",
                description:
                "The Chart Control's graphics rendering is optimized to display large data. The control provides high performance even when series contain millions of points",
                viewModelGetter: () => new CartesianChartLargeDataPageViewModel(),
                new VersionInfo(1, 0), new VersionInfo(1, 0), showInWeb: false),

            new(name: "Multiple Axes", title: "Multiple Axes",
                description:
                "This example demonstrates a CartesianChart control that has multiple data series and axes. You can use the mouse to scroll and zoom the entire chart area or individual axes. Use the options panel to change the main axis settings.",
                viewModelGetter: () => new CartesianChartAxesPageViewModel(),
                new VersionInfo(1, 0), new VersionInfo(1, 0)),

            new(name: "Logarithmic Scale", title: "Logarithmic Scale",
                description:
                "Chart Control supports logarithmic scales for any of its numeric axes. Log base 10 is default. You can specify a custom log base to change data scaling.",
                viewModelGetter: () => new CartesianChartLogarithmicScalePageViewModel(),
                new VersionInfo(1, 0), new VersionInfo(1, 0)),

            new(name: "Strips and Constant Lines", title: "Strips and Constant Lines",
                description:
                "This demo shows constant lines and strips used to highlight specific values and value ranges in the CartesianChart control.\nRight-click within the diagram to create a custom constant line for the X-axis.",
                viewModelGetter: () => new CartesianStripsAndConstantLinesViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Point", title: "Point Series View",
                description:
                "This example demonstrates the Point series view, which allows you to plot individual points.",
                viewModelGetter: () => new CartesianPointSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Line", title: "Line Series View",
                description:
                "The Line series view shown in this example allows you to draw a chart by connecting points with lines.",
                viewModelGetter: () => new CartesianLineSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Area", title: "Area Series View",
                description:
                "The Area series view allows you display filled areas. This view is helpful when you need to visually compare two or more data series.",
                viewModelGetter: () => new CartesianAreaSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Scatter Line", title: "Scatter Line Series View",
                description:
                "The Scatter Line series view is useful when you need to connect points in the order in which they appear in the data series.",
                viewModelGetter: () => new CartesianScatterLineSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Step Line", title: "Step Line Series View",
                description:
                "This example demonstrates the Step Line series view, which connects points with horizontal and vertical line segments.",
                viewModelGetter: () => new CartesianStepLineSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Step Area", title: "Step Area Series View",
                description:
                "The Step Area series view connects points with horizontal and vertical line segments, and fills the area between the lines and the X-axis with a specified color.",
                viewModelGetter: () => new CartesianStepAreaSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Range Area", title: "Range Area Series View",
                description:
                "A data series in this example contains two Y-values for each data point. The Range Area view fills the area between these Y-values.",
                viewModelGetter: () => new CartesianRangeAreaSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Side-by-Side Bar", title: "Side-by-Side Bar Series View",
                description:
                "This example demonstrates the Side-by-side Bar series view which visualizes data as a set of rectangular bars.",
                viewModelGetter: () => new CartesianSideBySideBarSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Side-by-Side Range Bar", title: "Side-by-Side Range Bar Series View",
                description:
                "A data series in this example contains two Y-values for each data point. The Side-by-side Range Bar view draws rectangular bars between these Y-values.",
                viewModelGetter: () => new CartesianSideBySideRangeBarSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Candlestick", title: "Candlestick Series View",
                description: "The Candlestick series view allows you to create a financial chart that describes price movements of an asset. For each time period, the chart displays a data set that contains Open, Close, High and Low values.\n\nStock data: https://www.investing.com/",
                viewModelGetter: () => new CartesianCandlestickSeriesViewViewModel(),
                new VersionInfo(1, 1)),
            
            new(name: "Candlestick Aggregation", title: "Candlestick Aggregation",
                description: "In this demo, the Candlestick series view uses a special data adapter (SummaryCandlestickDataAdapter) that accepts raw tick data and builds candlesticks from it.\nRaw ticks are single price values of an asset, taken at specific points in time. The data adapter aggregates the raw prices into candlesticks according to a specified time (measure) unit.\nFor instance, data can be aggregated to 1 second, 1 minute, 1 hour, 1 day, 1 week, etc. or to multiples of the selected time unit such as 5 seconds, 15 minutes, 2 days, etc.",
                viewModelGetter: () => new CartesianCandlestickAggregationViewModel(),
                new VersionInfo(1, 1))
        };
    }
}
