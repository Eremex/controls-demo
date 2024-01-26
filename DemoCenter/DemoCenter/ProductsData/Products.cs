using DemoCenter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.ProductsData
{
    public static class Products
    {
        static List<ProductInfoBase> CreateProducts() => new List<ProductInfoBase>()
        {
            new GroupInfo("Data Grid", "Data Grid", "Data Grid description", () => new DataGridPageViewModel(), DataGridGroupInfo.Create()),
            new GroupInfo("Tree List", "Tree List", "Tree List description", () => new TreeListGroupViewModel(), TreeListGroupInfo.Create()),
            new GroupInfo("Bars and Docking", "Bars and Docking", "Bars and Docking description", () => new BarsGroupViewModel(), BarsGroupInfo.Create()),
            new GroupInfo("Editors", "Editors", "Editors description", () => new EditorsGroupViewModel(), EditorsGroupInfo.Create()),
            new GroupInfo("Property Grid", "Property Grid", "Property Grid description", () => new PropertyGridGroupViewModel(), PropertyGridGroupInfo.Create()),
            new GroupInfo("Common Controls", "Common Controls", "Common Controls description", () => new CommonControlsGroupViewModel(), CommonControlsGroupInfo.Create()),
            new GroupInfo("Standard Controls", "Standard Controls", "Standard Controls description", () => new StandardControlsGroupViewModel(), StandardControlsGroupInfo.Create()),
        };
        static List<ProductInfoBase> products;

        public static List<ProductInfoBase> GetOrCreate()
        {
            if (products == null)
                products = CreateProducts();
            return products;
        }
    }
}
