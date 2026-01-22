using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public static class UseCasesGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Mortgage calculator", title: "Mortgage Calculator", descriptionGetter : () => Resources.UseCasesGroupInfo_Desc,
            viewModelGetter: () => new MortgageCalculatorViewModel(), updated: new VersionInfo(1, 3))
        };
    }
}
