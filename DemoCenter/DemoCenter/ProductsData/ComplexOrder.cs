using System.ComponentModel;

namespace DemoCenter.ProductsData;

public enum DiscountType
{
    Percentage,
    FixedAmount
}

public class ComplexOrder
{
    [Category("General")]
    public string OrderID { get; set; }

    [Category("General")]
    public DateTime OrderDate { get; set; }

    [Category("Customer")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public Customer Customer { get; set; }

    [Category("Items")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public OrderItem Item_1 { get; set; }
}

public class Customer
{
    [Category("Identity")]
    public string Name { get; set; }

    [Category("Identity")]
    public string Email { get; set; }

    [Category("Location")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public ShippingAddress ShippingAddress { get; set; }

    public override string ToString()
    {
        return string.Empty;
    }
}

public class ShippingAddress
{
    [Category("Address")]
    public string Street { get; set; }

    [Category("Address")]
    public string City { get; set; }

    [Category("Geo")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public GeoLocation GeoLocation { get; set; }

    public override string ToString()
    {
        return string.Empty;
    }
}

public class GeoLocation
{
    [Category("Coordinates")]
    public double Latitude { get; set; }

    [Category("Coordinates")]
    public double Longitude { get; set; }

    public override string ToString()
    {
        return string.Empty;
    }
}

public class OrderItem
{
    [Category("Product")]
    public string SKU { get; set; }

    [Category("Product")]
    public int Quantity { get; set; }

    [Category("Financials")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public Pricing Pricing { get; set; } = new Pricing();

    public override string ToString()
    {
        return string.Empty;
    }
}

public class Pricing
{
    [Category("Financials")]
    public decimal UnitPrice { get; set; }

    [Category("Financials")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public Discount Discount { get; set; }

    public override string ToString()
    {
        return string.Empty;
    }
}

public class Discount
{
    [Category("Discount Details")]
    public DiscountType DiscountType { get; set; }

    [Category("Discount Details")]
    public decimal Amount { get; set; }

    public override string ToString()
    {
        return string.Empty;
    }
}