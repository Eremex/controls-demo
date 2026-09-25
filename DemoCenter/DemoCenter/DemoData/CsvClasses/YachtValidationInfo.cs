using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Avalonia.Media;

using CommunityToolkit.Mvvm.ComponentModel;

namespace DemoCenter.DemoData;

public partial class YachtValidationInfo : ObservableObject
{
    const int FirstYear = 1900;

    IImage image;

    public YachtValidationInfo(YachtInfo yacht)
    {
        name = yacht.Name;
        builder = yacht.Builder;
        designer = yacht.Designer;
        length = yacht.Length;
        numberOfCabins = yacht.NumberOfCabins;
        maxSpeed = yacht.MaxSpeed;
        cruisingRange = yacht.CruisingRange;
        price = yacht.Price;
        launchingYear = yacht.LaunchingYear;
        refitYear = yacht.LaunchingYear + 4;
        flag = yacht.Flag;
        location = yacht.Location;
        imageName = yacht.ImageName;
    }

    [property: Required]
    [ObservableProperty]
    string name;

    [property: Required]
    [ObservableProperty]
    string builder;

    [property: Required]
    [ObservableProperty]
    string designer;

    [property: Range(20, 400, ErrorMessage = "Length must be between 20 and 400 feet")]
    [ObservableProperty]
    double length;

    [property: Range(1, 20, ErrorMessage = "A yacht has between 1 and 20 cabins")]
    [ObservableProperty]
    int numberOfCabins;

    [property: Range(5, 60, ErrorMessage = "Maximum speed must be between 5 and 60 knots")]
    [ObservableProperty]
    double maxSpeed;

    [property: Range(100, 12000, ErrorMessage = "Cruising range must be between 100 and 12000 nautical miles")]
    [ObservableProperty]
    decimal cruisingRange;

    [property: Range(typeof(decimal), "100000", "500000000", ErrorMessage = "The price is outside the expected range")]
    [ObservableProperty]
    decimal price;

    [property: CustomValidation(typeof(YachtValidationInfo), nameof(ValidateYear))]
    [ObservableProperty]
    int launchingYear;

    [property: CustomValidation(typeof(YachtValidationInfo), nameof(ValidateYear))]
    [ObservableProperty]
    int refitYear;

    [property: Required]
    [ObservableProperty]
    string flag;

    [property: Required]
    [ObservableProperty]
    string location;

    [property: Browsable(false)]
    [ObservableProperty]
    string imageName;

    public IImage Image => image ??= YachtInfo.LoadImage(ImageName);

    public static ValidationResult ValidateYear(int year)
    {
        if (year > DateTime.Today.Year)
            return new ValidationResult("The year cannot be in the future.");
        if (year < FirstYear)
            return new ValidationResult($"The year cannot be less than {FirstYear}.");

        return ValidationResult.Success;
    }
}
