using Avalonia;
using Avalonia.Platform;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace DemoCenter.DemoData
{
    public static class CsvSources
    {
        static List<CarInfo> cars;
        public static List<CarInfo> Cars
        {
            get
            {
                cars ??= GetCars();
                return cars;
            }
        }

        static List<MechInfo> mechs;
        public static List<MechInfo> Mechs
        {
            get
            {
                mechs ??= GetMechs();
                return mechs;
            }
        }

        static List<SpaceLaunchInfo> launches;
        public static List<SpaceLaunchInfo> Launches
        {
            get
            {
                launches ??= GetLaunches();
                return launches;
            }
        }

        static List<string> yachtNames;
        public static List<string> YachtNames
        {
            get
            {
                yachtNames ??= GetYachtNames();
                return yachtNames;
            }
        }

        static List<YachtInfo> yachts;
        public static List<YachtInfo> Yachts
        {
            get
            {
                yachts ??= GetYachts();
                return yachts;
            }
        }
        
        static List<CarInfo> GetCars()
        {
            var culture = new CultureInfo("en-US");
            
            return GetInfo<CarInfo>(GetUriString("cars"), GetCarInfo);

            CarInfo GetCarInfo(string[] values)
            {
                return new CarInfo
                {
                    Id = int.Parse(values[0], culture),
                    Trademark = values[1],
                    HP = decimal.Parse(values[2], culture),
                    Liter = decimal.Parse(values[3], culture),
                    Cyl = decimal.Parse(values[4], culture),
                    TransmissionSpeedCount = decimal.Parse(values[5]),
                    TransmissionType = values[6],
                    MPG = decimal.Parse(values[7], culture),
                    Description = values[8],
                    Price = decimal.Parse(values[9], NumberStyles.Currency, culture),
                    Currency = culture.NumberFormat.CurrencySymbol,
                    IsInStock = bool.Parse(values[10]),
                    ImageName = values[11]
                };
            }
        }

        static List<MechInfo> GetMechs()
        {
            var regex = new Regex(@"\d{1,}");
            return GetInfo<MechInfo>(GetUriString("mechs"), 
                values => new MechInfo(values[0], int.Parse(regex.Match(values[1]).Value), new List<string>() { values[2], values[3], values[4] }));
        }

        static List<SpaceLaunchInfo> GetLaunches()
        {
            var provider = CultureInfo.InvariantCulture;
            return GetInfo<SpaceLaunchInfo>(GetUriString("spacexlaunches"),
                values => 
                { 
                    var launchDate = DateTime.ParseExact(values[0] + "," + values[1], "MMMM d, yyyy", provider);
                    return new SpaceLaunchInfo(launchDate, values[2].TrimStart(), values[3].TrimStart()); 
                });
        }
        static List<string> GetYachtNames() => GetInfo<string>(GetUriString("yachtNames"), values => values[0]);

        static List<YachtInfo> GetYachts()
        {
            var provider = CultureInfo.InvariantCulture;
            return GetInfo<YachtInfo>(GetUriString("yachts"),
                values =>
                {
                    var price = 1000000m* decimal.Parse(values[5]) + 1000m * decimal.Parse(values[6]) + decimal.Parse(values[7]);
                    return new YachtInfo(values[0], double.Parse(values[1]), int.Parse(values[2]), double.Parse(values[3]), decimal.Parse(values[4]), price, int.Parse(values[8]), values[9], values[10], values[11]);
                });
        }

        static List<T> GetInfo<T>(string uriString, Func<string[], T> getInfo)
        {
            var result = new List<T>();
            var uri = new Uri(uriString);

            if (!AssetLoader.Exists(uri))
                return result;

            using (var reader = new StreamReader(AssetLoader.Open(uri)))
            {
                reader.ReadLine();

                using (var parser = new TextFieldParser(reader))
                {
                    parser.HasFieldsEnclosedInQuotes = true;
                    parser.Delimiters = new[] { "," };
                    while (!parser.EndOfData)
                    {
                        string[] values = parser.ReadFields();
                        result.Add(getInfo(values));
                    }
                }
            }

            return result;
        }

        static string GetUriString(string path) => $"avares://DemoCenter/DemoData/csv/{path}.csv";
    }
}
