using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.VisualTree;
using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.Editors;
using Eremex.AvaloniaUI.Controls.ListView;

namespace DemoCenter.Views;
using Avalonia.Threading;
using DemoCenter.Helpers;
using System.Text;

public partial class SvgIconsBrowserView : UserControl
{
    public SvgIconsBrowserView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        if(ViewModel != null)
            UnsubscribeEvents(ViewModel);
        ViewModel = (SvgIconsBrowserViewModel)DataContext;
        if(ViewModel != null)
            SubscribeEvents(ViewModel);
    }
    private void UpdateDocument(AvaloniaEdit.TextEditor codeEditor, string templateName, string selectedIconString)
    {
        codeEditor.Clear();
        if (string.IsNullOrWhiteSpace(selectedIconString))
            return;
        var name = App.EmbeddedResources.FirstOrDefault(x =>
            x.EndsWith(templateName, StringComparison.InvariantCultureIgnoreCase));
        UpdateHighlighter(codeEditor, name.EndsWith(".axaml"));
        using (var stream = System.Reflection.Assembly.GetAssembly(typeof(App))?.GetManifestResourceStream(name))
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            var templateText = reader.ReadToEnd();
            codeEditor.Text = templateText.Replace("@@NAME@@", selectedIconString);
        }
    }

    private void UpdateHighlighter(AvaloniaEdit.TextEditor codeEditor, bool isXaml)
    {
        if (isXaml)
            codeEditor.SyntaxHighlighting =
                            (new ThemedSyntaxHighlighter("Axaml-Highlight")).HighlightingDefinition;
        else
            codeEditor.SyntaxHighlighting =
                            new ThemedSyntaxHighlighter("CSharp-Highlight").HighlightingDefinition;
    }

    private void SubscribeEvents(SvgIconsBrowserViewModel vm)
    {
        vm.RequestUpdateData += VmOnRequestUpdateData;
        vm.RequestScrollToCategory += VmOnRequestScrollToCategory;
        vm.PropertyChanged += ViewModelPropertyChanged;
        UpdateDocument();
    }

    private void ViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "FocusedItem")
        {
            UpdateDocument();
        }
    }

    private void UpdateDocument()
    {
        UpdateDocument(sharpCodeEditor, "svgCodeExample.cs", ViewModel.IconPath);
        UpdateDocument(xamlCodeEditor, "svgXamlExample.axaml", ViewModel.IconAxamlPath);
    }

    private void VmOnRequestScrollToCategory(SvgIconCategoryViewModel obj)
    {
        var groupIndex = IconsListControl.GetGroupIndex(nameof(SvgIconViewModel.CategoryName), obj.DisplayName);
        IconsListControl.ScrollTo(groupIndex);
        Dispatcher.UIThread.Post(() => IconsListControl.FocusedItemIndex = groupIndex);
    }

    private void VmOnRequestUpdateData()
    {
        IconsListControl.RefreshData();
    }

    private void UnsubscribeEvents(SvgIconsBrowserViewModel vm)
    {
        vm.RequestUpdateData -= VmOnRequestUpdateData;
        vm.RequestScrollToCategory -= VmOnRequestScrollToCategory;
        vm.PropertyChanged -= ViewModelPropertyChanged;
    }

    private SvgIconsBrowserViewModel ViewModel { get; set; }
    private void ListViewControl_OnFilterItem(object sender, ListViewFilterEventArgs e)
    {
        ((SvgIconsBrowserViewModel)DataContext)?.OnCustomFilter(e);
    }

    private async void OnPathEditorPointerPressed(object sender, PointerPressedEventArgs e)
    {
        await CopyToClipboard((Control)sender, ViewModel.IconPath);
    }
    
    private async void OnAxamlPathEditorPointerPressed(object sender, PointerPressedEventArgs e)
    {
        await CopyToClipboard((Control)sender, ViewModel.IconAxamlPath);
    }

    async Task CopyToClipboard(Control owner, string text)
    {
        IClipboard clipboard = ((Window)this.GetVisualRoot())?.Clipboard;
        await clipboard?.SetTextAsync(text);
        ToolTip.SetTip(owner, "Copied");
        ToolTip.SetIsOpen(owner, true);
    }
}
