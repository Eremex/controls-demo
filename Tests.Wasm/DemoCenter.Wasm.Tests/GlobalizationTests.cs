using System.Globalization;

using Avalonia.Controls;

namespace DemoCenter.Wasm.Tests;

/// <summary>
/// Checks that the browser build formats numbers and dates the way the demo intends.
/// </summary>
/// <remarks>
/// <para>
/// The demo pins itself to en-US in the <c>App</c> static constructor. In the browser that
/// assignment used not to survive: the runtime starts with the navigator locale, and the
/// continuations the application actually runs on kept it, so the grids showed dd.MM.yyyy
/// dates, a comma decimal separator and - for a locale without a region, which is what a
/// browser reporting plain "ru" produces - the generic currency sign instead of a symbol.
/// </para>
/// <para>
/// The checks run after <see cref="DemoApp.Settle"/> on purpose: on the startup thread the
/// culture was always correct, and only a continuation reproduces the failure.
/// </para>
/// </remarks>
public class GlobalizationTests
{
	[Fact]
	public async Task CultureIsEnglishAfterStartup()
	{
		await DemoApp.Settle();

		Assert.Equal("en-US", CultureInfo.CurrentCulture.Name);
		Assert.Equal("en-US", CultureInfo.CurrentUICulture.Name);
	}

	[Fact]
	public async Task CurrencyFormattingHasASymbol()
	{
		await DemoApp.Settle();

		var formatted = 2551m.ToString("C0", CultureInfo.CurrentCulture);

		Assert.DoesNotContain('¤', formatted);
		Assert.Equal("$2,551", formatted);
	}

	[Fact]
	public async Task DatesUseTheEnglishPattern()
	{
		await DemoApp.Settle();

		Assert.Equal("05/18/2025", new DateTime(2025, 5, 18).ToString("d", CultureInfo.CurrentCulture));
	}

	/// <summary>
	/// The end of the chain: a page whose cells are formatted with "C0" must not show the
	/// generic currency sign anywhere in its visual tree.
	/// </summary>
	[Fact]
	public async Task CurrencyCellsShowNoGenericSign()
	{
		DemoApp.SelectModule("Total Summaries");
		await DemoApp.Settle();

		var texts = DemoApp.Descendants(DemoApp.MainView())
			.OfType<TextBlock>()
			.Select(x => x.Text)
			.Where(x => !string.IsNullOrEmpty(x) && x.Contains('¤'))
			.ToList();

		Assert.True(texts.Count == 0, "Cells show the generic currency sign: " + string.Join(", ", texts));
	}
}
