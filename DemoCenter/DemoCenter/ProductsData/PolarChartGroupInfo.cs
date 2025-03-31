using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class PolarChartGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Strips and Constant Lines", title: "Strips and Constant Lines",
                viewModelGetter: () => new PolarStripsAndConstantLinesViewModel(),
                descriptionGetter: () => Resources.InAPolarChartEachDataPointIsDeterminedByAn, introduced: new VersionInfo(1, 0)),
            
            new (name: "Point", title: "Point Series View",
                viewModelGetter: () => new PolarPointSeriesViewViewModel(),
                descriptionGetter: () => Resources.ThisExampleDemonstratesThePointSeriesViewW1, introduced: new VersionInfo(1, 0)),
            
            new (name: "Line", title: "Line Series View",
                viewModelGetter: () => new PolarLineSeriesViewViewModel(),
                descriptionGetter: () => Resources.TheLineSeriesViewShownInThisExampleAllowsY1, introduced: new VersionInfo(1, 0)),
            
            new (name: "Area", title: "Area Series View",
                viewModelGetter: () => new PolarAreaSeriesViewViewModel(),
                descriptionGetter: () => Resources.TheAreaSeriesViewAllowsYouDisplayFilledAre1, introduced: new VersionInfo(1, 0)),
            
            new (name: "Scatter Line", title: "Scatter Line Series View",
                viewModelGetter: () => new PolarScatterLineSeriesViewViewModel(),
                descriptionGetter: () => Resources.TheScatterLineSeriesViewIsUsefulWhenYouNee1, introduced: new VersionInfo(1, 0)),
            
            new (name: "Range Area", title: "Range Area Series View",
                viewModelGetter: () => new PolarRangeAreaSeriesViewViewModel(),
                descriptionGetter: () => Resources.ADataSeriesInThisExampleContainsTwoYValues2, introduced: new VersionInfo(1, 0))
        };
    }
}
