using System.Collections.Generic;
using DemoCenter.ViewModels;
using DemoCenter.ViewModels.Ribbon;

namespace DemoCenter.ProductsData
{
    public static class RibbonGroupInfo
    {
        internal static List<PageInfo> Create()
        {
            return new List<PageInfo>()
            {
                new PageInfo(name: "WordPadExample", title: "WordPad Example",
                    description: "WordPad Example",
                    viewModelGetter: () => new WordPadExampleViewModel(),
                    new VersionInfo(1, 1), null, false)
            };
        }
    }
}