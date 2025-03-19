using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData
{
    public static class RibbonGroupInfo
    {
        internal static List<PageInfo> Create()
        {
            return new List<PageInfo>()
            {
                new PageInfo(name: "WordPad Example", title: "WordPad Example",
                    description: string.Format(Resources.RibbonControlAllowsYouToIntegrateMicrosoft, Environment.NewLine + Environment.NewLine, Environment.NewLine+ Environment.NewLine),
                    viewModelGetter: () => new WordPadExampleViewModel(),
                    new VersionInfo(1, 1), null, false)
            };
        }
    }
}