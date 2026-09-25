using System.Collections;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text.RegularExpressions;

using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.Globalization;
using System.Text;

namespace DemoCenter.DemoData
{
    public static partial class EmployeesData
    {
        public const string PhoneRegex = @"^(\+\d{1,2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";

        static List<string> employeeNames = new List<string>()
        {
            "Angelica Grace", "Janet Francis", "Lance Phillips", "Herbert Gilmore", "Wei Chen",
            "Alfred McFarland", "Gwen Chandler", "Julia Mercer", "Thomas Howell", "Liz McGill",
            "Pat Dudley", "Jodi Funk", "Taylor Monroe", "Lan Zhao", "Roman Bridges",
            "Bradley Maloney", "Craig MacDonald", "Cora Cameron", "Katherine Tyler", "Rachel Shah",
            "Earl Lee", "Merle Williamson", "Gene Morse", "Steven Dodd", "Julius Peck",
            "Trevor Chaney", "Lon Schneider", "Ming Wang", "Rosario Kirby", "Janice Perry", "Lana Carey", 
            "Terrell Wells", "Herbert Hardy", "Thomas Downs", "Audrey Shields", "Davis Buchanan", "Cassie Barron",
            "Kirsten Huff", "Jing Lin", "Lily Alvarado", "Cruz Johns", "Michele Clark", "Jasper Ward",
            "Marcus Ellison", "Denise Lattimore", "Lohmadjon Khan", "Harold Quinn", "Mabel Turner"
        };

        public static List<string> EmployeeNames => employeeNames;

        static List<string> cities = new List<string>()
        {
            "New York", "Los Angeles", "Chicago", "Houston", "Phoenix",
            "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose, Calif"
        };

        private static Random random = new Random();

        public static IReadOnlyList<string> Positions { get; } = new List<string>()
        {
            "Accountant", "Sales Representative", "Manager", "Project Manager", "Sales Manager",
            "HR Manager", "Operations Manager", "Account Manager", "Electrical Engineer", "Customer Service Engineer",
            "Software Engineer", "Engineer", "Program Manager", "Administrative Assistant", "Financial Analyst",
            "Senior Software Engineer", "Backend Developer", "Frontend Developer", "Mobile Developer",
            "DevOps Engineer", "QA Engineer", "Systems Architect", "Database Administrator"
        };

        public static IList<EmployeeSale> GenerateEmployeeSales()
        {
            var sales = new ObservableCollection<EmployeeSale>();

            for (int i = 5; i > 0; i--)
            {
                foreach (var name in employeeNames)
                {
                    var employee = new EmployeeSale() { Employee = name, Year = DateTime.Now.Year - i + 1 };
                    employee.Quarter1 = GetQuarterSalePercent();
                    employee.Quarter2 = GetQuarterSalePercent();
                    employee.Quarter3 = GetQuarterSalePercent();
                    employee.Quarter4 = GetQuarterSalePercent();
                    employee.Total = Math.Round((employee.Quarter1 + employee.Quarter2 + employee.Quarter3 + employee.Quarter4) / 4);
                    sales.Add(employee);
                }
            }

            return sales;
        }

        private static decimal GetQuarterSale()
        {
            return random.Next(90000) + 10000;
        }

        private static decimal GetQuarterSalePercent()
        {
            return Math.Round(GetQuarterSale() / 1000);
        }

        static string GetPhotoName(string employeeName) =>
            PhotoNameRegex().Replace(employeeName, "-").Trim('-') + ".png";

        [GeneratedRegex("[^A-Za-z0-9_-]+")]
        private static partial Regex PhotoNameRegex();

        public static string SignedInUserName => "Katherine Tyler";

        public static IImage SignedInUserPhoto => LoadThumbnailOf(SignedInUserName);

        internal static IImage LoadPhotoOf(string employeeName) =>
            string.IsNullOrEmpty(employeeName) ? null : LoadPhoto(GetPhotoName(employeeName));

        internal static IImage LoadThumbnailOf(string employeeName) =>
            string.IsNullOrEmpty(employeeName) ? null : LoadThumbnail(GetPhotoName(employeeName));

        internal static IImage LoadPhoto(string photoName) => Load("Employees/", photoName);

        internal static IImage LoadThumbnail(string photoName) => Load("Employees/Thumbnails/", photoName);

