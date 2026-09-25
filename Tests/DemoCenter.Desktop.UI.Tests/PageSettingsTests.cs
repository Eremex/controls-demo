using System.Diagnostics;

using DemoCenter.ProductsData;

using Eremex.AvaloniaUI.Controls.Editors;

namespace DemoCenter.Desktop.UI.Tests;

/// <summary>
/// Changes every setting a demo page offers and checks that nothing falls over.
/// </summary>
/// <remarks>
/// The settings on the right are what a visitor actually touches, and they are the part
/// <see cref="ProductModulesTests"/> never reaches: it only proves the page builds in its default
/// state. A switch that throws on the second toggle, or a drop-down option whose template does
/// not build, stays invisible until something flips it.
/// </remarks>
public class PageSettingsTests : IClassFixture<DemoWindowFixture>
{
    private readonly DemoWindowFixture demo;

    public PageSettingsTests(DemoWindowFixture demo) => this.demo = demo;

    /// <summary>
    /// The products whose settings are walked.
    /// </summary>
    /// <remarks>
    /// Graphics3D is left out on purpose. Every one of its settings - multisampling above all -
    /// rebuilds the scene, and CI has no GPU: one product took longer than the whole rest of the
    /// suite. Its pages are still opened by <see cref="ProductModulesTests"/>, which is the check
    /// that matters for a control that draws itself.
    /// </remarks>
    public static IEnumerable<object[]> ProductNames() =>
        ProductModulesTests.ProductNames().Where(x => !SkippedProducts.Contains((string)x[0]));

    private static readonly HashSet<string> SkippedProducts = new() { "Graphics3D Control" };

    [Theory]
    [MemberData(nameof(ProductNames))]
    public void ProductSettingsCanBeChanged(string productName)
    {
        foreach (var module in ProductModulesTests.ModulesOf(productName))
        {
            // A group is a tree node without a page, so it has no settings of its own.
            if (module is GroupInfo)
                continue;

            demo.Open(module);

            var settings = demo.FindSettingsPanel();
            if (settings == null)
                continue;

            var started = Stopwatch.GetTimestamp();

            // Materialized before the walk: changing a setting rebuilds parts of the page, and
            // enumerating the visual tree lazily while it changes underneath is not safe.
            foreach (var editor in DemoWindowFixture.Descendants(settings).OfType<CheckEditor>().ToList())
                ToggleCheckEditor(productName, module, editor);

            foreach (var editor in DemoWindowFixture.Descendants(settings).OfType<ComboBoxEditor>().ToList())
                StepComboBoxEditor(productName, module, editor);

            // Printed per module: a whole product taking minutes says nothing about which page did
            // it, and some settings - switching multisampling on a 3D scene, for one - are simply
            // expensive rather than stuck.
            Console.WriteLine(
                $"settings walked: {module.Name} in {Stopwatch.GetElapsedTime(started).TotalMilliseconds:F0} ms");
        }
    }

    /// <summary>Flips a switch to its other state and back, checking both.</summary>
    private void ToggleCheckEditor(string productName, ProductInfoBase module, CheckEditor editor)
    {
        var original = editor.IsChecked;

        foreach (var value in new bool?[] { original != true, original })
        {
            using var errors = new AvaloniaErrorCollector();

            editor.IsChecked = value;
            demo.Settle();

            demo.Capture(productName, module.Name, $"{editor.Content}={value}");

            errors.AssertQuiet($"{module.Name}: check editor '{editor.Content}' set to {value}");
        }
    }

    /// <summary>
    /// Walks a drop-down through every option it offers and puts it back.
    /// </summary>
    /// <remarks>
    /// Every option rather than just one: these drop-downs pick display modes, layouts and edit
    /// modes, and it is the rarely chosen option that breaks.
    /// </remarks>
    private void StepComboBoxEditor(string productName, ProductInfoBase module, ComboBoxEditor editor)
    {
        var items = editor.ItemsSource?.Cast<object>().ToList();
        if (items == null || items.Count == 0)
            return;

        var original = editor.SelectedItem;

        foreach (var item in items)
        {
            using var errors = new AvaloniaErrorCollector();

            editor.SelectedItem = item;
            demo.Settle();

            demo.Capture(productName, module.Name, $"{editor.Name ?? "combo"}={item}");

            errors.AssertQuiet($"{module.Name}: combo box set to '{item}'");
        }

        editor.SelectedItem = original;
        demo.Settle();
    }
}
