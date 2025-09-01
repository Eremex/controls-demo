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
    public bool IsWebApp => App.IsWebApp;
    public abstract VersionInfo Introduced { get; }
    public abstract VersionInfo? Updated { get; }
    public abstract bool HasChildren { get; }
    
    protected ProductInfoBase(string name, string title, Func<PageViewModelBase> viewModelGetter, Func<string> descriptionGetter, bool showInWeb)
    {
        Name = name;
        Title = title;
        DescriptionGetter = descriptionGetter;
        ViewModelGetter = viewModelGetter;
        ShowInWeb = showInWeb;
    }
}

public readonly struct VersionInfo : IComparable<VersionInfo>
{
    public static VersionInfo Max(VersionInfo v1, VersionInfo? v2)
    {
        if (v2.HasValue)
        {
            int compare = v1.CompareTo(v2.Value);
            if (compare < 0)
                return v2.Value;
        }
        return v1;
    }

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
    public int CompareTo(VersionInfo other)
    {
        int result = Major.CompareTo(other.Major);
        return result == 0 ? Minor.CompareTo(other.Minor) : result;
    }
}
