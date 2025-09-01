namespace DemoCenter.DemoData
{
    public class ApparelProduct
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Style { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsNewArrival { get; set; }
        public bool IsBestSeller { get; set; }
    }

    public class ApparelProducts
    {
        private static readonly string[] ClothingBrands = { "Urban Wear", "Style Vista", "Thread Nation", "Cotton Bloom", "Denim Haven", "Silk Route", "Linen Legend", "Vogue Venture" };

        private static readonly string[] FootwearBrands = { "Sole Superior", "Prestige Steps", "Elite Treads", "Urban Stride", "Lux Walk", "Aristo Feet", "Prime Pace", "Noble Heels" };

        private static readonly string[] ClothingCategories = { "Men's T-Shirts", "Women's Dresses", "Jeans", "Jackets", "Sweaters", "Shirts", "Skirts", "Shorts", "Activewear", "Underwear" };

        private static readonly string[] FootwearCategories = { "Sneakers", "Boots", "Sandals", "Loafers", "High Heels", "Athletic Shoes", "Oxfords", "Slip-ons", "Flip Flops", "Espadrilles" };

        private static readonly string[] Styles = { "Casual", "Sport", "Formal", "Business", "Street", "Vintage", "Minimalist", "Grunge" };

        private static readonly string[] Colors = { "Black", "White", "Navy", "Charcoal", "Beige", "Khaki", "Burgundy", "Olive", "Denim Blue", "Cream", "Camel", "Rust", "Terracotta", "Moss Green", "Blush Pink", "Lavender", "Mustard", "Teal", "Coral", "Eggplant" };

        private static readonly string[] ProductNameTemplates = { "{0} {1}", "{0} {1} {2}", "{1} by {0}", "{0} - {1}" };

        private static readonly string[] ProductNameModifiers = { "Classic", "Modern", "Essential", "Pro", "Basic", "Standard", "Premium", "Lite", "Air", "Comfort" };

        public static IList<ApparelProduct> GenerateData(int count)
        {
            var random = new Random();
            var products = new List<ApparelProduct>();
            var startDate = DateTime.Now.AddYears(-1);

            for (int i = 0; i < count; i++)
            {
                bool isClothing = random.Next(2) == 0;
                var brand = isClothing ? ClothingBrands[random.Next(ClothingBrands.Length)] : FootwearBrands[random.Next(FootwearBrands.Length)];

                var category = isClothing ? "Clothing" : "Footwear";
                var subCategory = isClothing ? ClothingCategories[random.Next(ClothingCategories.Length)] : FootwearCategories[random.Next(FootwearCategories.Length)];

                var isNewArrival = random.Next(10) == 0;
                var isBestSeller = random.Next(5) == 0;

                var product = new ApparelProduct
                {
                    Name = GenerateProductName(brand, subCategory, i),
                    Brand = brand,
                    Category = category,
                    SubCategory = subCategory,
                    Style = Styles[random.Next(Styles.Length)],
                    Color = Colors[random.Next(Colors.Length)],
                    Size = GenerateSize(category, subCategory),
                    Price = (decimal)Math.Round(15 + random.NextDouble() * 200, 2),
                    Stock = random.Next(0, 500),
                    ReleaseDate = isNewArrival ? DateTime.Now.AddDays(-random.Next(30)) : startDate.AddDays(random.Next(365)),
                    IsNewArrival = isNewArrival,
                    IsBestSeller = isBestSeller,
                };

                products.Add(product);
            }

            return products;

            string GenerateSize(string category, string subCategory)
            {
                if (category == "Clothing")
                {
                    return subCategory switch
                    {
                        "Men's T-Shirts" or "Shirts" => new[] { "S", "M", "L", "XL", "XXL" }[random.Next(5)],
                        "Women's Dresses" or "Skirts" => new[] { "XS", "S", "M", "L", "XL" }[random.Next(5)],
                        "Jeans" => $"{random.Next(26, 36)}W/{random.Next(30, 34)}L",
                        _ => "One Size"
                    };
                }
                else
                {
                    return subCategory switch
                    {
                        "High Heels" => (35 + random.Next(6)).ToString(),
                        "Boots" => (36 + random.Next(7)).ToString(),
                        _ => (38 + random.Next(8)).ToString()
                    };
                }
            }

            string GenerateProductName(string brand, string subCategory, int index)
            {
                string template = ProductNameTemplates[random.Next(ProductNameTemplates.Length)];
                string modifier = ProductNameModifiers[random.Next(ProductNameModifiers.Length)];

                return string.Format(template, brand, subCategory, modifier);
            }
        }
    }
}
