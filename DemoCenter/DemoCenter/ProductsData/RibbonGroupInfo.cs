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
                    description: $"RibbonControl allows you to integrate Microsoft Office-inspired navigation menus into your Avalonia UI applications. The control comes with two views: Classic (three item rows) and Simplified (one item row). The dropdown button at the control's bottom right corner opens a selector between these views. {Environment.NewLine + Environment.NewLine}RibbonControl supports multiple item types: large and small buttons, check buttons, sub-menus, in-place editors, inline and dropdown galleries, groups of buttons, and more. You can create as many pages with items as you need, and place items in the Quick Access Toolbar or the tab header area. {Environment.NewLine+ Environment.NewLine}Press the ALT key to focus the Ribbon, and then use the keyboard to navigate between Ribbon elements and activate commands.",
                    viewModelGetter: () => new WordPadExampleViewModel(),
                    new VersionInfo(1, 1), null, false)
            };
        }
    }
}