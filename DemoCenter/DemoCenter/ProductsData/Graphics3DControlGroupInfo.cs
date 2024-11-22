using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public class Graphics3DControlGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Overview", title: "Overview",
                description: "TODO",
                viewModelGetter: () => new Graphics3DControlOverviewViewModel(),
                new VersionInfo(1, 1)),
            
            new (name: "STL Model", title: "STL Model",
                description: "TODO",
                viewModelGetter: () => new Graphics3DControlStlViewModel(),
                new VersionInfo(1, 1)),
            
            new (name: "Simple Materials", title: "Simple Materials",
            description: "TODO",
            viewModelGetter: () => new Graphics3DControlSimpleMaterialsViewModel(),
            new VersionInfo(1, 1)),
            
            new (name: "Textured Materials", title: "Textured Materials",
                description: "TODO",
                viewModelGetter: () => new Graphics3DControlTexturedMaterialsViewModel(),
                new VersionInfo(1, 1))
        };
    }
}
