using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace DemoCenter.DemoData;

public class CarInfo
{
    private readonly List<CarPartInfo> parts = new();
    private IImage image;
    
    public int Id { get; init; }
    public string Trademark { get; init; }
    public decimal HP { get; init; }
    public decimal Liter { get; init; }
    public decimal Cyl { get; init; }
    public decimal TransmissionSpeedCount { get; init; }
    public string TransmissionType { get; init; }
    public decimal MPG { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public string Currency { get; init; }
    public bool IsInStock { get; init; }
    public string ImageName { get; init; }

    public IReadOnlyList<CarPartInfo> Parts => parts;

    internal List<CarPartInfo> PartList => parts;

    public IImage Image
    {
        get
        {
            if (image == null && !string.IsNullOrEmpty(ImageName))
            {
                var uri = new Uri($"avares://DemoCenter/DemoData/csv/CarImages/{ImageName}", UriKind.RelativeOrAbsolute);
                if (AssetLoader.Exists(uri))
                {
                    using var stream = AssetLoader.Open(uri);
                    image = new Bitmap(stream);
                }
            }

            return image;
        }
    }

    public CarInfo() { }

    public CarInfo(string trademark, CarInfo carInfo)
    {
        Trademark = trademark;
        HP = carInfo.HP;
        Liter = carInfo.Liter;
        Cyl = carInfo.Cyl;
        TransmissionSpeedCount = carInfo.TransmissionSpeedCount;
        TransmissionType = carInfo.TransmissionType;
        MPG = carInfo.MPG;
        Description = carInfo.Description.Replace(carInfo.Trademark, Trademark);
        Price = carInfo.Price;
        Currency = carInfo.Currency;
        IsInStock = carInfo.IsInStock;
        ImageName = carInfo.ImageName;
    }
}