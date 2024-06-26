using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class SmithChartGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Point", title: "Point Series View",
                description: "TODO",
                viewModelGetter: () => new SmithPointSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Scatter Line", title: "Scatter Line Series View",
                description: "TODO",
                viewModelGetter: () => new SmithLineSeriesViewViewModel(),
                ProductBadgeType.New)
        };
    }
}
