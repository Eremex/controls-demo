using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoCenter.DemoData
{
    public static class EmployeesData
    {
        static List<string> employeeNames = new List<string>()
        {
            "Angelica Grace", "Janet Francis", "Lance Phillips", "Herbert Gilmore", "Jose Horn",
            "Alfred McFarland", "Gwen Chandler", "Julia Case", "Thomas Howell", "Liz McGill",
            "Pat Dudley", "Jodi Funk", "Taylor Monroe", "Vicki Stevenson", "Roman Bridges",
            "Bradley Maloney", "Craig MacDonald", "Cora Cameron", "Katherine Tyler", "Rachel Shah",
            "Earl Lee", "Merle Williamson", "Gene Morse", "Steven Dodd", "Julius Peck",
            "Trevor Chaney", "Lon Schneider", "Julio Hammond", "Rosario Kirby", "Janice Perry" };

        static List<string> cities = new List<string>()
        {
            "New York", "Los Angeles", "Chicago", "Houston", "Phoenix",
            "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose, Calif"
        };

        private static Random random = new Random();

        public static IReadOnlyList<string> Positions { get; } = new List<string>()
        {
            "Accountant", "Sales Representative", "Management", "Project manager", "Sales Management",
            "Human Resources", "Operations management", "Account Manager", "Electrical engineer", "Customer Service",
            "Software Engineer", "Engineer", "Program management", "Administrative assistant", "Financial Analyst"
        };

        public static IList<EmployeeSale> GenerateEmployeeSales()
        {
            var sales = new ObservableCollection<EmployeeSale>();

            for (int i = 5; i > 0; i--)
            {
                foreach (var name in employeeNames)
                {
                    var employee = new EmployeeSale() { Employee = name, Year = 2023 - i + 1 };
                    employee.Quarter1 = GetQuarterSale();
                    employee.Quarter2 = GetQuarterSale();
                    employee.Quarter3 = GetQuarterSale();
                    employee.Quarter4 = GetQuarterSale();
                    employee.Total = employee.Quarter1 + employee.Quarter2 + employee.Quarter3 + employee.Quarter4;
                    sales.Add(employee);
                }
            }

            return sales;
        }

        private static decimal GetQuarterSale()
        {
            return random.Next(90000) + 10000;
        }

        public static IList<EmployeeInfo> GenerateEmployeeInfo()
        {
            var employees = new ObservableCollection<EmployeeInfo>();

            foreach (var employeeName in employeeNames)
            {
                var name = employeeName.Split(' ');
                var employee = new EmployeeInfo() { FirstName = name[0], LastName = name[1] };
                var age = 20 + random.Next(40);
                employee.BirthDate = new DateTime(DateTime.Now.Year - age, random.Next(12) + 1, random.Next(28) + 1);
                employee.HiredAt = new DateTime(DateTime.Now.Year - random.Next(20) - 1, random.Next(12) + 1, random.Next(28) + 1);
                employee.Experience = Math.Max(age - 20 - random.Next(age - 20), DateTime.Now.Year - employee.HiredAt.Year);
                employee.Position = Positions[random.Next(Positions.Count)];
                employee.EmploymentType = (EmploymentType)random.Next(3);
                employee.Married = random.Next(3) != 0;
                employee.City = cities[random.Next(cities.Count)];
                employee.Phone = GetPhoneNumber();
                employees.Add(employee);
            }

            return employees;
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



    public class EmployeeInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime HiredAt { get; set; }

        public int Experience { get; set; }

        public string Position { get; set; }

        public EmploymentType EmploymentType { get; set; }

        public bool Married { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }
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