using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class Products
{
    static List<ProductInfoBase> products;

    static List<ProductInfoBase> CreateProducts() => new()
    {
        new GroupInfo("Data Grid", "Data Grid", "Data Grid description", () => new DataGridPageViewModel(), DataGridGroupInfo.Create(), showInWeb: true),
        new GroupInfo("Tree List", "Tree List", "Tree List description", () => new TreeListGroupViewModel(), TreeListGroupInfo.Create()),
        new GroupInfo("Ribbon", "Ribbon", "Ribbon description", () => new RibbonGroupViewModel(), RibbonGroupInfo.Create()),
        new GroupInfo("Graphics3D Control", "Graphics3D Control", "Graphics3D Control description", () => new Graphics3DControlViewModel(), Graphics3DControlGroupInfo.Create(), new VersionInfo(1, 1), showInWeb: false),
        new GroupInfo("Cartesian Chart", "Cartesian Chart", "Cartesian Chart description", () => new ChartsPageViewModel(), CartesianChartGroupInfo.Create()),
        new GroupInfo("Polar Chart", "Polar Chart", "Polar Chart description", () => new ChartsPageViewModel(), PolarChartGroupInfo.Create()),
        new GroupInfo("Smith Chart", "Smith Chart", "Smith Chart description", () => new ChartsPageViewModel(), SmithChartGroupInfo.Create()),
        new GroupInfo("Heatmap", "Heatmap", "Heatmap description", () => new ChartsPageViewModel(), HeatmapGroupInfo.Create(), new VersionInfo(1, 1)),
        new GroupInfo("Bars and Docking", "Bars and Docking", "Bars and Docking description", () => new BarsGroupViewModel(), BarsGroupInfo.Create()),
        new GroupInfo("Editors", "Editors", "Editors description", () => new EditorsGroupViewModel(), EditorsGroupInfo.Create()),
        new GroupInfo("Property Grid", "Property Grid", "Property Grid description", () => new PropertyGridGroupViewModel(), PropertyGridGroupInfo.Create()),
        new GroupInfo("Common Controls", "Common Controls", "Common Controls description", () => new CommonControlsGroupViewModel(), CommonControlsGroupInfo.Create()),
        new GroupInfo("Standard Controls", "Standard Controls", "Standard Controls description", () => new StandardControlsGroupViewModel(), StandardControlsGroupInfo.Create()),
        new GroupInfo("Tools", "Tools", "Developer Tools", () => new DeveloperToolsGroupViewModel(), DeveloperToolsGroupInfo.Create()),
    };

    public static List<ProductInfoBase> GetOrCreate() => products ??= CreateProducts();
    public static void Reset() { products = null; }
}
