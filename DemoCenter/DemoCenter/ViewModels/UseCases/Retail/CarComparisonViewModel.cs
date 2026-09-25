using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;

using Avalonia.Media;

using DemoCenter.DemoData;

namespace DemoCenter.ViewModels;

public class CarSpecCell
{
    public CarSpecCell(string text, bool isBetter, bool isWorse)
    {
        Text = text;
        IsBetter = isBetter;
        IsWorse = isWorse;
    }

    public string Text { get; }

    public bool IsBetter { get; }

    public bool IsWorse { get; }

    public override string ToString() => Text;
}

public class CarSpecs
{
    public CarSpecs(
        IImage image,
        CarSpecCell power,
        CarSpecCell displacement,
        CarSpecCell cylinders,
        CarSpecCell transmission,
        CarSpecCell speeds,
        CarSpecCell fuelEconomy,
        CarSpecCell price,
        CarSpecCell inStock)
    {
        Image = image;
        Power = power;
        Displacement = displacement;
        Cylinders = cylinders;
        Transmission = transmission;
        Speeds = speeds;
        FuelEconomy = fuelEconomy;
        Price = price;
        InStock = inStock;
    }

    public IImage Image { get; }

    public CarSpecCell Power { get; }

    public CarSpecCell Displacement { get; }

    public CarSpecCell Cylinders { get; }

    public CarSpecCell Transmission { get; }

    public CarSpecCell Speeds { get; }

    public CarSpecCell FuelEconomy { get; }

    public CarSpecCell Price { get; }

    public CarSpecCell InStock { get; }
}

public partial class CarComparisonViewModel : PageViewModelBase
{
    static readonly CultureInfo Culture = CultureInfo.CurrentCulture;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LeftSpecs))]
    [NotifyPropertyChangedFor(nameof(RightSpecs))]
    CarInfo leftCar;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LeftSpecs))]
    [NotifyPropertyChangedFor(nameof(RightSpecs))]
    CarInfo rightCar;

    public CarComparisonViewModel()
    {
        Cars = CsvSources.Cars;
        leftCar = Cars.First(x => x.Trademark == "Kestrel Sport Sedan");
        rightCar = Cars.First(x => x.Trademark == "Meridian GT");
    }

    public IList<CarInfo> Cars { get; }

    public CarSpecs LeftSpecs => BuildSpecs(LeftCar, RightCar);

    public CarSpecs RightSpecs => BuildSpecs(RightCar, LeftCar);

    static CarSpecs BuildSpecs(CarInfo car, CarInfo other) => new(
        car.Image,
        Cell(car, other, x => x.HP, x => x.HP.ToString("0", Culture), higherWins: true),
        Cell(car, other, null, x => x.Liter.ToString("0.0", Culture), higherWins: null),
        Cell(car, other, null, x => x.Cyl.ToString("0", Culture), higherWins: null),
        Cell(car, other, x => x.TransmissionType == "Automatic" ? 1 : 0, x => x.TransmissionType, higherWins: true),
        Cell(car, other, x => x.TransmissionSpeedCount, x => x.TransmissionSpeedCount.ToString("0", Culture), higherWins: true),
        Cell(car, other, x => x.MPG, x => x.MPG.ToString("0", Culture), higherWins: true),
        Cell(car, other, x => x.Price, x => x.Price.ToString("c0", Culture), higherWins: false),
        Cell(car, other, x => x.IsInStock ? 1 : 0, x => x.IsInStock ? "Yes" : "No", higherWins: true));

    static CarSpecCell Cell(
        CarInfo car,
        CarInfo other,
        Func<CarInfo, decimal> value,
        Func<CarInfo, string> text,
        bool? higherWins)
    {
        var isBetter = false;
        var isWorse = false;

        if (value != null && higherWins != null && value(car) != value(other))
        {
            isBetter = higherWins.Value ? value(car) > value(other) : value(car) < value(other);
            isWorse = !isBetter;
        }

        return new CarSpecCell(text(car), isBetter, isWorse);
    }
}
