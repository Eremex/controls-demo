using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using Avalonia.VisualTree;

using DemoCenter.ProductsData;
using DemoCenter.ViewModels;

namespace DemoCenter.Wasm.Tests;

/// <summary>
/// Access to the running demo and the actions the tests share.
/// </summary>
/// <remarks>
/// There is a single application for the whole run: the browser backend is started by
/// DemoCenter.Wasm.TestHost before discovery begins, and a second Avalonia instance in one
/// process is impossible. Hence static access to what is already running rather than a
/// fixture.
/// </remarks>
internal static class DemoApp
{
	/// <summary>Every demo module: groups and their pages, in display order.</summary>
	public static IEnumerable<ProductInfoBase> EnumerateProducts()
	{
		foreach (var product in Products.GetOrCreate())
		{
			yield return product;

			if (product is GroupInfo group)
			{
				foreach (var page in group.Pages)
					yield return page;
			}
		}
	}

	public static Control MainView()
	{
		var lifetime = Assert.IsAssignableFrom<ISingleViewApplicationLifetime>(
			Application.Current?.ApplicationLifetime);

		return Assert.IsAssignableFrom<Control>(lifetime.MainView);
	}

	public static MainViewModel MainViewModel() =>
		Assert.IsType<MainViewModel>(MainView().DataContext);

	/// <summary>Switches the demo to the module with this name and returns it.</summary>
	public static ProductInfoBase SelectModule(string moduleName)
	{
		var viewModel = MainViewModel();
		var product = viewModel.FlatProducts.FirstOrDefault(x => x.Name == moduleName);
		Assert.NotNull(product);

		viewModel.SelectProduct(product);
		return product;
	}

	/// <summary>
	/// Drives the switch through to a completed layout.
	/// </summary>
	/// <remarks>
	/// Layout is computed explicitly instead of waiting for a frame: Avalonia's rendering in
	/// the browser is driven by requestAnimationFrame, which never fires in a background
	/// tab - a test that waits for painting would hang in a headless run.
	/// <c>UpdateLayout</c> builds the visual tree deterministically and does not depend on
	/// frames.
	/// </remarks>
	public static async Task Settle()
	{
		// Bindings and view creation go through the dispatcher queue.
		Dispatcher.UIThread.RunJobs();

		var topLevel = TopLevel.GetTopLevel(
			(Application.Current?.ApplicationLifetime as ISingleViewApplicationLifetime)?.MainView);
		topLevel?.UpdateLayout();

		// Hand control back to the JS loop: in single-threaded wasm neither timers nor the
		// asynchronous loading of page data make progress without it.
		await Task.Yield();
		Dispatcher.UIThread.RunJobs();
		topLevel?.UpdateLayout();
	}

	/// <summary>
	/// Waits for real rendering passes.
	/// </summary>
	/// <remarks>
	/// Needed wherever the thing under test is produced by rendering rather than by layout:
	/// the <c>OverlayLayer</c>, for instance, in which Avalonia shows popups when there are
	/// no operating-system windows. <see cref="Settle"/> is not suitable there - it
	/// deliberately does not wait for frames.
	///
	/// A pause rather than <c>Task.Yield</c>: a frame in the browser is driven by
	/// requestAnimationFrame, so control has to be handed back to the JS loop for real, with
	/// time actually elapsing.
	/// </remarks>
	public static async Task SettleFrames(int frames = 3)
	{
		for (var i = 0; i < frames; i++)
		{
			Dispatcher.UIThread.RunJobs();
			await Task.Delay(32);
		}

		await Settle();
	}

	/// <summary>The whole visual tree below a node, depth first.</summary>
	public static IEnumerable<Visual> Descendants(Visual root)
	{
		foreach (var child in root.GetVisualChildren())
		{
			yield return child;

			foreach (var nested in Descendants(child))
				yield return nested;
		}
	}
}
