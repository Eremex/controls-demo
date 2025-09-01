using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using Assimp;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlLinesViewModel : Graphics3DControlViewModel
{
    static GeometryModel3D LoadModel(string modelName, string resourceName, string materialKey = null)
    {
        var assembly = Assembly.GetAssembly(typeof(Graphics3DControlViewModel));
        var stream = assembly!.GetManifestResourceStream(resourceName);
        using var context = new AssimpContext();
        var scene = context.ImportFileFromStream(stream);

        var model = new GeometryModel3D();
        if (!string.IsNullOrEmpty(modelName))
            model.Hint = modelName;
        
        foreach (var mesh in scene.Meshes)
        {
            var vertices = new Vertex3D[mesh.VertexCount];
            for (int i = 0; i < mesh.VertexCount; ++i)
                vertices[i] = new Vertex3D { Normal = mesh.Normals[i], Position = mesh.Vertices[i] };
            var indices = new List<uint>();
            foreach (var face in mesh.Faces)
            {
                for (int i = 0; i < face.IndexCount - 1; ++i)
                {
                    indices.Add((uint)face.Indices[i]);
                    indices.Add((uint)face.Indices[i + 1]);
                }
            }
            model.Meshes.Add(new MeshGeometry3D { Vertices = vertices, Indices = indices.ToArray(), FillType = MeshFillType.Lines });
        }

        return model;
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
        {
            foreach (var mesh in Models.Single().Meshes)
                mesh.PrimitiveSize = LineWidth;
        }
    }
}
