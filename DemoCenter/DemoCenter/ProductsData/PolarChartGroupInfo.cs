using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class PolarChartGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Strips and Constant Lines", title: "Strips and Constant Lines",
                description: "TODO LMB to add new constant line to axis X, RMB to Y",
                viewModelGetter: () => new PolarStripsAndConstantLinesViewModel(),
                ProductBadgeType.New),
            
            new (name: "Point", title: "Point Series View",
                description: "TODO",
                viewModelGetter: () => new PolarPointSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Line", title: "Line Series View",
                description: "TODO",
                viewModelGetter: () => new PolarLineSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Area", title: "Area Series View",
                description: "TODO",
                viewModelGetter: () => new PolarAreaSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Scatter Line", title: "Scatter Line Series View",
                description: "TODO",
                viewModelGetter: () => new PolarScatterLineSeriesViewViewModel(),
                ProductBadgeType.New),
            
            new (name: "Range Area", title: "Range Area Series View",
                description: "TODO",
                viewModelGetter: () => new PolarRangeAreaSeriesViewViewModel(),
                ProductBadgeType.New)
        };
    }
}
