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
                    viewModelGetter: () => new WordPadExampleViewModel(),
                    descriptionGetter: () => string.Format( Resources.RibbonControlAllowsYouToIntegrateMicrosoft, Environment.NewLine + Environment.NewLine, Environment.NewLine+ Environment.NewLine), introduced: new VersionInfo(1, 1), updated: null, showInWeb: false)
            };
        }
    }
}