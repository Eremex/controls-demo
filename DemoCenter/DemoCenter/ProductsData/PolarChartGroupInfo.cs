using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class PolarChartGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Strips and Constant Lines", title: "Strips and Constant Lines",
                description: Resources.InAPolarChartEachDataPointIsDeterminedByAn,
                viewModelGetter: () => new PolarStripsAndConstantLinesViewModel(),
                new VersionInfo(1, 0)),
            
            new (name: "Point", title: "Point Series View",
                description: Resources.ThisExampleDemonstratesThePointSeriesViewW1,
                viewModelGetter: () => new PolarPointSeriesViewViewModel(),
                new VersionInfo(1, 0)),
            
            new (name: "Line", title: "Line Series View",
                description: Resources.TheLineSeriesViewShownInThisExampleAllowsY1,
                viewModelGetter: () => new PolarLineSeriesViewViewModel(),
                new VersionInfo(1, 0)),
            
            new (name: "Area", title: "Area Series View",
                description: Resources.TheAreaSeriesViewAllowsYouDisplayFilledAre1,
                viewModelGetter: () => new PolarAreaSeriesViewViewModel(),
                new VersionInfo(1, 0)),
            
            new (name: "Scatter Line", title: "Scatter Line Series View",
                description: Resources.TheScatterLineSeriesViewIsUsefulWhenYouNee1,
                viewModelGetter: () => new PolarScatterLineSeriesViewViewModel(),
                new VersionInfo(1, 0)),
            
            new (name: "Range Area", title: "Range Area Series View",
                description: Resources.ADataSeriesInThisExampleContainsTwoYValues2,
                viewModelGetter: () => new PolarRangeAreaSeriesViewViewModel(),
                new VersionInfo(1, 0))
        };
    }
}
