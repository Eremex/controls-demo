using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
        public string Name { get; init; }
        public double Length { get; init; }
        public int NumberOfCabins { get; init; }
        public double MaxSpeed { get; init; }
        public decimal CruisingRange { get; init; }
        public decimal Price { get; init; }
        public int LaunchingYear { get; init; }
        public string Builder { get; init; }
        public string BuilderWebsite { get; init; }
        public string Designer { get; init; }
        public string Flag { get; init; }

        public YachtInfo(string name, double length, int numberOfCabins, double maxSpeed, decimal cruisingRange, 
            decimal price, int launchingYear, string builder, string designer, string flag)
        {
            Name = name;
            Length = length;
            NumberOfCabins = numberOfCabins;
            MaxSpeed = maxSpeed;
            CruisingRange = cruisingRange;
            Price = price;
            LaunchingYear = launchingYear;
            Builder = builder;
            BuilderWebsite = $"http://www.{Regex.Replace(Builder, "[^A-Za-z]", "")}.com";
            Designer = designer;
            Flag = flag;
        }
    }
}
