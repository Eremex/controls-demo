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
    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(PreviousViewCommand)), NotifyCanExecuteChangedFor(nameof(NextViewCommand))] 
    ProductInfoBase currentProductItem;
    [ObservableProperty]
    PageViewModelBase currentProductItemViewModel;

    [ObservableProperty] 
    string title;

    [ObservableProperty]
    bool isNextButtonEnabled; // TODO Remove after fix bars
    [ObservableProperty]
    bool isPreviousButtonEnabled;  // TODO Remove after fix bars

    [ObservableProperty]
    List<PaletteTypeInfo> palettes;

    [ObservableProperty]
    PaletteType selectedPalette;
    [ObservableProperty]
    IPageTransition pageTransition;

    [ObservableProperty] 
    List<ProductInfoBase> products;
    public List<ProductInfoBase> flatProducts = new List<ProductInfoBase>();

    [ObservableProperty]
    ObservableCollection<string> sourceFiles;
    [ObservableProperty]
    string sourceFile;

    [ObservableProperty]
    bool showCode;

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
        SelectProduct("Grouping");
        PageTransition = new PageSlide(TimeSpan.FromMilliseconds(200), PageSlide.SlideAxis.Horizontal);
    }

    public void SelectProduct(string productName) => CurrentProductItem = flatProducts.FirstOrDefault(x => string.Equals(x.Name, productName));

    [RelayCommand(CanExecute = nameof(CanExecutePreviousViewCommand))]
    private void PreviousView(object parameter) => Navigate(-1);

    [RelayCommand(CanExecute = nameof(CanExecuteNextViewCommand))]
    private void NextView(object parameter) => Navigate(1);

    private bool CanExecutePreviousViewCommand(object parameter) => AllowPreviousNavigation();

    private bool CanExecuteNextViewCommand(object parameter) => AllowNextNavigation();

    private void Navigate(int delta)
    {
        if(CurrentProductItem != null)
        {
            var newIndex = flatProducts.IndexOf(CurrentProductItem) + delta;
            if (flatProducts[newIndex] is GroupInfo)
                newIndex += delta;
            CurrentProductItem = flatProducts[newIndex];
        }
    }

    partial void OnCurrentProductItemChanged(ProductInfoBase value)
    {
        if (value is GroupInfo)
            return;
        CreateTitle(value);
        IsNextButtonEnabled = AllowNextNavigation();
        IsPreviousButtonEnabled = AllowPreviousNavigation();
        CurrentProductItemViewModel = CurrentProductItem?.ViewModelGetter?.Invoke();

        var viewModelName = CurrentProductItemViewModel?.GetType().Name;
        var viewName = viewModelName.Replace("ViewModel", "View");

        SourceFiles = new ObservableCollection<string> { $"{viewName}.axaml", $"{viewName}.axaml.cs", $"{viewModelName}.cs", };
        SourceFile = SourceFiles.First();
        ShowCode = false;
    }

    private void CreateTitle(ProductInfoBase product)
    {
        var prefix = "Demo Center v.1.0";
        var groupPrefix = string.Empty;
        var title = string.Empty;

        if (product is PageInfo page)
        {
            var group = Products.OfType<GroupInfo>().FirstOrDefault(x => x.Pages.Contains(page));
            if (group != null)
                groupPrefix = $" - {group.Title}";
            title = $" - {page.Title}";
        }
        else
            title = $" - {product?.Title}";
        Title = $"{prefix}{groupPrefix}{title}";
    }

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

    private bool AllowPreviousNavigation() => CurrentProductItem != null && flatProducts.Any() && CurrentProductItem != flatProducts[1];

    private bool AllowNextNavigation() => CurrentProductItem != null && flatProducts.Any() && CurrentProductItem != flatProducts.Last();
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
