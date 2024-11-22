using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class HeatmapGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Color Providers", title: "Color Providers",
                description: "A heatmap renders a 2-dimensional array of values as a color-encoded matrix. Each data point's color corresponds to the data point's value. This example demonstrates the Heatmap control which allows you to create heatmaps from your data. Click a color gradient on the right to apply this color encoding to the heatmap.\n\nImage source: Webb Space Telescope, https://webbtelescope.org/",
                viewModelGetter: () => new HeatmapColorProvidersViewModel(),
                new VersionInfo(1, 1)),

            new(name: "Real-Time Data", title: "Real-Time Data",
                description: "In this example the Heatmap control uses custom color encoding to visualize the intensity of a sample signal changing in real time. A chart control at the top displays the amplitude of this signal. The controls update seamlessly as the underlying data changes using a timer.",
                viewModelGetter: () => new HeatmapRealTimeViewModel(),
                new VersionInfo(1, 1), showInWeb: false),
        };
    }
}
