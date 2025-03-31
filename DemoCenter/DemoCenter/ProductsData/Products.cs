using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class Products
{
    static List<ProductInfoBase> products;

    static List<ProductInfoBase> CreateProducts() => new()
    {
        new GroupInfo("Data Grid", "Data Grid", () => new DataGridPageViewModel(), () => "Data Grid description", DataGridGroupInfo.Create(), showInWeb: true),
        new GroupInfo("Tree List", "Tree List", () => new TreeListGroupViewModel(), () => "Tree List description", TreeListGroupInfo.Create()),
        new GroupInfo("Ribbon", "Ribbon", () => new RibbonGroupViewModel(), () => "Ribbon description", RibbonGroupInfo.Create()),
        new GroupInfo("Graphics3D Control", "Graphics3D Control", () => new Graphics3DControlViewModel(), () => "Graphics3D Control description", Graphics3DControlGroupInfo.Create(), new VersionInfo(1, 1), showInWeb: false),
        new GroupInfo("Cartesian Chart", "Cartesian Chart", () => new ChartsPageViewModel(), () => "Cartesian Chart description", CartesianChartGroupInfo.Create()),
        new GroupInfo("Polar Chart", "Polar Chart", () => new ChartsPageViewModel(), () => "Polar Chart description", PolarChartGroupInfo.Create()),
        new GroupInfo("Smith Chart", "Smith Chart", () => new ChartsPageViewModel(), () => "Smith Chart description", SmithChartGroupInfo.Create()),
        new GroupInfo("Heatmap", "Heatmap", () => new ChartsPageViewModel(), () => "Heatmap description", HeatmapGroupInfo.Create(), new VersionInfo(1, 1)),
        new GroupInfo("Bars and Docking", "Bars and Docking", () => new BarsGroupViewModel(), () => "Bars and Docking description", BarsGroupInfo.Create()),
        new GroupInfo("Editors", "Editors", () => new EditorsGroupViewModel(), () => "Editors description", EditorsGroupInfo.Create()),
        new GroupInfo("Property Grid", "Property Grid", () => new PropertyGridGroupViewModel(), () => "Property Grid description", PropertyGridGroupInfo.Create()),
        new GroupInfo("Common Controls", "Common Controls", () => new CommonControlsGroupViewModel(), () => "Common Controls description", CommonControlsGroupInfo.Create()),
        new GroupInfo("Standard Controls", "Standard Controls", () => new StandardControlsGroupViewModel(), () => "Standard Controls description", StandardControlsGroupInfo.Create()),
        new GroupInfo("Tools", "Tools", () => new DeveloperToolsGroupViewModel(), () => "Developer Tools", DeveloperToolsGroupInfo.Create()),
    };

    public static List<ProductInfoBase> GetOrCreate() => products ??= CreateProducts();
    public static void Reset() { products = null; }
}