        static IImage Load(string folder, string photoName)
        {
            if (string.IsNullOrEmpty(photoName))
                return null;

            var key = folder + photoName;
            if (photos.TryGetValue(key, out var cached))
                return cached;

            var uri = new Uri("avares://DemoCenter/DemoData/" + key, UriKind.RelativeOrAbsolute);
            IImage photo = null;

            if (AssetLoader.Exists(uri))
            {
                using var stream = AssetLoader.Open(uri);
                photo = new Bitmap(stream);
            }

            photos[key] = photo;
            return photo;
        }

        static readonly Dictionary<string, IImage> photos = new();

        // The employees whose portrait is a developer: their record has to say so too.
        static readonly HashSet<string> DeveloperNames = new()
        {
            "Herbert Gilmore", "Alfred McFarland", "Thomas Howell", "Craig MacDonald", "Earl Lee",
            "Julius Peck", "Herbert Hardy", "Davis Buchanan", "Jasper Ward"
        };

        static readonly string[] DeveloperPositions =
        {
            "Senior Software Engineer", "Backend Developer", "Frontend Developer", "Mobile Developer",
            "DevOps Engineer", "QA Engineer", "Systems Architect", "Database Administrator",
            "Software Engineer"
        };

        public static IList<EmployeeInfo> GenerateEmployeeInfo()
        {
            var employees = new ObservableCollection<EmployeeInfo>();

            foreach (var employeeName in employeeNames)
            {
                var name = employeeName.Split(' ');
                var employee = new EmployeeInfo() { FirstName = name[0], LastName = name[1] };
                var age = 20 + random.Next(40);
                employee.BirthDate = new DateTime(DateTime.Now.Year - age, random.Next(12) + 1, random.Next(28) + 1);
                employee.HireDate = new DateTime(DateTime.Now.Year - random.Next(20) - 1, random.Next(12) + 1, random.Next(28) + 1);
                employee.Experience = Math.Max(age - 20 - random.Next(age - 20), DateTime.Now.Year - employee.HireDate.Year);
                employee.Position = Positions[random.Next(Positions.Count)];
                employee.EmploymentType = (EmploymentType)random.Next(3);
                employee.Married = random.Next(3) != 0;
                employee.City = cities[random.Next(cities.Count)];
                employee.Phone = GetPhoneNumber();
                employee.PhotoName = GetPhotoName(employeeName);
                employee.Notes = string.Empty;

                if (DeveloperNames.Contains(employeeName))
                    employee.Position = DeveloperPositions[employees.Count(x => DeveloperNames.Contains($"{x.FirstName} {x.LastName}")) % DeveloperPositions.Length];

                employees.Add(employee);
            }

            return employees;
        }

        public static IList<EmployeeValidationInfo> GenerateValidationEmployeeInfo()
        {            
            Random rnd = new();

            var employees = GenerateEmployeeInfo();

            GetRandomEmployee(rnd, employees).FirstName = string.Empty;
            GetRandomEmployee(rnd, employees).LastName = string.Empty;
            GetRandomEmployee(rnd, employees).FirstName = string.Empty;
            GetRandomEmployee(rnd, employees).LastName = string.Empty;
            GetRandomEmployee(rnd, employees).City = string.Empty;
            GetRandomEmployee(rnd, employees).City = string.Empty;
            GetRandomEmployee(rnd, employees).City = string.Empty;
            GetRandomEmployee(rnd, employees).Phone = "123 456";
            GetRandomEmployee(rnd, employees).Phone = "(2994)345-235";
            GetRandomEmployee(rnd, employees).Phone = "(574)786";
            GetRandomEmployee(rnd, employees).BirthDate = DateTime.Now.AddDays(10);
            GetRandomEmployee(rnd, employees).HireDate = DateTime.Now.AddDays(4);
            
            var employee = GetRandomEmployee(rnd, employees);
            employee.HireDate = employee.BirthDate - TimeSpan.FromDays(10);
            employee.Experience = 0;

            return employees.Select(x => new EmployeeValidationInfo(x)).ToList();

            EmployeeInfo GetRandomEmployee(Random rnd, IList<EmployeeInfo> employees)
            {
                return employees[rnd.Next(20)];
            }
        }

