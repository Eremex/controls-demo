using Avalonia.Platform;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;
using DemoCenter.DemoData.CsvClasses;

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

        static List<StockProduct> stockProducts;
        public static List<StockProduct> StockProducts
        {
            get
            {
                stockProducts ??= GetStockProducts();
                return stockProducts;
            }
        }

        static List<CsvDoubleColumn> logarithmic;
        public static List<CsvDoubleColumn> Logarithmic => logarithmic ??= GetLogarithmic();

        static List<StockInfo> stock;
        public static List<StockInfo> Stock => stock ??= GetStock();
       
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
        static List<CsvDoubleColumn> GetLogarithmic() => GetColumnInfo<CsvDoubleColumn>(GetUriString("logarithmic"));
        static List<StockInfo> GetStock() => GetInfo(GetUriString("Bitcoin Historical Data"),
            values =>
            {
                var date = DateTime.Parse(values[0], CultureInfo.InvariantCulture);
                var close = double.Parse(values[1], CultureInfo.InvariantCulture);
                var open = double.Parse(values[2], CultureInfo.InvariantCulture);
                var high = double.Parse(values[3], CultureInfo.InvariantCulture);
                var low = double.Parse(values[4], CultureInfo.InvariantCulture);
                var volume = double.Parse(values[5].Substring(0, values[5].Length - 1), CultureInfo.InvariantCulture) * 1000;
                return new StockInfo(date, close, open, high, low, volume);
            });

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
        static List<T> GetColumnInfo<T>(string uriString) where T : CsvColumn
        {
            const char separator = ',';
            
            var result = new List<T>();
            var uri = new Uri(uriString);

            if (!AssetLoader.Exists(uri))
                return result;

            using (var reader = new StreamReader(AssetLoader.Open(uri)))
            {
                string[] headers = reader.ReadLine()!.Split(separator);
                foreach (string header in headers)
                    result.Add((T)Activator.CreateInstance(typeof(T), header.Trim()));

                using (var parser = new TextFieldParser(reader))
                {
                    parser.HasFieldsEnclosedInQuotes = false;
                    parser.Delimiters = new[] { separator.ToString() };
                    while (!parser.EndOfData)
                    {
                        string[] values = parser.ReadFields();
                        if (values != null)
                        {
                            for(int i = 0; i < result.Count; i++)
                                result[i].AddValue(values[i]);
                        }
                    }
                }
            }

            return result;
        }

        static List<StockProduct> GetStockProducts()
        {
            return GetInfo<StockProduct>(GetUriString("stockProducts"), GetStockProduct);

            StockProduct GetStockProduct(string[] values)
            {
                return new StockProduct()
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                    Color = values[2],
                    Size = values[3],
                    Category = values[4],
                    Cost = decimal.Parse(values[5]),
                    Quantity = int.Parse(values[6]),
                };
            }
        }

        static string GetUriString(string path) => $"avares://DemoCenter/DemoData/csv/{path}.csv";
    }
}
