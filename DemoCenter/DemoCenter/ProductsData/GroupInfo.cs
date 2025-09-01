using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public class GroupInfo : ProductInfoBase
{
    public List<PageInfo> Pages { get; }
    public override bool HasChildren => Pages.Count > 0;
    public override VersionInfo Introduced { get; }
    public override VersionInfo? Updated { get; }

    public GroupInfo(string name, string title, Func<PageViewModelBase> viewModelGetter, Func<string> descriptionGetter, List<PageInfo> pages, bool showInWeb = true)  : base(name, title, viewModelGetter, descriptionGetter, showInWeb)
    {
        Pages = pages;
        Introduced = pages.Min(info => info.Introduced);
        var maxUpdated = pages.Max(info => info.Updated); 
        var maxIntroduced = pages.Max(info => info.Introduced); 
        Updated = VersionInfo.Max(maxIntroduced, maxUpdated);
    }
}
