namespace DemoCenter.DemoData
{
    public class SalesData
    {
        static List<string> cityNames = new List<string>()
        {
            "New York", "Los Angeles", "Chicago", "Houston", 
            "Phoenix", "Philadelphia", "San Francisco", "Las Vegas"
        };

        public string Name { get; set; }

        public double Q1 { get; set; }

        public double Q2 { get; set; }

        public double Q3 { get; set; }

        public double Q4 { get; set; }

        public double Total { get; set; }

        public List<SalesData> Children { get; set; }

        public static IList<SalesData> GenerateData()
        {
            var random = new Random();
            var data = new List<SalesData>();
            var employeeNames = EmployeesData.EmployeeNames.ToList();
            double employeesCount = employeeNames.Count / cityNames.Count;
            foreach (var cityName in cityNames)
            {
                var citySale = new SalesData() { Name = cityName, Children = new List<SalesData>() };
                for (int i = 0; i < employeesCount; i++)
                {   
                    int employeeIndex = random.Next(employeeNames.Count);
                    var employeeSale = new SalesData() { Name = employeeNames[employeeIndex] };
                    employeeNames.RemoveAt(employeeIndex);
                    employeeSale.Q1 = random.NextDouble() * 10000;
                    employeeSale.Q2 = random.NextDouble() * 10000;
                    employeeSale.Q3 = random.NextDouble() * 10000;
                    employeeSale.Q4 = random.NextDouble() * 10000;
                    employeeSale.Total = employeeSale.Q1 + employeeSale.Q2 + employeeSale.Q3 + employeeSale.Q4;

                    citySale.Children.Add(employeeSale);
                    citySale.Q1 += employeeSale.Q1;
                    citySale.Q2 += employeeSale.Q2;
                    citySale.Q3 += employeeSale.Q3;
                    citySale.Q4 += employeeSale.Q4;
                    citySale.Total += employeeSale.Total;
                }
                data.Add(citySale);
            }
            return data;
        }
    }
}