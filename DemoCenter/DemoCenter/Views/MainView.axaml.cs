using System.Collections;
using System.ComponentModel;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using DemoCenter.Helpers;
using DemoCenter.ProductsData;
using DemoCenter.ViewModels;

using Eremex.AvaloniaUI.Controls.TreeList;

namespace DemoCenter.Views;

public partial class MainView : UserControl
{
    string loadedResource;
    IDisposable titleSubscriber;

    public MainView()
    {
        InitializeComponent();
        pageSelector.AddHandler(TextBox.KeyDownEvent, OnPageSelectorKeyDown, RoutingStrategies.Tunnel);
    }

    MainViewModel ViewModel { get; set; }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        UnsubscribeEvents(ViewModel);
        ViewModel = DataContext as MainViewModel;
        SubscribeEvents(ViewModel);
    }

    private void SubscribeEvents(MainViewModel viewModel)
    {
        if(viewModel == null)
            return;
        titleSubscriber = this.Bind(TitleProperty, new Binding() { Source = viewModel, Path = "Title" });
        UpdateDocument();
        viewModel.PropertyChanged += OnMainViewModelPropertyChanged;
    }

    private void UnsubscribeEvents(MainViewModel viewModel)
    {
        if(viewModel == null)
            return;
        titleSubscriber?.Dispose();
        titleSubscriber = null;
        viewModel.PropertyChanged -= OnMainViewModelPropertyChanged;
    }

    private void OnMainViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MainViewModel.SelectedThemeVariant) && ViewModel?.SelectedThemeVariant != null && Application.Current is App application)
            application.RequestedThemeVariant = ViewModel.SelectedThemeVariant;
        else if (e.PropertyName == nameof(MainViewModel.SourceFile))
            UpdateDocument();
        else if (e.PropertyName == nameof(MainViewModel.SelectedCode))
        {
            CodeViewEditor.SearchPanel?.Open();
            CodeViewEditor.SearchPanel.SearchPattern = ViewModel.SelectedCode ?? string.Empty;
        }
    }

    private void UpdateDocument()
    {
        if(ViewModel == null || string.IsNullOrEmpty(ViewModel.SourceFile))
            return;
        var name = App.EmbeddedResources.FirstOrDefault(x =>
            x.EndsWith(ViewModel.SourceFile, StringComparison.InvariantCultureIgnoreCase));
        if(string.IsNullOrEmpty(name))
        {
            CodeViewEditor.Clear();
            return;
        }

        if(loadedResource != name)
            using(var stream = System.Reflection.Assembly.GetAssembly(typeof(App))?.GetManifestResourceStream(name))
            {
                if(name.EndsWith(".cs"))
                    CodeViewEditor.SyntaxHighlighting =
                        new ThemedSyntaxHighlighter("CSharp-Highlight").HighlightingDefinition;
                else if(name.EndsWith(".axaml"))
                    CodeViewEditor.SyntaxHighlighting =
                        (new ThemedSyntaxHighlighter("Axaml-Highlight")).HighlightingDefinition;
                else
                    CodeViewEditor.SyntaxHighlighting = null;
                CodeViewEditor.Load(stream);
                CodeViewEditor.ScrollToHome();
                loadedResource = name;
            }
    }

    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<MainView, string>(nameof(Title));

    public string Title
    {
        get { return GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    private void OnPageSelectorKeyDown(object sender, KeyEventArgs e)
    {
        if(e.Key == Key.Up)
        {
            if (pageSelector.FocusedNode == null)
                return;

            if(pageSelector.FocusedNode.ParentNode.Nodes.Where(x => x.IsVisible).First() == pageSelector.FocusedNode)
            {
                var parentNode = pageSelector.FocusedNode.ParentNode;
                var visibleParentNodes = pageSelector.Nodes.Where((x) => x.IsVisible).ToList();

                int parentNodeIndex = visibleParentNodes.IndexOf(parentNode);
                if (parentNodeIndex > 0)
                {
                    parentNodeIndex--;

                    visibleParentNodes[parentNodeIndex].IsExpanded = true;
                    pageSelector.FocusedNode = visibleParentNodes[parentNodeIndex].Nodes.Where(x => x.IsVisible).Last();

                    parentNode.IsExpanded = false;
                }
                e.Handled = true;
            }
        }
    }

    private void OnPageSelectorFocusedNodeChanged(object sender, TreeListFocusedNodeChangedEventArgs e)
    {
        if (e.Node != null && e.Node.ParentNode == null)
        {
            if (!e.Node.IsExpanded)
            {
                pageSelector.CollapseAllNodes();
                e.Node.IsExpanded = true;
            }

            pageSelector.FocusedNode = e.Node.Nodes.Where(x => x.IsVisible).First();
        }
    }

    private void CodeViewEditor_OnActualThemeVariantChanged(object sender, EventArgs e)
    {
        loadedResource = null;
        UpdateDocument();
    }
}

internal class PagesChildrenSelector : ITreeListChildrenSelector
{
    bool ITreeListChildrenSelector.HasChildren(object item) => (item as GroupInfo)?.Pages?.Any() == true;

    IEnumerable ITreeListChildrenSelector.SelectChildren(object item)
    {
        if(item is GroupInfo groupInfo)
            return groupInfo.Pages;
        return null;
    }
}

public class ThemeVariantToIconDataConverter : MarkupExtension, IMultiValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] == null || values[0] == AvaloniaProperty.UnsetValue || values.Count != 3)
            return null;
        var variant = (ThemeVariant)values[0]!;
        if(variant == ThemeVariant.Light)
            return values[1];
        if(variant == ThemeVariant.Dark)
            return values[2];
        return null;
    }
}
