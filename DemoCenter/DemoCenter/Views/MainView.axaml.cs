using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Xml;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Avalonia.Threading;
using AvaloniaEdit.Highlighting;
using DemoCenter.Helpers;
using DemoCenter.ProductsData;
using DemoCenter.ViewModels;

using Eremex.AvaloniaUI.Controls.TreeList;
using Eremex.AvaloniaUI.Themes;

namespace DemoCenter.Views;

public partial class MainView : UserControl
{
    string loadedResource;

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

        else if(e.PropertyName == nameof(MainViewModel.SourceFile))
            UpdateDocument();
        else if (e.PropertyName == nameof(MainViewModel.SelectedCode))
        {
            CodeViewEditor.SearchPanel?.Open();
            CodeViewEditor.SearchPanel.SearchPattern = ViewModel.SelectedCode ?? string.Empty;
        }
    }

    private void UpdateDocument()
    {
        if(ViewModel == null)
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

    IDisposable titleSubscriber;

    private void OnPageSelectorKeyDown(object sender, KeyEventArgs e)
    {
        if(e.Key == Key.Up)
        {
            var currentIndex = ViewModel.FlatProducts.IndexOf(ViewModel.CurrentProductItem);
            if(ViewModel.FlatProducts[--currentIndex] is GroupInfo)
            {
                if(--currentIndex > 0)
                    ViewModel.CurrentProductItem = ViewModel.FlatProducts[currentIndex];
                e.Handled = true;
            }
        }
    }

    private void OnPageSelectorFocusedNodeChanged(object sender, TreeListFocusedNodeChangedEventArgs e)
    {
        if(e.Node.Content is GroupInfo)
            pageSelector.MoveNextNode();
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
        return ((GroupInfo)item).Pages;
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
        else if(variant == ThemeVariant.Dark)
            return values[2];
        return null;
    }
}

public class BadgeVisibilityConverter : MarkupExtension, IValueConverter
{
    public ProductBadgeType BadgeType { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (value is ProductBadgeType badgeType) && BadgeType == badgeType;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
