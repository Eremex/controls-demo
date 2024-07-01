using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class PolarChartGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Strips and Constant Lines", title: "Strips and Constant Lines",
                description: "In a Polar Chart each data point is determined by an angle and a distance. This example demonstrates constant lines and strips used to highlight specific values and value ranges. Angle ranges and distance ranges are set in code of this demo. Left-click within a diagram to create custom constant lines for angles. Right-click to create custom constant lines for distances.",
                viewModelGetter: () => new PolarStripsAndConstantLinesViewModel(),
                ProductBadgeType.New),
            
            new (name: "Point", title: "Point Series View",
                description: "This example demonstrates the Point series view, which allows you to plot individual points.",
                viewModelGetter: () => new PolarPointSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Line", title: "Line Series View",
                description: "The Line series view shown in this example allows you to draw a chart by connecting points with lines.",
                viewModelGetter: () => new PolarLineSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Area", title: "Area Series View",
                description: "The Area series view allows you display filled areas between a chart and point Zero. This view is helpful when you need to visually compare two or more data series.",
                viewModelGetter: () => new PolarAreaSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Scatter Line", title: "Scatter Line Series View",
                description: "The Scatter Line series view is useful when you need to connect points in the order in which they appear in the data series.",
                viewModelGetter: () => new PolarScatterLineSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Range Area", title: "Range Area Series View",
                description: "A data series in this example contains two Y-values for each data point. The Range Area view fills the area between these Y-values.",
                viewModelGetter: () => new PolarRangeAreaSeriesViewViewModel(),
                ProductBadgeType.New)
        };
    }
}
