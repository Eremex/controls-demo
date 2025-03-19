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
                Resources.TheChartControlSupportsInstantDisplayOfRap,
                viewModelGetter: () => new CartesianChartRealtimePageViewModel(),
                new VersionInfo(1, 0), new VersionInfo(1, 0), showInWeb: false),

            new(name: "Large Data", title: "Large Data",
                description:
                Resources.TheChartControlSGraphicsRenderingIsOptimiz,
                viewModelGetter: () => new CartesianChartLargeDataPageViewModel(),
                new VersionInfo(1, 0), new VersionInfo(1, 0), showInWeb: false),

            new(name: "Multiple Axes", title: "Multiple Axes",
                description:
                Resources.ThisExampleDemonstratesACartesianChartCont,
                viewModelGetter: () => new CartesianChartAxesPageViewModel(),
                new VersionInfo(1, 0), new VersionInfo(1, 0)),

            new(name: "Logarithmic Scale", title: "Logarithmic Scale",
                description:
                Resources.ChartControlSupportsLogarithmicScalesForAn,
                viewModelGetter: () => new CartesianChartLogarithmicScalePageViewModel(),
                new VersionInfo(1, 0), new VersionInfo(1, 0)),

            new(name: "Strips and Constant Lines", title: "Strips and Constant Lines",
                description:
                Resources.ThisDemoShowsConstantLinesAndStripsUsedToH,
                viewModelGetter: () => new CartesianStripsAndConstantLinesViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Point", title: "Point Series View",
                description:
                Resources.ThisExampleDemonstratesThePointSeriesViewW,
                viewModelGetter: () => new CartesianPointSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Line", title: "Line Series View",
                description:
                Resources.TheLineSeriesViewShownInThisExampleAllowsY,
                viewModelGetter: () => new CartesianLineSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Area", title: "Area Series View",
                description:
                Resources.TheAreaSeriesViewAllowsYouDisplayFilledAre,
                viewModelGetter: () => new CartesianAreaSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Scatter Line", title: "Scatter Line Series View",
                description:
                Resources.TheScatterLineSeriesViewIsUsefulWhenYouNee,
                viewModelGetter: () => new CartesianScatterLineSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Step Line", title: "Step Line Series View",
                description:
                Resources.ThisExampleDemonstratesTheStepLineSeriesVi,
                viewModelGetter: () => new CartesianStepLineSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Step Area", title: "Step Area Series View",
                description:
                Resources.TheStepAreaSeriesViewConnectsPointsWithHor,
                viewModelGetter: () => new CartesianStepAreaSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Range Area", title: "Range Area Series View",
                description:
                Resources.ADataSeriesInThisExampleContainsTwoYValues,
                viewModelGetter: () => new CartesianRangeAreaSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Side-by-Side Bar", title: "Side-by-Side Bar Series View",
                description:
                Resources.ThisExampleDemonstratesTheSideBySideBarSer,
                viewModelGetter: () => new CartesianSideBySideBarSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Side-by-Side Range Bar", title: "Side-by-Side Range Bar Series View",
                description:
                Resources.ADataSeriesInThisExampleContainsTwoYValues1,
                viewModelGetter: () => new CartesianSideBySideRangeBarSeriesViewViewModel(),
                new VersionInfo(1, 0)),

            new(name: "Candlestick", title: "Candlestick Series View",
                description: Resources.TheCandlestickSeriesViewAllowsYouToCreateA,
                viewModelGetter: () => new CartesianCandlestickSeriesViewViewModel(),
                new VersionInfo(1, 1)),
            
            new(name: "Candlestick Aggregation", title: "Candlestick Aggregation",
                description: Resources.InThisDemoTheCandlestickSeriesViewUsesASpe,
                viewModelGetter: () => new CartesianCandlestickAggregationViewModel(),
                new VersionInfo(1, 1))
        };
    }
}
