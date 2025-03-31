using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class SmithChartGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Point", title: "Point Series View",
                viewModelGetter: () => new SmithPointSeriesViewViewModel(),
                descriptionGetter: () => Resources.ThisExampleDemonstratesThePointSeriesViewW2, introduced: new VersionInfo(1, 0)),
            
            new (name: "Scatter Line", title: "Scatter Line Series View",
                viewModelGetter: () => new SmithLineSeriesViewViewModel(),
                descriptionGetter: () => Resources.TheScatterLineSeriesViewIsUsefulWhenYouNee2, introduced: new VersionInfo(1, 0))
        };
    }
}
