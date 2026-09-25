using Avalonia.Logging;

using DemoCenter.ProductsData;

namespace DemoCenter.Wasm.Tests;

/// <summary>
/// Opens every demo module in turn in the browser and checks that none of them fails.
/// </summary>
/// <remarks>
/// <para>
/// The counterpart of <c>WindowsTests.ShowAllModules</c> from
/// Tests/DemoCenter.Desktop.UI.Tests, but written from scratch rather than linked. The
/// reason is not the xunit version: the desktop test opens a <c>MainWindow</c>, and there
/// are no windows at all in the browser - the application lives in
/// <c>ISingleViewApplicationLifetime.MainView</c>. There was nothing to port.
/// </para>
/// <para>
/// One test per module on purpose, rather than one for everything: if a module breaks, the
/// report names it, and the rest are still checked.
/// </para>
/// </remarks>
public class DemoModulesTests
{
	/// <summary>
	/// The names of every demo module. Taken from the same registry the application uses,
	/// so the list cannot drift away from the demo.
	/// </summary>
	public static IEnumerable<object[]> ModuleNames() =>
		DemoApp.EnumerateProducts().Select(x => new object[] { x.Name });

	[Theory]
	[MemberData(nameof(ModuleNames))]
	public async Task ModuleOpens(string moduleName)
	{
		var viewModel = DemoApp.MainViewModel();
		var product = DemoApp.SelectModule(moduleName);

		using var errors = new ErrorCollector();

		// The switch itself is the check: the view model is created, the ViewLocator finds
		// the view, bindings are applied, layout is computed. Anything that throws along the
		// way surfaces right here.
		await DemoApp.Settle();

		// A group is a tree node and has no page of its own: OnCurrentProductItemChanged
		// returns immediately for a GroupInfo. There is nothing to check beyond the absence
		// of a failure.
		if (product is not GroupInfo)
		{
			var pageViewModel = viewModel.CurrentProductItemViewModel;
			Assert.NotNull(pageViewModel);

			// The view model alone is not enough: it is created by a plain assignment and
			// proves nothing. The real check is that the ViewLocator found the view and that
			// the view was built into the tree. Without it the test would pass on a page
			// that shows nothing but blank space in the application.
			var expectedViewName = pageViewModel.GetType().Name.Replace("ViewModel", "View", StringComparison.Ordinal);
			Assert.Contains(expectedViewName, DemoApp.Descendants(DemoApp.MainView()).Select(x => x.GetType().Name));
		}

		Assert.True(errors.Messages.Count == 0, "Avalonia logged errors:" + Environment.NewLine + string.Join(Environment.NewLine, errors.Messages));
	}

	/// <summary>
	/// Checks that not everything is shown on the web.
	/// </summary>
	/// <remarks>
	/// Pages marked <c>showInWeb: false</c> must not open in the browser: exporting to PDF
	/// does not work there, and 3D is stripped from the build entirely. The test exists so
	/// that those markers cannot be lost unnoticed.
	/// </remarks>
	[Fact]
	public void HiddenInWebModulesStayHidden()
	{
		Assert.True(App.IsWebApp, "The application must be running on the browser backend.");

		var names = DemoApp.EnumerateProducts().Select(x => x.Name).ToList();

		Assert.DoesNotContain("Graphics3D Control", names);
		Assert.All(
			DemoApp.EnumerateProducts().Where(x => x.Name == "Export"),
			exportPage => Assert.False(exportPage.ShowInWeb, "The Export page must not be shown on the web."));
	}

	/// <summary>
	/// The language switcher is hidden on the web: satellite assemblies and the ICU shard
	/// are both chosen before the runtime starts, so the language cannot be changed on the
	/// fly.
	/// </summary>
	[Fact]
	public void LocaleSelectionIsHiddenInWeb() =>
		Assert.False(DemoApp.MainViewModel().IsLocaleSelectionAvailable);

	/// <summary>
	/// Collects what Avalonia writes to the log at Error level while a module opens.
	/// </summary>
	/// <remarks>
	/// Needed because a failed binding or a template that did not build throws nothing - the
	/// page simply ends up empty. Warning level is deliberately left out: routine messages
	/// land there too, and the test would be permanently red.
	/// </remarks>
	private sealed class ErrorCollector : ILogSink, IDisposable
	{
		private readonly ILogSink? previous = Logger.Sink;

		public ErrorCollector() => Logger.Sink = this;

		public List<string> Messages { get; } = [];

		public bool IsEnabled(LogEventLevel level, string area) => level >= LogEventLevel.Error;

		public void Log(LogEventLevel level, string area, object? source, string messageTemplate)
		{
			if (level >= LogEventLevel.Error)
				Messages.Add($"[{area}] {messageTemplate}");
		}

		public void Log(LogEventLevel level, string area, object? source, string messageTemplate, params object?[] propertyValues)
		{
			if (level >= LogEventLevel.Error)
				Messages.Add($"[{area}] {messageTemplate} {string.Join(", ", propertyValues)}");
		}

		public void Dispose() => Logger.Sink = previous;
	}
}
