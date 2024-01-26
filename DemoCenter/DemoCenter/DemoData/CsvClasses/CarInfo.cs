using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace DemoCenter.DemoData;

public class CarInfo
{
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

    public IImage Image
    {
        get
        {
            if (image == null)
            {
                if (!string.IsNullOrEmpty(ImageName))
                {
                    var s = AssetLoader.Open(new Uri($"avares://DemoCenter/DemoData/csv/CarImages/{ImageName}", UriKind.RelativeOrAbsolute));
                    image = new Bitmap(s);
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