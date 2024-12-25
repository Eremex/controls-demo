using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;
using JeremyAnsel.Media.WavefrontObj;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlLinesViewModel : Graphics3DControlViewModel
{
    static Vertex3D ToVertex3D(ObjVertex vertex, ObjVector3 normal) => new()
    {
        Position = new Vector3(vertex.Position.X, vertex.Position.Y, vertex.Position.Z),
        Normal = new Vector3(normal.X, normal.Y, normal.Z)
    };
    static GeometryModel3D LoadModel(string modelName, string resourceName)
    {
        var assembly = Assembly.GetAssembly(typeof(Graphics3DControlViewModel));
        var stream = assembly!.GetManifestResourceStream(resourceName);
        var obj = ObjFile.FromStream(stream);
        var vertices = new List<Vertex3D>();
        var indices = new List<uint>();
        uint index = 0;
        foreach (var face in obj.Faces)
        {
            for (int i = 0; i < face.Vertices.Count; i++)
            {
                var vertex = face.Vertices[i];
                vertices.Add(ToVertex3D(obj.Vertices[vertex.Vertex - 1], obj.VertexNormals[vertex.Normal - 1]));
                if (i > 0)
                {
                    indices.Add(index - 1);
                    indices.Add(index);
                }
                index++;
            }
        }

        return new GeometryModel3D
        {
            Name = modelName,
            Meshes = { new MeshGeometry3D { Vertices = vertices.ToArray(), Indices = indices.ToArray(), FillType = MeshFillType.Lines } }
        };
    }

    [ObservableProperty] ObservableCollection<GeometryModel3D> models = new();
    [ObservableProperty] float lineWidth = 1;

    public Graphics3DControlLinesViewModel()
    {
        var model = LoadModel("Teapot", "DemoCenter.Resources.Graphics3D.Models.Teapot_Quad.obj");
        model.TranslateToZero();
        models.Add(model);
    }
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(LineWidth))
            Models[0].Meshes[0].PrimitiveSize = LineWidth;
    }
}
