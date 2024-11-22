using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public class GroupInfo : ProductInfoBase
{
    public List<PageInfo> Pages { get; }
    public override bool HasChildren => Pages.Count > 0;

    public GroupInfo(string name, string title, string description, Func<PageViewModelBase> viewModelGetter, List<PageInfo> pages, VersionInfo? introduced = null, VersionInfo? updated = null, bool showInWeb = true)  : base(name, title, description, viewModelGetter, introduced, updated, showInWeb)
    {
        Pages = pages;
    }
}
