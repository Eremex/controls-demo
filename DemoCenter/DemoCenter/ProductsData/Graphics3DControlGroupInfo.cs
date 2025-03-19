using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public class Graphics3DControlGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Overview", title: "Overview",
                description: Resources.Graphics3DControlAllowsYouToVisualizeAndIn,
                viewModelGetter: () => new Graphics3DControlOverviewViewModel(),
                new VersionInfo(1, 1)),
            
            new (name: "Lines", title: "Line",
                description: Resources.ThisExampleDemonstratesAGraphics3DControlT,
                viewModelGetter: () => new Graphics3DControlLinesViewModel(),
                new VersionInfo(1, 1)),

            new (name: "Points", title: "Points",
                description: Resources.ThisExampleDemonstratesAGraphics3DControlT1,
                viewModelGetter: () => new Graphics3DControlPointsViewModel(),
                new VersionInfo(1, 1)),
            
            new (name: "Transformations", title: "Transformations",
                description: Resources.ThisExampleDemonstratesDynamicTransformati,
                viewModelGetter: () => new Graphics3DControlTransformationViewModel(),
                new VersionInfo(1, 1)),

            new (name: "STL Model", title: "STL Model",
                description: Resources.ThisExampleDemonstratesAGraphics3DControlD,
                viewModelGetter: () => new Graphics3DControlStlViewModel(),
                new VersionInfo(1, 1)),
            
            new (name: "Simple Materials", title: "Simple Materials",
            description: Resources.ThisExampleDemonstratesAGraphics3DControlD1,
            viewModelGetter: () => new Graphics3DControlSimpleMaterialsViewModel(),
            new VersionInfo(1, 1)),
            
            new (name: "Textured Materials", title: "Textured Materials",
                description: Resources.InThisExampleAGraphics3DControlDisplaysA3D,
                viewModelGetter: () => new Graphics3DControlTexturedMaterialsViewModel(),
                new VersionInfo(1, 1)),
            
            new (name: "Camera", title: "Camera",
                description: Resources.ThisExampleDemonstratesTheIsometricAndPers,
                viewModelGetter: () => new Graphics3DControlCameraViewModel(),
                new VersionInfo(1, 1))
        };
    }
}
