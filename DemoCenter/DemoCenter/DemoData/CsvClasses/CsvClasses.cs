using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace DemoCenter.DemoData
{
    public class MechInfo
    {
        public string Name {  get; init; }
        public double Weight { get; init; }
        public List<string> Weapons { get; init; }

        public MechInfo(string name, int weight, List<string> weapons) 
        {
            Name = name;
            Weight = weight;
            Weapons = weapons;
        }
    }

    public class SpaceLaunchInfo
    {
        public DateTime Date { get; init; }
        public string MissionName { get; init; }
        public string LaunchSite { get; init; }

        public SpaceLaunchInfo(DateTime date, string missionName, string launchSite)
        {
            Date = date;
            MissionName = missionName;
            LaunchSite = launchSite;
        }
    }

    public class YachtInfo
    {
        private readonly List<CarPartInfo> parts = new();
        private IImage image;

        public string Name { get; init; }
        public double Length { get; init; }
        public int NumberOfCabins { get; init; }
        public double MaxSpeed { get; init; }
        public decimal CruisingRange { get; init; }
        public decimal Price { get; init; }
        public int LaunchingYear { get; init; }
        public string Builder { get; init; }
        public string Designer { get; init; }
        public string Flag { get; init; }
        public string Location { get; init; }
        public string Description { get; init; }
        public string ImageName { get; init; }
        public YachtWebInfo Details { get; init; }

        public IReadOnlyList<CarPartInfo> Parts => parts;

        internal List<CarPartInfo> PartList => parts;

        public IImage Image => image ??= LoadImage(ImageName);

        internal static IImage LoadImage(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
                return null;

            var uri = new Uri($"avares://DemoCenter/DemoData/csv/YachtImages/{imageName}", UriKind.RelativeOrAbsolute);
            if (!AssetLoader.Exists(uri))
                return null;

            using var stream = AssetLoader.Open(uri);
            return new Bitmap(stream);
        }

        public YachtInfo(string name, double length, int numberOfCabins, double maxSpeed, decimal cruisingRange, 
            decimal price, int launchingYear, string builder, string designer, string flag, string location,
            string description, string imageName)
        {
            Name = name;
            Length = length;
            NumberOfCabins = numberOfCabins;
            MaxSpeed = maxSpeed;
            CruisingRange = cruisingRange;
            Price = price;
            LaunchingYear = launchingYear;
            Builder = builder;
            Designer = designer;
            Flag = flag;
            Location = location;
            Description = description;
            ImageName = imageName;
            Details = new YachtWebInfo(Name, Location, $"https://www.google.com/search?q=yacht+{Name.Replace(" ", "+")}");
        }
    }

    public class YachtWebInfo
    {
        public string Header { get; init; }
        public string Link { get; init; }

        public YachtWebInfo(string name, string location, string link)
        {
            Header = $"{name}, {location}";
            Link = link;
        }
    }
}
