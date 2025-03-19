using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class SmithChartGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Point", title: "Point Series View",
                description: Resources.ThisExampleDemonstratesThePointSeriesViewW2,
                viewModelGetter: () => new SmithPointSeriesViewViewModel(),
                new VersionInfo(1, 0)),
            
            new (name: "Scatter Line", title: "Scatter Line Series View",
                description: Resources.TheScatterLineSeriesViewIsUsefulWhenYouNee2,
                viewModelGetter: () => new SmithLineSeriesViewViewModel(),
                new VersionInfo(1, 0))
        };
    }
}
