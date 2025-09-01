#nullable enable
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlHighlightingViewModel : Graphics3DControlViewModel
{
    [ObservableProperty] ObservableCollection<GeometryModel3D> models = new();
    [ObservableProperty] SelectableElement? highlightedElement;
    [ObservableProperty] SelectableElement? selectedElement;

    public Graphics3DControlHighlightingViewModel()
    {
	    const float offset = 3;
        AddCube(new Vector3(offset, 0, -offset));
        AddPyramid(new Vector3(-offset, 0, offset));
        var model = Model3DLoader.LoadModel("Teapot Model", "DemoCenter.Resources.Graphics3D.Models.Teapot.obj");
        model.Meshes.Single().Hint = "Teapot Mesh";
        models.Add(model);
    }
    void AddCube(Vector3 offset)
    {
	    const float size = 1;
	    const float max = size;
	    const float min = -size;
    
	    var v1 = new Vector3(min, min, max) + offset;
	    var v2 = new Vector3(max, min, max) + offset;
	    var v3 = new Vector3(max, max, max) + offset;
	    var v4 = new Vector3(min, max, max) + offset;
	    var v5 = new Vector3(min, max, min) + offset;
	    var v6 = new Vector3(min, min, min) + offset;
	    var v7 = new Vector3(max, min, min) + offset;
	    var v8 = new Vector3(max, max, min) + offset;

	    var geometry = new GeometryModel3D
	    {
		    Meshes =
		    {
			    new()
			    {
				    Vertices =new Vertex3D[]
				    {
					    new() { Position = v1, TextureCoord = new Vector2(0, 1), Normal = new Vector3(0, 0, 1) },
					    new() { Position = v2, TextureCoord = new Vector2(1, 1), Normal = new Vector3(0, 0, 1) },
					    new() { Position = v3, TextureCoord = new Vector2(1, 0), Normal = new Vector3(0, 0, 1) },
					    new() { Position = v4, TextureCoord = new Vector2(0, 0), Normal = new Vector3(0, 0, 1) }
				    },
				    Indices =new uint[] { 0, 1, 2, 2, 3, 0 },
				    Hint = "Front"
			    },
			    new()
			    {
				    Vertices = new Vertex3D[]
				    {
					    new() { Position = v8, TextureCoord = new Vector2(0, 0), Normal = new Vector3(0, 0, -1) },
					    new() { Position = v7, TextureCoord = new Vector2(0, 1), Normal = new Vector3(0, 0, -1) },
					    new() { Position = v6, TextureCoord = new Vector2(1, 1), Normal = new Vector3(0, 0, -1) },
					    new() { Position = v5, TextureCoord = new Vector2(1, 0), Normal = new Vector3(0, 0, -1) }
				    },
				    Indices = new uint[] { 0, 1, 2, 2, 3, 0 },
				    Hint = "Back"
			    },
			    new()
			    {
				    Vertices = new Vertex3D[]
				    {
					    new() { Position = v1, TextureCoord = new Vector2(1, 1), Normal = new Vector3(-1, 0, 0) },
					    new() { Position = v4, TextureCoord = new Vector2(1, 0), Normal = new Vector3(-1, 0, 0) },
					    new() { Position = v5, TextureCoord = new Vector2(0, 0), Normal = new Vector3(-1, 0, 0) },
					    new() { Position = v6, TextureCoord = new Vector2(0, 1), Normal = new Vector3(-1, 0, 0) }
				    },
				    Indices = new uint[] { 0, 1, 2, 2, 3, 0 },
				    Hint = "Left"
			    },
			    new()
			    {
				    Vertices = new Vertex3D[]
				    {
					    new() { Position = v2, TextureCoord = new Vector2(0, 1), Normal = new Vector3(1, 0, 0) },
					    new() { Position = v7, TextureCoord = new Vector2(1, 1), Normal = new Vector3(1, 0, 0) },
					    new() { Position = v8, TextureCoord = new Vector2(1, 0), Normal = new Vector3(1, 0, 0) },
					    new() { Position = v3, TextureCoord = new Vector2(0, 0), Normal = new Vector3(1, 0, 0) }
				    },
				    Indices = new uint[] { 0, 1, 2, 2, 3, 0 },
				    Hint = "Right"
			    },
			    new()
			    {
				    Vertices = new Vertex3D[]
				    {
					    new() { Position = v8, TextureCoord = new Vector2(1, 0), Normal = new Vector3(0, 1, 0) },
					    new() { Position = v5, TextureCoord = new Vector2(0, 0), Normal = new Vector3(0, 1, 0) },
					    new() { Position = v4, TextureCoord = new Vector2(0, 1), Normal = new Vector3(0, 1, 0) },
					    new() { Position = v3, TextureCoord = new Vector2(1, 1), Normal = new Vector3(0, 1, 0) }
				    },
				    Indices = new uint[] { 0, 1, 2, 2, 3, 0 },
				    Hint = "Top"
			    },
			    new()
			    {
				    Vertices = new Vertex3D[]
				    {
					    new() { Position = v2, TextureCoord = new Vector2(1, 0), Normal = new Vector3(0, -1, 0) },
					    new() { Position = v1, TextureCoord = new Vector2(0, 0), Normal = new Vector3(0, -1, 0) },
					    new() { Position = v6, TextureCoord = new Vector2(0, 1), Normal = new Vector3(0, -1, 0) },
					    new() { Position = v7, TextureCoord = new Vector2(1, 1), Normal = new Vector3(0, -1, 0) }
				    },
				    Indices = new uint[] { 0, 1, 2, 2, 3, 0 },
				    Hint = "Bottom"
			    }
		    },
		    Hint = "Cube"
	    };
	    Models.Add(geometry);
    }
    void AddPyramid(Vector3 offset)
    {
        var basis = new MeshGeometry3D
        {
            Vertices = new[]
            {
                new Vertex3D(new Vector3(-1, 0, -1) + offset, -Vector3.UnitY),
                new Vertex3D(new Vector3(-1, 0, 1) + offset, -Vector3.UnitY),
                new Vertex3D(new Vector3(1, 0, 1) + offset, -Vector3.UnitY),
                new Vertex3D(new Vector3(1, 0, -1) + offset, -Vector3.UnitY)
            },
            Indices = new uint[] { 0, 1, 2, 0, 2, 3 },
            Hint = "Basis"
        };
        var triangle1 = CreateTriangle("Left", basis.Vertices[0].Position, basis.Vertices[1].Position);
        var triangle2 = CreateTriangle("Front", basis.Vertices[1].Position, basis.Vertices[2].Position);
        var triangle3 = CreateTriangle("Right", basis.Vertices[2].Position, basis.Vertices[3].Position);
        var triangle4 = CreateTriangle("Back", basis.Vertices[3].Position, basis.Vertices[0].Position);
        Models.Add(new GeometryModel3D {
            Hint = "Pyramid",
            Meshes = { basis, triangle1, triangle2, triangle3, triangle4 }
        });

        MeshGeometry3D CreateTriangle(string name, Vector3 p1, Vector3 p2)
        {
            var p3 = new Vector3(0, 2, 0) + offset;
            var normal = Vector3.Cross(p2 - p1, p3 - p1);
            return new MeshGeometry3D
            {
                Vertices = new[]
                {
                    new Vertex3D(p2, normal),
                    new Vertex3D(p1, normal),
                    new Vertex3D(p3, normal)
                },
                Indices = new uint[] { 0, 1, 2 },
                Hint = name
            };
        }
    }
}
