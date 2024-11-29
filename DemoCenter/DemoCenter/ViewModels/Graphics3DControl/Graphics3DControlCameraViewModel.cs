using System.Numerics;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlCameraViewModel : Graphics3DControlViewModel
{
	const float size = 10;
    const float max = size;
    const float min = -size;
    
    static Vector3 v1 = new(min, min, max);
    static Vector3 v2 = new(max, min, max);
    static Vector3 v3 = new(max, max, max);
    static Vector3 v4 = new(min, max, max);
    static Vector3 v5 = new(min, max, min);
    static Vector3 v6 = new(min, min, min);
    static Vector3 v7 = new(max, min, min);
    static Vector3 v8 = new(max, max, min);

    [ObservableProperty] List<MeshViewModel> meshes = new()
    {
	    new(new Vertex3D[]
		    {
			    new() { Position = v1, TextureCoord = new Vector2(0, 1), Normal = new Vector3(0, 0, 1) },
			    new() { Position = v2, TextureCoord = new Vector2(1, 1), Normal = new Vector3(0, 0, 1) },
			    new() { Position = v3, TextureCoord = new Vector2(1, 0), Normal = new Vector3(0, 0, 1) },
			    new() { Position = v4, TextureCoord = new Vector2(0, 0), Normal = new Vector3(0, 0, 1) }
		    },
		    new uint[] { 0, 1, 2, 2, 3, 0 })
	    {
		    MaterialKey = "Front",
		    Name = "Front",
	    },
	    new(new Vertex3D[]
		    {
			    new() { Position = v8, TextureCoord = new Vector2(0, 0), Normal = new Vector3(0, 0, -1) },
			    new() { Position = v7, TextureCoord = new Vector2(0, 1), Normal = new Vector3(0, 0, -1) },
			    new() { Position = v6, TextureCoord = new Vector2(1, 1), Normal = new Vector3(0, 0, -1) },
			    new() { Position = v5, TextureCoord = new Vector2(1, 0), Normal = new Vector3(0, 0, -1) }
		    },
		    new uint[] { 0, 1, 2, 2, 3, 0 })
	    {
		    MaterialKey = "Back",
		    Name = "Back"
	    },
	    new(new Vertex3D[]
		    {
			    new() { Position = v1, TextureCoord = new Vector2(1, 1), Normal = new Vector3(-1, 0, 0) },
			    new() { Position = v4, TextureCoord = new Vector2(1, 0), Normal = new Vector3(-1, 0, 0) },
			    new() { Position = v5, TextureCoord = new Vector2(0, 0), Normal = new Vector3(-1, 0, 0) },
			    new() { Position = v6, TextureCoord = new Vector2(0, 1), Normal = new Vector3(-1, 0, 0) }
		    },
			new uint[] { 0, 1, 2, 2, 3, 0 })
	    {
		    MaterialKey = "Left",
		    Name = "Left"
	    },
	    new(new Vertex3D[]
		    {
			    new() { Position = v2, TextureCoord = new Vector2(0, 1), Normal = new Vector3(1, 0, 0) },
			    new() { Position = v7, TextureCoord = new Vector2(1, 1), Normal = new Vector3(1, 0, 0) },
			    new() { Position = v8, TextureCoord = new Vector2(1, 0), Normal = new Vector3(1, 0, 0) },
			    new() { Position = v3, TextureCoord = new Vector2(0, 0), Normal = new Vector3(1, 0, 0) }
		    },
		    new uint[] { 0, 1, 2, 2, 3, 0 })
	    {
		    MaterialKey = "Right",
		    Name = "Right"
	    },
	    new(new Vertex3D[]
		    {
			    new() { Position = v8, TextureCoord = new Vector2(1, 0), Normal = new Vector3(0, 1, 0)},
			    new() { Position = v5, TextureCoord = new Vector2(0, 0), Normal = new Vector3(0, 1, 0)},
			    new() { Position = v4, TextureCoord = new Vector2(0, 1), Normal = new Vector3(0, 1, 0)},
			    new() { Position = v3, TextureCoord = new Vector2(1, 1), Normal = new Vector3(0, 1, 0)}
		    },
		    new uint[] { 0, 1, 2, 2, 3, 0 })
	    {
		    MaterialKey = "Top",
		    Name = "Top"
	    },
	    new(new Vertex3D[]
		    {
			    new() { Position = v2, TextureCoord = new Vector2(1, 0), Normal = new Vector3(0, -1, 0) },
			    new() { Position = v1, TextureCoord = new Vector2(0, 0), Normal = new Vector3(0, -1, 0) },
			    new() { Position = v6, TextureCoord = new Vector2(0, 1), Normal = new Vector3(0, -1, 0) },
			    new() { Position = v7, TextureCoord = new Vector2(1, 1), Normal = new Vector3(0, -1, 0) }
		    },
		    new uint[] { 0, 1, 2, 2, 3, 0 })
	    {
		    MaterialKey = "Bottom",
		    Name = "Bottom"
	    }
    };
    [ObservableProperty] List<CameraItem> cameras = new() { new CameraItem(new PerspectiveCamera()), new(new IsometricCamera()) };
    [ObservableProperty] CameraItem selectedCamera;
    [ObservableProperty] List<SimplePbrMaterial> materials = new()
    {
	    new SimplePbrMaterial(Colors.Red, "Front"),
	    new(Colors.Lime, "Back"),
	    new(Colors.Blue, "Left"),
	    new(Colors.Yellow, "Right"),
	    new(Colors.Magenta, "Top"),
	    new(Colors.Cyan, "Bottom"),
    };

    public Graphics3DControlCameraViewModel()
    {
	    SelectedCamera = Cameras[0];
    }
}

public class CameraItem
{
	public Camera Camera { get; }
	public bool IsPerspective => Camera is PerspectiveCamera;
	public float FieldOfView
	{
		get => Camera is PerspectiveCamera camera ? camera.FieldOfView : default;
		set
		{
			if (Camera is PerspectiveCamera camera)
				camera.FieldOfView = value;
		}
	}
	public string Name => Camera is PerspectiveCamera ? "Perspective Camera" : "Isometric Camera";

	public CameraItem(Camera camera)
	{
		Camera = camera;
	}
}