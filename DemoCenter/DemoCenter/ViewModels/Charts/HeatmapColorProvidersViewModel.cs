using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using AvaloniaEdit.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.Helpers;
using Eremex.AvaloniaUI.Charts;
using Eremex.AvaloniaUI.Charts.Native;

namespace DemoCenter.ViewModels;

public partial class HeatmapColorProvidersViewModel : ChartsPageViewModel
{
    [ObservableProperty] HeatmapDataAdapter dataAdapter = HeatmapHelper.GetAdapter("hlsp_jwst-ero_jwst_miri_carina_f1130w_v1_i2d");
    [ObservableProperty] HeatmapColorProviderItem selectedColorProvider;
    public string MiddleX => DataAdapter.XArguments[DataAdapter.XArguments.Count / 2];
    public string MiddleY => DataAdapter.YArguments[DataAdapter.YArguments.Count / 2];
    public List<HeatmapColorProviderItem> ColorProviders { get; } = new()
    {
        new("Hot Spot", new HeatmapRangeStop[]
        {
            new() { Value = 0.0, Color = Color.FromRgb(0, 0, 0) },
            new() { Value = 0.6, Color = Color.FromRgb(160, 160, 160) },
            new() { Value = 0.65, Color = Color.FromRgb(238, 137, 17) },
            new() { Value = 0.8, Color = Color.FromRgb(242, 36, 0) },
            new() { Value = 1.0, Color = Color.FromRgb(160, 20, 0) }
        }),

        new("Rainbow", new HeatmapRangeStop[]
        {
            new() { Value = 0.0, Color = Color.FromRgb(0, 8, 73) },
            new() { Value = 0.2, Color = Color.FromRgb(0, 118, 212) },
            new() { Value = 0.4, Color = Color.FromRgb(64, 171, 57) },
            new() { Value = 0.6, Color = Color.FromRgb(241, 197, 9) },
            new() { Value = 0.8, Color = Color.FromRgb(247, 38, 63) },
            new() { Value = 1.0, Color = Color.FromRgb(250, 226, 202) }
        }),

        new(),

        new("Green", new HeatmapRangeStop[]
        {
            new() { Value = 0.0, Color = Colors.Black },
            new() { Value = 1.0, Color = Colors.Lime }
        }),
        
        new("Hot Metal", new HeatmapRangeStop[]
        {
            new() { Value = 0.0, Color = Color.FromRgb(0, 0, 12) },
            new() { Value = 0.2, Color = Color.FromRgb(74, 25, 144) },
            new() { Value = 0.4, Color = Color.FromRgb(187, 38, 143) },
            new() { Value = 0.6, Color = Color.FromRgb(230, 77, 12) },
            new() { Value = 0.8, Color = Color.FromRgb(248, 218, 12) },
            new() { Value = 1.0, Color = Color.FromRgb(248, 251, 245) }
        }),

        new("Cold Spot", new HeatmapRangeStop[]
        {
            new() { Value = 0.0, Color = Color.FromRgb(1, 33, 184) },
            new() { Value = 0.2, Color = Color.FromRgb(46, 168, 220) },
            new() { Value = 0.25, Color = Color.FromRgb(155, 155, 155) },
            new() { Value = 1.0, Color = Color.FromRgb(0, 0, 0) }
        })
    };

    public HeatmapColorProvidersViewModel()
    {
        SelectedColorProvider = ColorProviders[4];
    }
}

public partial class HeatmapColorProviderItem : ObservableObject
{
    public string Name { get; }
    public IHeatmapColorProvider ColorProvider { get; }
    public IImage PreviewImage { get; }

    public HeatmapColorProviderItem()
    {
        Name = "Gray";
        ColorProvider = new HeatmapGrayscaleColorProvider();
        PreviewImage = UpdatePreview();
    }
    public HeatmapColorProviderItem(string name, IEnumerable<HeatmapRangeStop> rangeStops)
    {
        Name = name;
        var provider = new HeatmapRangeColorProvider { IsNormalizedValues = true };
        provider.AddRange(rangeStops);
        ColorProvider = provider;
        PreviewImage = UpdatePreview();
    }
    IImage UpdatePreview()
    {
        const int width = 200;
        const int height = 8;
        var bitmap = new RenderTargetBitmap(new PixelSize(width, height), new Vector(96, 96));
        using var context = bitmap.CreateDrawingContext();
        var range = new MinMaxValues(0, width);
        for (int i = 0; i < width; i++)
        {
            var brush = new SolidColorBrush(ColorProvider.GetColor(i, range));
            context.FillRectangle(brush, new Rect(i, 0, i + 1, height));
        }
        return bitmap;
    }
} 
