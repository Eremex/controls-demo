using DemoCenter.ViewModels;

namespace DemoCenter.ProductsData;

public class Graphics3DControlGroupInfo
{
    internal static List<PageInfo> Create()
    {
        return new List<PageInfo>
        {
            new (name: "Overview", title: "Overview",
                description: "Graphics3DControl allows you to visualize and interact with 3D models. Use the Properties pane on the right to customize the control's basic rendering and behavior settings.",
                viewModelGetter: () => new Graphics3DControlOverviewViewModel(),
                new VersionInfo(1, 1)),
            
            new (name: "Lines", title: "Line",
                description: "This example demonstrates a Graphics3DControl that renders a 3D model using lines.",
                viewModelGetter: () => new Graphics3DControlLinesViewModel(),
                new VersionInfo(1, 1)),

            new (name: "Points", title: "Points",
                description: "This example demonstrates a Graphics3DControl that renders a 3D model using points.",
                viewModelGetter: () => new Graphics3DControlPointsViewModel(),
                new VersionInfo(1, 1)),
            
            new (name: "Transformations", title: "Transformations",
                description: "This example demonstrates dynamic transformations applied to 3D Models in a Graphics3DControl.",
                viewModelGetter: () => new Graphics3DControlTransformationViewModel(),
                new VersionInfo(1, 1)),

            new (name: "STL Model", title: "STL Model",
                description: "This example demonstrates a Graphics3DControl displaying a large 3D model loaded from an STL file. The STLDotNet6.Formats.StereoLithography library is used to parse the STL file, and to obtain data to initialize vertices and materials of the created model.",
                viewModelGetter: () => new Graphics3DControlStlViewModel(),
                new VersionInfo(1, 1)),
            
            new (name: "Simple Materials", title: "Simple Materials",
            description: "This example demonstrates a Graphics3DControl displaying a 3D model with a simple material. Use the Properties pane on the right to customize the material settings.",
            viewModelGetter: () => new Graphics3DControlSimpleMaterialsViewModel(),
            new VersionInfo(1, 1)),
            
            new (name: "Textured Materials", title: "Textured Materials",
                description: "In this example, a Graphics3DControl displays a 3D model with a textured material. Use the Materials pane on the right to choose the texture.",
                viewModelGetter: () => new Graphics3DControlTexturedMaterialsViewModel(),
                new VersionInfo(1, 1)),
            
            new (name: "Camera", title: "Camera",
                description: "This example demonstrates the isometric and perspective cameras supported by Graphics3DControl. Use the Camera pane on the right to choose camera mode and one of predefined camera views.",
                viewModelGetter: () => new Graphics3DControlCameraViewModel(),
                new VersionInfo(1, 1))
        };
    }
}
