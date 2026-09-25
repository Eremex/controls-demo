using System.Collections;
using System.Globalization;

using DemoCenter;

namespace DemoCenter.Desktop.UI.Tests;

/// <summary>
/// Checks that every string the demo shows is translated into all the languages it ships.
/// </summary>
/// <remarks>
/// A missing translation is invisible at run time: the resource manager silently falls back to the
/// neutral resources, so the page opens with an English description in a Russian UI and nothing
/// reports it. Comparing against the neutral value is what makes the fallback detectable.
/// </remarks>
public class LocalizationTests
{
    /// <summary>The cultures the demo ships satellite resources for.</summary>
    public static IEnumerable<object[]> Cultures() =>
        new[] { new object[] { "ru" }, new object[] { "zh-Hans" } };

    [Theory]
    [MemberData(nameof(Cultures))]
    public void EveryStringIsTranslated(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var neutral = Resources.ResourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true);
        Assert.True(neutral != null, "The neutral resources of the demo could not be loaded.");

        var untranslated = new List<string>();

        foreach (DictionaryEntry entry in neutral)
        {
            var key = (string)entry.Key;

            // Only strings are translated - images and other resources are shared by all cultures.
            if (entry.Value is not string neutralValue)
                continue;

            var localized = Resources.ResourceManager.GetString(key, culture);
            if (localized == null || localized == neutralValue)
                untranslated.Add(key);
        }

        Assert.True(
            untranslated.Count == 0,
            $"{cultureName}: {untranslated.Count} string(s) fall back to the neutral resources:"
                + Environment.NewLine + string.Join(Environment.NewLine, untranslated.Order()));
    }
}
