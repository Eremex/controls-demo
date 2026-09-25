using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace DemoCenter.DemoData;

public class CarPartInfo
{
    private readonly List<CarInfo> cars = new();
    private readonly List<YachtInfo> yachts = new();
    private IImage image;

    public string PartNumber { get; init; }
    public string Name { get; init; }
    public string Group { get; init; }
    public string Material { get; init; }
    public double Weight { get; init; }
    public string Dimensions { get; init; }
    public decimal Price { get; init; }
    public string Supplier { get; init; }
    public string CountryOfOrigin { get; init; }
    public int LeadTimeDays { get; init; }
    public int Stock { get; init; }
    public int MinimumStock { get; init; }
    public string LifecycleStatus { get; init; }
    public string Revision { get; init; }
    public int WarrantyMonths { get; init; }
    public string Fits { get; init; }
    public string FitsYachts { get; init; }
    public string Description { get; init; }
    public string ImageName { get; init; }

    public IReadOnlyList<string> FitsModels =>
        Fits?.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? Array.Empty<string>();

    public IReadOnlyList<string> FitsYachtModels =>
        FitsYachts?.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? Array.Empty<string>();

    public IReadOnlyList<CarInfo> Cars => cars;

    public IReadOnlyList<YachtInfo> Yachts => yachts;

    internal List<CarInfo> CarList => cars;

    internal List<YachtInfo> YachtList => yachts;

    public bool IsBelowMinimumStock => Stock < MinimumStock;

    public IImage Image
    {
        get
        {
            if (image == null && !string.IsNullOrEmpty(ImageName))
            {
                var uri = new Uri($"avares://DemoCenter/DemoData/csv/CarPartImages/{ImageName}", UriKind.RelativeOrAbsolute);
                if (AssetLoader.Exists(uri))
                {
                    using var stream = AssetLoader.Open(uri);
                    image = new Bitmap(stream);
                }
            }

            return image;
        }
    }
}
