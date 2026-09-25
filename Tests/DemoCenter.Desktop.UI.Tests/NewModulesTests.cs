using DemoCenter;
using DemoCenter.ProductsData;

namespace DemoCenter.Desktop.UI.Tests;

/// <summary>
/// Checks that the pages added in this release carry the New badge in the products tree.
/// </summary>
/// <remarks>
/// The badge is driven by ProductInfoBase.IsNew, which compares the version a page was introduced
/// in against the version of the controls the demo is built against - so a page registered without
/// an explicit version silently ships unmarked. This test goes red once the product version moves
/// on, and that is the reminder to retire the badge rather than a defect.
/// </remarks>
public class NewModulesTests
{
    /// <summary>The Property Grid pages added in this release.</summary>
    public static IEnumerable<object[]> NewPropertyGridPages() =>
        new[]
        {
            new object[] { "Multiple Objects Editing" },
            new object[] { "Validation" },
            new object[] { "PropertyGrid Layout" },
        };

    public static IEnumerable<object[]> NewRibbonPages() =>
        new[] { new object[] { "Ribbon Layout" } };

    [Theory]
    [MemberData(nameof(NewPropertyGridPages))]
    public void NewPropertyGridPagesAreMarkedNew(string pageName) => AssertNew("Property Grid", pageName);

    [Theory]
    [MemberData(nameof(NewRibbonPages))]
    public void NewRibbonPagesAreMarkedNew(string pageName) => AssertNew("Ribbon", pageName);

    static void AssertNew(string productName, string pageName)
    {
        var page = ProductModulesTests.ModulesOf(productName).FirstOrDefault(x => x.Name == pageName);
        Assert.True(page != null, $"There is no {productName} page named {pageName}.");

        Assert.True(
            page.IsNew,
            $"{pageName}: introduced in {page.Introduced.Major}.{page.Introduced.Minor}, "
                + $"the demo is built against {App.Version.Major}.{App.Version.Minor}, so the New badge is not shown.");
    }
}
