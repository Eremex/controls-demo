using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public class DeveloperToolsGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>()
        {
            new PageInfo(name: "SVG Icons Browser", title: "Svg Icons Browser",
                viewModelGetter: () => new SvgIconsBrowserViewModel())
        };
    }
}