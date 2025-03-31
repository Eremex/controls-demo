using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.ProductsData;
using Eremex.AvaloniaUI.Controls.Common;

namespace DemoCenter.ViewModels;

public partial class MainViewModel : ViewModelBase
{   
    [ObservableProperty] 
    ProductInfoBase currentProductItem;
    [ObservableProperty]
    PageViewModelBase currentProductItemViewModel;

    [ObservableProperty] 
    string title;

    [ObservableProperty]
    List<LocaleInfo> locales;

    [ObservableProperty]
    CultureInfo selectedLocale;

    [ObservableProperty]
    List<ThemeVariantInfo> themeVariants;

    [ObservableProperty]
    ThemeVariant selectedThemeVariant;

    [ObservableProperty] 
    List<ProductInfoBase> products;
    public List<ProductInfoBase> FlatProducts = new List<ProductInfoBase>();

    [ObservableProperty]
    ObservableCollection<string> sourceFiles;
    [ObservableProperty]
    string sourceFile;

    [ObservableProperty]
    string selectedCode;

    [ObservableProperty]
    bool allowCode = true;

    [ObservableProperty]
    bool isDemoSelected = true;
    
    private readonly string titlePrefix;

    public MainViewModel() { }

    public MainViewModel(ThemeVariant startupThemeVariant = null)
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "1.0";
        titlePrefix = $"Demo Center v.{version}";
        
        ThemeVariants = new List<ThemeVariantInfo>()
        {
            new ThemeVariantInfo("Light", ThemeVariant.Light),
            new ThemeVariantInfo("Dark", ThemeVariant.Dark),
        };

        Locales = new List<LocaleInfo>()
        {
           new LocaleInfo("En", new CultureInfo("En-us")),
           new LocaleInfo("Ru", new CultureInfo("Ru-ru")),
           new LocaleInfo("CN", new CultureInfo("zh-Hans")),
        };
        SelectedThemeVariant = startupThemeVariant == ThemeVariant.Dark ? ThemeVariant.Dark : ThemeVariant.Light;
        SelectedLocale = Locales.First().Locale;//EN
        
        Products = ProductsData.Products.GetOrCreate();
        PopulateFlatCollection();
        CurrentProductItem = FlatProducts.FirstOrDefault(x => x is PageInfo && AllowDemoContent(x));
    }

    public void SelectProduct(string productName) => CurrentProductItem = FlatProducts.FirstOrDefault(x => string.Equals(x.Name, productName));

    partial void OnCurrentProductItemChanged(ProductInfoBase value)
    {
        if (value is null)
        {
            CurrentProductItemViewModel = null;
            return;
        }
        
        if (value is GroupInfo)
            return;
        
        CreateTitle(value);

        var allowContent = AllowDemoContent(value);
        CurrentProductItemViewModel = allowContent ? CurrentProductItem?.ViewModelGetter?.Invoke() : new DesktopOnlyViewModel();
        AllowCode = allowContent;
        if (!AllowCode)
            IsDemoSelected = true;
        var viewModelName = CurrentProductItemViewModel?.GetType().Name;
        var viewName = viewModelName.Replace("ViewModel", "View");

        SourceFiles = new ObservableCollection<string> { $"{viewName}.axaml", $"{viewName}.axaml.cs", $"{viewModelName}.cs", };
        SourceFile = SourceFiles.First();
    }

    private void CreateTitle(ProductInfoBase product)
    {
        var groupPrefix = string.Empty;
        var title = string.Empty;

        if (product is PageInfo page)
        {
            var group = GetGroupInfo(page);
            if (group != null)
                groupPrefix = $" - {group.Title}";
            title = $" - {page.Title}";
        }
        else
            title = $" - {product?.Title}";
        Title = $"{titlePrefix}{groupPrefix}{title}";
    }

    private GroupInfo GetGroupInfo(ProductInfoBase value)
    {
        if (value is PageInfo info)
            return Products.OfType<GroupInfo>().FirstOrDefault(x => x.Pages.Contains(info));
        return null;
    }

    private bool AllowDemoContent(ProductInfoBase product) => !App.IsWebApp || (product.ShowInWeb && GetGroupInfo(product) is { } group && group.ShowInWeb);

    private void PopulateFlatCollection()
    {
        FlatProducts.Clear();
        foreach (var product in Products)
        {
            FlatProducts.Add(product);
            if (product is GroupInfo productGroup)
                foreach (var page in productGroup.Pages)
                    FlatProducts.Add(page);
        }
    }

    partial void OnSelectedLocaleChanged(CultureInfo value)
    {
        CultureInfo.CurrentUICulture = SelectedLocale;

        var current = CurrentProductItem;
        CurrentProductItem = null;
        CurrentProductItem = current;
    }
}

public class ThemeVariantInfo
{
    public string ThemeVariantName { get; init; } 

    public ThemeVariant ThemeVariant { get; init; }

    public ThemeVariantInfo(string name, ThemeVariant themeVariant)
    {
        ThemeVariantName = name;
        ThemeVariant = themeVariant;
    }
}

public class LocaleInfo
{
    public string LocaleName { get; init; }

    public CultureInfo Locale { get; init; }

    public LocaleInfo(string name, CultureInfo culture)
    {
        LocaleName = name;
        Locale = culture;
    }
}

