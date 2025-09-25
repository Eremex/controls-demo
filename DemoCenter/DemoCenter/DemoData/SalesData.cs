using System;

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

        public decimal Q1Absolute { get; set; }

        public decimal Q1Fraction { get; set; }

        public decimal Q2Absolute { get; set; }

        public decimal Q2Fraction { get; set; }

        public decimal Q3Absolute { get; set; }

        public decimal Q3Fraction { get; set; }

        public decimal Q4Absolute { get; set; }

        public decimal Q4Fraction { get; set; }

        public decimal Total { get; set; }

        public List<SalesData> Children { get; set; }

        public static IList<SalesData> GenerateData()
        {   
            var data = new List<SalesData>();
            var employeeNames = EmployeesData.EmployeeNames.ToList();
            int employeesCount = employeeNames.Count / cityNames.Count;
            foreach (var cityName in cityNames)
            {
                var citySale = new SalesData() { Name = cityName, Children = new List<SalesData>() };
                for (int i = 0; i < employeesCount; i++)
                {   
                    int employeeIndex = Random.Shared.Next(employeeNames.Count);
                    var employeeSale = new SalesData() { Name = employeeNames[employeeIndex] };
                    employeeNames.RemoveAt(employeeIndex);
                    employeeSale.Q1Absolute = GetAbsoluteQuarterSale();
                    employeeSale.Q1Fraction = GetEmployeeFractionQuarterSale(employeeSale.Q1Absolute);
                    employeeSale.Q2Absolute = GetAbsoluteQuarterSale();
                    employeeSale.Q2Fraction = GetEmployeeFractionQuarterSale(employeeSale.Q2Absolute);
                    employeeSale.Q3Absolute = GetAbsoluteQuarterSale();
                    employeeSale.Q3Fraction = GetEmployeeFractionQuarterSale(employeeSale.Q3Absolute);
                    employeeSale.Q4Absolute = GetAbsoluteQuarterSale();
                    employeeSale.Q4Fraction = GetEmployeeFractionQuarterSale(employeeSale.Q4Absolute);
                    employeeSale.Total = employeeSale.Q1Absolute + employeeSale.Q2Absolute + employeeSale.Q3Absolute + employeeSale.Q4Absolute;

                    citySale.Children.Add(employeeSale);
                    citySale.Q1Absolute += employeeSale.Q1Absolute;
                    citySale.Q1Fraction += GetCityFractionQuarterSale(employeeSale.Q1Absolute, employeesCount);
                    citySale.Q2Absolute += employeeSale.Q2Absolute;
                    citySale.Q2Fraction += GetCityFractionQuarterSale(employeeSale.Q2Absolute, employeesCount);
                    citySale.Q3Absolute += employeeSale.Q3Absolute;
                    citySale.Q3Fraction += GetCityFractionQuarterSale(employeeSale.Q3Absolute, employeesCount);
                    citySale.Q4Absolute += employeeSale.Q4Absolute;
                    citySale.Q4Fraction += GetCityFractionQuarterSale(employeeSale.Q4Absolute, employeesCount);
                    citySale.Total += employeeSale.Total;
                }
                data.Add(citySale);
            }
            return data;
        }

        static decimal GetAbsoluteQuarterSale()
        {
            return (decimal)Math.Round(Random.Shared.NextDouble() * 10000, 2);
        }

        static decimal GetEmployeeFractionQuarterSale(decimal employeeSale)
        {
            return (decimal)Math.Round(employeeSale / 100);
        }

        static decimal GetCityFractionQuarterSale(decimal employeeSale, int employeesCount)
        {
            return Math.Round(employeeSale / 100m / employeesCount);
        }
    }
}