using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class SmithChartGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Point", title: "Point Series View",
                description: "This example demonstrates the Point series view, which allows you to plot individual points on the Smith diagram.",
                viewModelGetter: () => new SmithPointSeriesViewViewModel(),
                new VersionInfo(1, 0)),
            
            new (name: "Scatter Line", title: "Scatter Line Series View",
                description: "The Scatter Line series view is useful when you need to connect points in the order in which they appear in the data series.",
                viewModelGetter: () => new SmithLineSeriesViewViewModel(),
                new VersionInfo(1, 0))
        };
    }
}
