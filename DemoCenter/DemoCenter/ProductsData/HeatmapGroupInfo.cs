using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class HeatmapGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Color Providers", title: "Color Providers",
                description: Resources.AHeatmapRendersA2DimensionalArrayOfValuesA,
                viewModelGetter: () => new HeatmapColorProvidersViewModel(),
                new VersionInfo(1, 1)),

            new(name: "Real-Time Data", title: "Real-Time Data",
                description: Resources.InThisExampleTheHeatmapControlUsesCustomCo,
                viewModelGetter: () => new HeatmapRealTimeViewModel(),
                new VersionInfo(1, 1), showInWeb: false),
        };
    }
}
