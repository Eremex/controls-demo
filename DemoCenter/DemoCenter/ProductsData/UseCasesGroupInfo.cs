using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class UseCasesGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Mortgage calculator", title: "Mortgage Calculator", descriptionGetter : () => Resources.UseCasesGroupInfo_Desc,
            viewModelGetter: () => new MortgageCalculatorViewModel(), updated: new VersionInfo(1, 3)),
            new (name: "Yacht parts", title: "Yacht Parts Catalogue", descriptionGetter : () => Resources.YachtParts_Desc,
            viewModelGetter: () => new YachtPartsViewModel(), introduced: new VersionInfo(1, 5)),
            new (name: "Car comparison", title: "Car Comparison", descriptionGetter : () => Resources.CarComparison_Desc,
            viewModelGetter: () => new CarComparisonViewModel(), introduced: new VersionInfo(1, 5))
        };
    }
}
