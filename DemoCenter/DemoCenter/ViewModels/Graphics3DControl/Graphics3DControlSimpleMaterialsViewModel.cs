using System.Collections.ObjectModel;
using System.Numerics;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;
using JeremyAnsel.Media.WavefrontObj;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlSimpleMaterialsViewModel : Graphics3DControlViewModel
{
    static Vertex3D ToVertex3D(ObjVertex vertex, ObjVector3 normal) => new()
    {
        Position = new Vector3(vertex.Position.X, vertex.Position.Y, vertex.Position.Z),
        Normal = new Vector3(normal.X, normal.Y, normal.Z)
    };
    static GeometryModel3D LoadModel(string resourceName)
    {
        var assembly = Assembly.GetAssembly(typeof(Graphics3DControlViewModel));
        var stream = assembly!.GetManifestResourceStream(resourceName);
        var obj = ObjFile.FromStream(stream);
        var vertices = new List<Vertex3D>();
        var indices = new List<uint>();
        uint index = 0;
        foreach (var face in obj.Faces)
        {
            foreach (var vertex in face.Vertices)
            {
                vertices.Add(ToVertex3D(obj.Vertices[vertex.Vertex - 1], obj.VertexNormals[vertex.Normal - 1]));
                indices.Add(index++);
            }
        }

        return new GeometryModel3D { Meshes = { new MeshGeometry3D { Vertices = vertices.ToArray(), Indices = indices.ToArray() } } };
    }

    [ObservableProperty] ObservableCollection<GeometryModel3D> models = new();

    public Graphics3DControlSimpleMaterialsViewModel()
    {
        var model = LoadModel("DemoCenter.Resources.Graphics3D.Models.Sphere.obj");
        models.Add(model);
    }
}
