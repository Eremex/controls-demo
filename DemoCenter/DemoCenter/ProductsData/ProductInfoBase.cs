using System.Reflection;
using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public abstract class ProductInfoBase
{
    public string Name { get; }
    public string Title { get; }
    public Func<string> DescriptionGetter { get; set; }

    public string Description => DescriptionGetter?.Invoke();
    public Func<PageViewModelBase> ViewModelGetter { get; }
    public bool ShowInWeb { get; }
    public bool IsNew => Introduced.Matches(App.Version) && !IsUpdated;
    public bool IsUpdated => Updated?.Matches(App.Version) is true;
    public VersionInfo Introduced { get; }
    public VersionInfo? Updated { get; }
    public abstract bool HasChildren { get; }
    public bool IsWebApp => App.IsWebApp;
    

    protected ProductInfoBase(string name, string title, Func<PageViewModelBase> viewModelGetter, Func<string> descriptionGetter, VersionInfo? introduced, VersionInfo? updated, bool showInWeb)
    {
        Name = name;
        Title = title;
        DescriptionGetter = descriptionGetter;
        ViewModelGetter = viewModelGetter;
        ShowInWeb = showInWeb;
        Introduced = introduced ?? new VersionInfo(0, 0);
        Updated = updated;
    }
}

public readonly struct VersionInfo
{
    public int Major { get; } = 0;
    public int Minor { get; } = 0;

    public VersionInfo(int major, int minor)
    {
        Major = major;
        Minor = minor;
    }
    public VersionInfo(Assembly assembly)
    {
        if (assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true).SingleOrDefault() is AssemblyFileVersionAttribute attribute)
        {
            var parts = attribute.Version.Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length > 0 && int.TryParse(parts[0], out var major))
                Major = major;
            if (parts.Length > 1 && int.TryParse(parts[1], out var minor))
                Minor = minor;
        }
    }
    public bool Matches(VersionInfo version) => version.Major == Major && version.Minor == Minor;
}