        private static string GetPhoneNumber()
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                stringBuilder.Append(random.Next(10));
            }
            var phone = stringBuilder.ToString();
            return $"({phone.Substring(0, 3)}) {phone.Substring(3, 3)}-{phone.Substring(6, 4)}";
        }

        public static IList GenerateComplexEmployeeSales()
        {
            var sales = new ObservableCollection<object>();
            foreach (var name in employeeNames.Order())
            {
                IDictionary<string, object> employeeSale = new ExpandoObject();
                employeeSale["Employee"] = name;
                decimal total = 0;
                for (int i = 1; i < 4; i++)
                {
                    var year = DateTime.Now.Year - i;
                    decimal yearTotal = 0;
                    for (int j = 0; j < 12; j++)
                    {   
                        var sale = GetQuarterSale();
                        yearTotal += sale;
                        var monthName = CultureInfo.InvariantCulture.DateTimeFormat.GetAbbreviatedMonthName(j + 1);
                        var propertyName = $"{year}/Q{j / 3 + 1}/{monthName}";
                        employeeSale[propertyName] = sale;
                    }
                    employeeSale[$"{year}/Total"] = yearTotal;
                    total += yearTotal;
                }
                employeeSale["Total"] = total;
                sales.Add(employeeSale);
            }

            return sales;
        }
    }

    public class EmployeeSale
    {
        public string Employee { get; set; }

        public int Year { get; set; }

        public decimal Quarter1 { get; set; }

        public decimal Quarter2 { get; set; }

        public decimal Quarter3 { get; set; }

        public decimal Quarter4 { get; set; }

        public decimal Total { get; set; }
    }

    public partial class EmployeeInfo : ObservableObject
    {
        IImage photo;
        IImage thumbnail;

        [ObservableProperty]
        string firstName;

        [ObservableProperty]
        string lastName;

        [ObservableProperty]
        DateTime birthDate;

        [ObservableProperty]
        DateTime hireDate;

        [ObservableProperty]
        int experience;

        [ObservableProperty]
        string position;

        [ObservableProperty]
        EmploymentType employmentType;

        [ObservableProperty]
        bool married;

        [ObservableProperty]
        string city;

        [ObservableProperty]
        string phone;

        [ObservableProperty]
        string notes;

        // Hidden from auto-generated rows and columns; the pictures themselves stay browsable.
        [property: Browsable(false)]
        [ObservableProperty]
        string photoName;

        public IImage Photo => photo ??= EmployeesData.LoadPhoto(PhotoName);

        public IImage PhotoThumbnail => thumbnail ??= EmployeesData.LoadThumbnail(PhotoName);
    }

    public partial class EmployeeValidationInfo : ObservableObject
    {
        static readonly DateTime MinDate = new DateTime(1900, 1, 1);

        IImage photo;
        IImage thumbnail;

        public EmployeeValidationInfo(EmployeeInfo employee)
        {
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            BirthDate = employee.BirthDate;
            HireDate = employee.HireDate;
            Experience = employee.Experience;   
            City = employee.City;
            Phone = employee.Phone;
            PhotoName = employee.PhotoName;
        }

        [property: Required]
        [ObservableProperty]
        string firstName;

        [property: Required]
        [ObservableProperty]
        string lastName;

        [property: CustomValidation(typeof(EmployeeValidationInfo), nameof(ValidateDate))]
        [ObservableProperty]
        DateTime birthDate;

        [property: CustomValidation(typeof(EmployeeValidationInfo), nameof(ValidateDate))]
        [ObservableProperty]
        DateTime hireDate;

        [property: Required]
        [ObservableProperty]
        int experience;

        [property: Required]
        [ObservableProperty]
        string city;

        [property: RegularExpression(EmployeesData.PhoneRegex, ErrorMessage = "The phone number is not valid")]
        [ObservableProperty]
        string phone;

        [property: Browsable(false)]
        [ObservableProperty]
        string photoName;

        public IImage Photo => photo ??= EmployeesData.LoadPhoto(PhotoName);

        public IImage PhotoThumbnail => thumbnail ??= EmployeesData.LoadThumbnail(PhotoName);

        public static ValidationResult ValidateDate(DateTime date)
        {
            if(date > DateTime.Today)
                return new ValidationResult("The date cannot be in the future.");
            if (date < MinDate)
                return new ValidationResult($"The date cannot be less than {MinDate:d}");
            return ValidationResult.Success; 
        } 
    }

    public enum EmploymentType
    {
        [Display(Name = "Full Time")]
        FullTime,
        [Display(Name = "Part Time")]
        PartTime,
        Contract
    }
}