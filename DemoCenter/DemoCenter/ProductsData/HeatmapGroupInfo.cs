using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class HeatmapGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Color Providers", title: "Color Providers",
                viewModelGetter: () => new HeatmapColorProvidersViewModel(),
                descriptionGetter: () => Resources.AHeatmapRendersA2DimensionalArrayOfValuesA, introduced: new VersionInfo(1, 1)),

            new(name: "Real-Time Data", title: "Real-Time Data",
                viewModelGetter: () => new HeatmapRealTimeViewModel(),
                descriptionGetter: () => Resources.InThisExampleTheHeatmapControlUsesCustomCo, introduced: new VersionInfo(1, 1), showInWeb: false),
        };
    }
}
