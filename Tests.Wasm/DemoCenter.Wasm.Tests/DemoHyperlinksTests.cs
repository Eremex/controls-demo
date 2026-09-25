using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

using Eremex.AvaloniaUI.Controls.Editors;

namespace DemoCenter.Wasm.Tests;

/// <summary>
/// Checks the links in the Resources panel: the addresses are in place, there is something
/// to open them with, and navigation does not fail.
/// </summary>
/// <remarks>
/// <para>
/// The reason. In the browser the links silently did nothing:
/// <c>ExternalProcessHelper.Launch</c> went down the start-a-process path, which does not
/// exist under wasm, the exception was swallowed by an empty catch - and nothing happened.
/// Navigation now goes through <c>TopLevel.Launcher</c>.
/// </para>
/// <para>
/// What this test does not check, honestly: that the browser actually opened a tab. That is
/// invisible from inside wasm - <c>Launcher</c> calls <c>window.open</c>, and the call can
/// only be observed from outside the page. That check is done by a CDP run: a spy is
/// installed on <c>window.open</c>, and after a click on the link it receives
/// <c>https://eremexcontrols.net/</c>. Everything up to that call is caught here: a lost
/// address, an unavailable <c>Launcher</c>, an exception along the way.
/// </para>
/// </remarks>
public class DemoHyperlinksTests
{
	/// <summary>The links the demo shows in its Resources panel.</summary>
	public static IEnumerable<object[]> ResourceLinks() =>
	[
		["Documentation"],
		["Telegram (En)"],
		["Telegram (Ru)"],
		["Support Center"],
		["Contact sales"],
	];

	[Theory]
	[MemberData(nameof(ResourceLinks))]
	public async Task ResourceLinkNavigates(string text)
	{
		await DemoApp.Settle();

		var link = FindLinks().FirstOrDefault(x => x.Text == text);
		Assert.NotNull(link);

		// The address must be an absolute http(s) one: Launcher does not open relative URLs,
		// and nothing in the markup stops anyone from writing whatever they like.
		Assert.True(
			Uri.TryCreate(link.NavigationUrl, UriKind.Absolute, out var uri)
				&& (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps),
			$"The address of link \"{text}\" is not an absolute http(s) URL: {link.NavigationUrl ?? "<empty>"}");

		// Without a Launcher there is nothing to open the link with in the browser - exactly
		// the breakage that made the clicks do nothing.
		var topLevel = Assert.IsAssignableFrom<TopLevel>(TopLevel.GetTopLevel(link));
		Assert.NotNull(topLevel.Launcher);

		// The navigation itself, the same way the mouse does it. The controls do have
		// DoNavigateForTests, but it sits under #if DEBUG and the demo builds against the
		// Release package; so the very event the editor listens for is raised instead. The
		// listener is the inner TextBlock, and routed events bubble upwards - the event has
		// to be raised on it, not on the editor.
		var realEditor = DemoApp.Descendants(link)
			.OfType<TextBlock>()
			.FirstOrDefault(x => x.Name == "PART_RealEditor");
		Assert.NotNull(realEditor);

		Click(realEditor);
		await DemoApp.Settle();
	}

	/// <summary>The demo has exactly these resource links; one more makes this fail.</summary>
	[Fact]
	public async Task AllResourceLinksAreCovered()
	{
		await DemoApp.Settle();

		var actual = FindLinks().Select(x => x.Text).OrderBy(x => x, StringComparer.Ordinal).ToList();
		var covered = ResourceLinks().Select(x => (string)x[0]).OrderBy(x => x, StringComparer.Ordinal).ToList();

		Assert.Equal(covered, actual);
	}

	/// <summary>A left-button press, the same event a real mouse delivers.</summary>
	private static void Click(Control control)
	{
		var pointer = new Pointer(Pointer.GetNextFreeId(), PointerType.Mouse, isPrimary: true);
		var properties = new PointerPointProperties(RawInputModifiers.LeftMouseButton, PointerUpdateKind.LeftButtonPressed);

		control.RaiseEvent(new PointerPressedEventArgs(
			control,
			pointer,
			TopLevel.GetTopLevel(control)!,
			default,
			timestamp: 0,
			properties,
			KeyModifiers.None));
	}

	private static List<HyperlinkEditor> FindLinks() =>
		DemoApp.Descendants(DemoApp.MainView()).OfType<HyperlinkEditor>().ToList();
}
