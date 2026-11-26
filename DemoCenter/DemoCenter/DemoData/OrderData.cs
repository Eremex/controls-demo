using Avalonia.Media;

namespace DemoCenter.DemoData
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered
    }

    public class OrderData
    {
        public const int MaxOrdersPerCity = 7;

        static readonly string[] Cities =
        {
            "New York", "Los Angeles", "Chicago", "Houston", "Phoenix",
            "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose",
            "Austin", "Jacksonville", "Fort Worth", "Columbus", "Charlotte",
            "Indianapolis", "San Francisco", "Seattle", "Denver", "Washington",
            "Boston", "El Paso", "Nashville", "Detroit", "Oklahoma City",
            "Portland", "Las Vegas", "Memphis", "Louisville", "Baltimore",
            "Milwaukee", "Albuquerque", "Tucson", "Fresno", "Sacramento",
            "Kansas City", "Atlanta", "Miami", "Raleigh", "Omaha",
            "Colorado Springs", "Virginia Beach", "Long Beach", "Oakland", "Minneapolis",
            "Tulsa", "Bakersfield", "Wichita", "Arlington", "Tampa",
            "Aurora", "New Orleans", "Cleveland", "Anaheim", "Honolulu",
            "Corpus Christi", "Lexington", "Stockton", "St. Paul", "Cincinnati",
            "St. Louis", "Henderson", "Greensboro", "Plano", "Newark",
            "Lincoln", "Orlando", "Irvine", "Toledo", "Riverside",
            "Lubbock", "Madison", "Durham", "Reno", "Boise",
            "Chandler", "Glendale", "Fort Wayne", "Winston-Salem", "Scottsdale",
            "Buffalo", "Des Moines", "Modesto", "Salt Lake City", "Little Rock",
            "Richmond", "Grand Rapids", "Providence", "Spokane", "Tallahassee",
            "Bridgeport", "Oxnard", "Akron", "Montgomery", "Augusta",
            "Shreveport", "Fort Lauderdale", "Fargo", "Charleston", "Savannah"
        };

        static readonly (string Name, decimal BasePrice)[] Products =
        {
            ("Ultrabook 14", 1199m), ("Gaming Laptop 16", 1899m), ("2-in-1 Laptop 13", 999m),
            ("Smartphone X", 799m), ("Smartphone Pro", 1099m), ("Smartphone Lite", 299m),
            ("Tablet 11", 649m), ("Tablet Pro 12.9", 999m),
            ("Noise-Cancelling Headphones", 349m), ("Wireless Earbuds", 179m), ("Bluetooth Speaker", 129m),
            ("Action Camera 4K", 449m), ("Mirrorless Camera", 1299m), ("Webcam 4K", 199m),
            ("Monitor 27 4K", 549m), ("Ultrawide Monitor 34", 899m),
            ("Mechanical Keyboard", 149m), ("Wireless Mouse", 79m), ("USB-C Docking Station", 249m),
            ("External SSD 1TB", 179m), ("Portable Power Bank", 59m),
            ("Smartwatch", 399m), ("Fitness Tracker", 129m)
        };

        static readonly decimal[] Discounts = { 0m, 0m, 0m, 0.05m, 0.05m, 0.1m, 0.15m, 0.2m };

        public static IList<OrderData> GenerateData(int count)
        {
            var random = new Random();
            var data = new List<OrderData>(count);
            var availableCities = new List<string>(Cities);
            var ordersPerCity = new Dictionary<string, int>();
            for (int i = 0; i < count && availableCities.Count > 0; i++)
            {
                int cityIndex = random.Next(availableCities.Count);
                string city = availableCities[cityIndex];
                int ordersInCity = ordersPerCity.TryGetValue(city, out var cityOrders) ? cityOrders + 1 : 1;
                ordersPerCity[city] = ordersInCity;
                if (ordersInCity == MaxOrdersPerCity)
                    availableCities.RemoveAt(cityIndex);

                var product = Products[random.Next(Products.Length)];
                data.Add(new OrderData()
                {
                    OrderId = i,
                    Manager = EmployeesData.EmployeeNames[random.Next(EmployeesData.EmployeeNames.Count)],
                    Product = product.Name,
                    City = city,
                    Date = DateTime.Today.AddDays(-random.Next(730)),
                    Status = (OrderStatus)random.Next(4),
                    Quantity = random.Next(1, 11),
                    Price = Math.Round(product.BasePrice * (0.8m + (decimal)random.NextDouble() * 0.4m)),
                    Discount = Discounts[random.Next(Discounts.Length)]
                });
            }
            return data;
        }

        public int OrderId { get; private set; }

        public string Manager { get; set; }

        public string Product { get; set; }

        public string City { get; set; }

        public DateTime Date { get; set; }

        public OrderStatus Status { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalPrice => Math.Round(Quantity * Price * (1m - Discount), 2);

        public IImage ManagerPhoto => EmployeesData.LoadThumbnailOf(Manager);
    }
}
