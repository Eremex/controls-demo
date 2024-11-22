using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.DemoData.CsvClasses
{
    public class StockProduct
    {
        public int Id { get; set; }
       
        public string Name { get; set; }

        public string Category { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public decimal Cost { get; set; }

        public int Quantity { get; set; }
    }
}
