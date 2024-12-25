using System.ComponentModel;
using System.Reflection;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.ListView;
using Eremex.AvaloniaUI.Icons;

namespace DemoCenter.ViewModels;

public partial class SvgIconsBrowserViewModel : PageViewModelBase
{
    [ObservableProperty] private List<SvgIconCategoryViewModel> categories;
    [ObservableProperty] private SvgIconCategoryViewModel focusedCategory;
    [ObservableProperty] private List<SvgIconViewModel> items;
    [ObservableProperty] private object focusedItem;
    [ObservableProperty] private string searchText;
    [ObservableProperty] private string iconPath;
    [ObservableProperty] private string iconAxamlPath;
    [ObservableProperty] private bool hintVisible;

    public SvgIconsBrowserViewModel()
    {
        LoadIcons();
        focusedCategory = Categories.Count > 0? Categories[0]: null;
        focusedItem = Items[1];
    }

    private readonly string[] prohibitedCategories = new[] { "Library", "Logo", "Painting", "PCB", "Scheme", "SimOne", "SimPCB", "Simtera", "Status", "_8", "_144" };
    private readonly Dictionary<string, string> mappingCategories = new()
    {
        {"_3D", "3D"}, {"Dimensional_lines", "Dimensions"}, {"Drawing", "Graphical Editor"},
        {"CAM", "Factory"}, {"Graphics", "Charts"}, {"Models", "Schematic"}, {"Scaling", "Zoom"}, {"_12", "Glyphs"}, {"_40", "Status"}
    };
    private void LoadIcons()
    {
        var cat = typeof(Basic).Assembly.GetTypes().Where(t => t.Namespace != null && t.Namespace.Contains(".Icons")).ToList();
        List<SvgIconCategoryViewModel> catList = new List<SvgIconCategoryViewModel>(cat.Count);
        List<SvgIconViewModel> iconList = new List<SvgIconViewModel>();
        foreach(var c in cat)
        {
            if(prohibitedCategories.Contains(c.Name))
                continue;
            var icons = c.GetProperties(BindingFlags.Static | BindingFlags.Public).Where(p => p.PropertyType == typeof(IImage)).ToList();
            if(icons.Count == 0)
                continue;
            string name = c.Name;
            mappingCategories.TryGetValue(name, out name);
            name ??= c.Name;
            var cm = new SvgIconCategoryViewModel(this) { DisplayName = name, Name = c.Name, IsChecked = true };
            cm.PropertyChanged += OnSvgCategoryPropertyChanged;

            foreach(var pi in icons)
            {
                var ivm = new SvgIconViewModel(cm, pi.Name, (IImage)pi.GetValue(null));
                iconList.Add(ivm);
            }
            catList.Add(cm);
        }

        Categories = catList;
        Items = iconList;
    }
    
    void OnSvgCategoryPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(SvgIconCategoryViewModel.IsChecked))
        {
            RequestUpdateData?.Invoke();
        } 
    }

    public event Action RequestUpdateData;

    public event Action<SvgIconCategoryViewModel> RequestScrollToCategory;
    partial void OnFocusedCategoryChanged(SvgIconCategoryViewModel value)
    {
        if(value is { IsChecked: true })
        {
            RequestScrollToCategory?.Invoke(value);
        }
    }
    
    partial void OnFocusedItemChanged(object value)
    {
        if(value is SvgIconViewModel vm)
        {
            IconPath = vm.Path;
            IconAxamlPath = vm.AxamlPath;
            HintVisible = true;
        }
        else
        {
            IconPath = string.Empty;
            IconAxamlPath = string.Empty;
            HintVisible = false;
        }
    }

    public void OnCustomFilter(ListViewFilterEventArgs e)
    {
        SvgIconViewModel vm = (SvgIconViewModel)e.Item;
        e.Visible = vm.Category.IsChecked && (string.IsNullOrEmpty(SearchText) || vm.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
    }

    public void OnCategoryCheckedChanged(SvgIconCategoryViewModel cat)
    {
        RequestUpdateData?.Invoke();
        if(cat.IsChecked)
        {
            RequestScrollToCategory?.Invoke(cat);
        }
    }
}

public partial class SvgIconCategoryViewModel : ObservableObject, IComparable<SvgIconCategoryViewModel>, IComparable
{
    [ObservableProperty] private bool isChecked;
    [ObservableProperty] private string name;
    [ObservableProperty] private string displayName;
    [ObservableProperty] private bool isExpanded;

    public SvgIconCategoryViewModel(SvgIconsBrowserViewModel owner)
    {
        this.owner = owner;
    }

    private SvgIconsBrowserViewModel owner;
    
    public int CompareTo(SvgIconCategoryViewModel other)
    {
        return String.Compare(Name, other.Name, StringComparison.Ordinal);
    }
    public int CompareTo(object other)
    {
        return String.Compare(Name, ((SvgIconCategoryViewModel)other).Name, StringComparison.Ordinal);
    }

    partial void OnIsCheckedChanged(bool value)
    {
        this.owner.OnCategoryCheckedChanged(this);
    }
}

public partial class SvgIconViewModel : ObservableObject, IComparable<SvgIconViewModel>, IComparable
{
    [ObservableProperty] private string name;
    [ObservableProperty] private string path;
    [ObservableProperty] private string axamlPath;
    [ObservableProperty] private IImage icon;
    [ObservableProperty] private SvgIconCategoryViewModel category;

    public SvgIconViewModel(SvgIconCategoryViewModel category, string name, IImage icon)
    {
        this.category = category;
        this.name = name;
        path = $"{Category.Name}.{Name}";
        axamlPath = $"{{x:Static mxi:{Category.Name}.{Name}}}";
        this.icon = icon;
    }

    public string CategoryName => Category?.DisplayName;
    public int CompareTo(SvgIconViewModel other)
    {
        return String.Compare(Name, other.Name, StringComparison.Ordinal);
    }

    public int CompareTo(object other)
    {
        return String.Compare(Name, ((SvgIconViewModel)other)?.Name, StringComparison.Ordinal);
    }
}