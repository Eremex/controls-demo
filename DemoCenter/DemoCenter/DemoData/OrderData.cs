using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoCenter.DemoData
{
    public class OrderData
    {
        public static IList<OrderData> GenerateData(int count)
        {
            var random = new Random();
            var data = new List<OrderData>(count);
            for (int i = 0; i < count; i++)
            {
                data.Add(new OrderData()
                {
                    OrderId = i,
                    Manager = EmployeesData.EmployeeNames[random.Next(EmployeesData.EmployeeNames.Count)],
                    Count = random.Next(1000),
                    Price = random.Next(1000),
                    Date = DateTime.Today.AddDays(random.Next(200) - 100)
                });
            }
            return data;
        }

        public int OrderId { get; private set; }

        public string Manager { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice => Count * Price;

        public DateTime Date { get; set; }
    }
}