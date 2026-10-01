using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.ProductsData;

namespace DemoCenter.ViewModels;

public partial class PropertyGridNestedPropertiesViewModel : PageViewModelBase
{
    [ObservableProperty]
    object selectedObject;

    public PropertyGridNestedPropertiesViewModel()
    {
        SelectedObject = new ComplexOrder()
        {
            OrderID = "ORD-2023-001",
            OrderDate = DateTime.Now.Date,
            Customer = new Customer()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                ShippingAddress = new ShippingAddress()
                {
                    Street = "123 Main St",
                    City = "Springfield",
                    GeoLocation = new GeoLocation()
                    {
                        Latitude = 40.7128,
                        Longitude = -74.0060
                    }
                }
            },
            Item_1 = new OrderItem()
            {
                SKU = "SKU-12345",
                Quantity = 1,
                Pricing = new Pricing()
                {
                    UnitPrice = 19.99m,
                    Discount = new Discount()
                    {
                        Amount = 10.0m
                    }
                }
            }
        };
    }
}
