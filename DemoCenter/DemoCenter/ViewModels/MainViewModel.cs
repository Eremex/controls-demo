using System.Collections.ObjectModel;

using Avalonia.Animation;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DemoCenter.ProductsData;
using Eremex.AvaloniaUI.Controls.Common;
using Eremex.AvaloniaUI.Themes;

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
    List<PaletteTypeInfo> palettes;

    [ObservableProperty]
    PaletteType selectedPalette;

    [ObservableProperty] 
    List<ProductInfoBase> products;
    public List<ProductInfoBase> flatProducts = new List<ProductInfoBase>();

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

    public MainViewModel()
    {
        Palettes = new List<PaletteTypeInfo>()
        {
            new PaletteTypeInfo(PaletteType.White, "Light"),
            new PaletteTypeInfo(PaletteType.Black, "Dark"),
        };
        SelectedPalette = Palettes[0].PaletteType;
        Products = ProductsData.Products.GetOrCreate();
        CreateFlatCollection();
        CurrentProductItem = flatProducts.FirstOrDefault(x => x is PageInfo && AllowDemoContent(x));
    }

    public void SelectProduct(string productName) => CurrentProductItem = flatProducts.FirstOrDefault(x => string.Equals(x.Name, productName));

    partial void OnCurrentProductItemChanged(ProductInfoBase value)
    {
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
        var prefix = "Demo Center v.1.1";
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
        Title = $"{prefix}{groupPrefix}{title}";
    }

    private GroupInfo GetGroupInfo(ProductInfoBase value)
    {
        if (value is PageInfo info)
            return Products.OfType<GroupInfo>().FirstOrDefault(x => x.Pages.Contains(info));
        return null;
    }

    private bool AllowDemoContent(ProductInfoBase product) => !App.IsWebApp || (product.ShowInWeb && GetGroupInfo(product) is { } group && group.ShowInWeb);

    private void CreateFlatCollection()
    {
        foreach (var product in Products)
        {
            flatProducts.Add(product);
            if (product is GroupInfo productGroup)
                foreach (var page in productGroup.Pages)
                    flatProducts.Add(page);
        }
    }
}

public class PaletteTypeInfo
{
    public PaletteType PaletteType { get; set; }

    public string PaletteTypeName { get; set; } 

    public PaletteTypeInfo(PaletteType paletteType, string paletteTypeName)
    {
        PaletteType = paletteType;
        PaletteTypeName = paletteTypeName;
    }
}
