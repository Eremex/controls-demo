using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public class Graphics3DControlGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Overview", title: "Overview",
                viewModelGetter: () => new Graphics3DControlOverviewViewModel(),
                descriptionGetter: () => Resources.Graphics3DControlAllowsYouToVisualizeAndIn, introduced: new VersionInfo(1, 1)),
            
            new (name: "Lines", title: "Line",
                viewModelGetter: () => new Graphics3DControlLinesViewModel(),
                descriptionGetter: () => Resources.ThisExampleDemonstratesAGraphics3DControlT, introduced: new VersionInfo(1, 1)),

            new (name: "Points", title: "Points",
                viewModelGetter: () => new Graphics3DControlPointsViewModel(),
                descriptionGetter: () => Resources.ThisExampleDemonstratesAGraphics3DControlT1, introduced: new VersionInfo(1, 1)),
            
            new (name: "Transformations", title: "Transformations",
                viewModelGetter: () => new Graphics3DControlTransformationViewModel(),
                descriptionGetter: () => Resources.ThisExampleDemonstratesDynamicTransformati, introduced: new VersionInfo(1, 1)),

            new (name: "STL Model", title: "STL Model",
                viewModelGetter: () => new Graphics3DControlStlViewModel(),
                descriptionGetter: () => Resources.ThisExampleDemonstratesAGraphics3DControlD, introduced: new VersionInfo(1, 1)),
            
            new (name: "Simple Materials", title: "Simple Materials",
            viewModelGetter: () => new Graphics3DControlSimpleMaterialsViewModel(),
            descriptionGetter: () => Resources.ThisExampleDemonstratesAGraphics3DControlD1, introduced: new VersionInfo(1, 1)),
            
            new (name: "Textured Materials", title: "Textured Materials",
                viewModelGetter: () => new Graphics3DControlTexturedMaterialsViewModel(),
                descriptionGetter: () => Resources.InThisExampleAGraphics3DControlDisplaysA3D, introduced: new VersionInfo(1, 1)),
            
            new (name: "Camera", title: "Camera",
                viewModelGetter: () => new Graphics3DControlCameraViewModel(),
                descriptionGetter: () => Resources.ThisExampleDemonstratesTheIsometricAndPers, introduced: new VersionInfo(1, 1))
        };
    }
}
