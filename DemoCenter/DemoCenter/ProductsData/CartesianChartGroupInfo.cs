using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class CartesianChartGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new(name: "Real-Time Data", title: "Real-Time Data",
                viewModelGetter: () => new CartesianChartRealtimePageViewModel(),
                descriptionGetter: () => Resources.TheChartControlSupportsInstantDisplayOfRap, introduced: new VersionInfo(1, 0), updated: new VersionInfo(1, 0), showInWeb: false),

            new(name: "Large Data", title: "Large Data",
                viewModelGetter: () => new CartesianChartLargeDataPageViewModel(),
                descriptionGetter: () => Resources.TheChartControlSGraphicsRenderingIsOptimiz, introduced: new VersionInfo(1, 0), updated: new VersionInfo(1, 0), showInWeb: false),

            new(name: "Multiple Axes", title: "Multiple Axes",
                viewModelGetter: () => new CartesianChartAxesPageViewModel(),
                descriptionGetter: () => Resources.ThisExampleDemonstratesACartesianChartCont, introduced: new VersionInfo(1, 0), updated: new VersionInfo(1, 0)),

            new(name: "Logarithmic Scale", title: "Logarithmic Scale",
                viewModelGetter: () => new CartesianChartLogarithmicScalePageViewModel(),
                descriptionGetter: () => Resources.ChartControlSupportsLogarithmicScalesForAn, introduced: new VersionInfo(1, 0), updated: new VersionInfo(1, 0)),

            new(name: "Strips and Constant Lines", title: "Strips and Constant Lines",
                viewModelGetter: () => new CartesianStripsAndConstantLinesViewModel(),
                descriptionGetter: () => Resources.ThisDemoShowsConstantLinesAndStripsUsedToH, introduced: new VersionInfo(1, 0)),

            new(name: "Point", title: "Point Series View",
                viewModelGetter: () => new CartesianPointSeriesViewViewModel(),
                descriptionGetter: () => Resources.ThisExampleDemonstratesThePointSeriesViewW, introduced: new VersionInfo(1, 0)),

            new(name: "Line", title: "Line Series View",
                viewModelGetter: () => new CartesianLineSeriesViewViewModel(),
                descriptionGetter: () => Resources.TheLineSeriesViewShownInThisExampleAllowsY, introduced: new VersionInfo(1, 0)),

            new(name: "Area", title: "Area Series View",
                viewModelGetter: () => new CartesianAreaSeriesViewViewModel(),
                descriptionGetter: () => Resources.TheAreaSeriesViewAllowsYouDisplayFilledAre, introduced: new VersionInfo(1, 0)),

            new(name: "Scatter Line", title: "Scatter Line Series View",
                viewModelGetter: () => new CartesianScatterLineSeriesViewViewModel(),
                descriptionGetter: () => Resources.TheScatterLineSeriesViewIsUsefulWhenYouNee, introduced: new VersionInfo(1, 0)),

            new(name: "Step Line", title: "Step Line Series View",
                viewModelGetter: () => new CartesianStepLineSeriesViewViewModel(),
                descriptionGetter: () => Resources.ThisExampleDemonstratesTheStepLineSeriesVi, introduced: new VersionInfo(1, 0)),

            new(name: "Step Area", title: "Step Area Series View",
                viewModelGetter: () => new CartesianStepAreaSeriesViewViewModel(),
                descriptionGetter: () => Resources.TheStepAreaSeriesViewConnectsPointsWithHor, introduced: new VersionInfo(1, 0)),

            new(name: "Range Area", title: "Range Area Series View",
                viewModelGetter: () => new CartesianRangeAreaSeriesViewViewModel(),
                descriptionGetter: () => Resources.ADataSeriesInThisExampleContainsTwoYValues, introduced: new VersionInfo(1, 0)),

            new(name: "Side-by-Side Bar", title: "Side-by-Side Bar Series View",
                viewModelGetter: () => new CartesianSideBySideBarSeriesViewViewModel(),
                descriptionGetter: () => Resources.ThisExampleDemonstratesTheSideBySideBarSer, introduced: new VersionInfo(1, 0)),

            new(name: "Side-by-Side Range Bar", title: "Side-by-Side Range Bar Series View",
                viewModelGetter: () => new CartesianSideBySideRangeBarSeriesViewViewModel(),
                descriptionGetter: () => Resources.ADataSeriesInThisExampleContainsTwoYValues1, introduced: new VersionInfo(1, 0)),

            new(name: "Candlestick", title: "Candlestick Series View",
                viewModelGetter: () => new CartesianCandlestickSeriesViewViewModel(),
                descriptionGetter: () => Resources.TheCandlestickSeriesViewAllowsYouToCreateA, introduced: new VersionInfo(1, 1)),
            
            new(name: "Candlestick Aggregation", title: "Candlestick Aggregation",
                viewModelGetter: () => new CartesianCandlestickAggregationViewModel(),
                descriptionGetter: () => Resources.InThisDemoTheCandlestickSeriesViewUsesASpe, introduced: new VersionInfo(1, 1))
        };
    }
}
