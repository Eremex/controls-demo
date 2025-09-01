using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Numerics;
using System.Reflection;
using Assimp;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;
using Material = Eremex.AvaloniaUI.Controls3D.Material;
using Vector = Avalonia.Vector;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlStlViewModel : Graphics3DControlViewModel
{
    static GeometryModel3D LoadModel(string modelName, string resourceName)
    {
        var assembly = Assembly.GetAssembly(typeof(Graphics3DControlViewModel));
        var stream = assembly!.GetManifestResourceStream(resourceName);
        using var archive = new ZipArchive(stream!, ZipArchiveMode.Read);
        using var stlStream = archive.Entries.Single().Open();
        
        using var context = new AssimpContext();
        var scene = context.ImportFileFromStream(stlStream);

        var model = new GeometryModel3D { Name = modelName };
        int geometryIndex = 0;
        foreach (var mesh in scene.Meshes)
        {
            var vertices = new Vertex3D[mesh.VertexCount];
            for (int i = 0; i < mesh.VertexCount; ++i)
                vertices[i] = new Vertex3D { Normal = mesh.Normals[i], Position = mesh.Vertices[i] };
            model.Meshes.Add(new MeshGeometry3D
            {
                Hint = $"Solid {geometryIndex++}",
                MaterialKey = mesh.Name,
                Vertices = vertices,
                Indices = mesh.GetUnsignedIndices().ToArray()
            });
        }

        return model;
    }

    [ObservableProperty] ObservableCollection<GeometryModel3D> models = new();
    [ObservableProperty] ObservableCollection<Material> materials = new();
    [ObservableProperty] Skybox skybox;

    public Graphics3DControlStlViewModel()
    {
        skybox = CreateSkybox();
        
        var model = LoadModel("ddBox", "DemoCenter.Resources.Graphics3D.Models.ddBox-C1.zip");
        models.Add(model);

        materials.Add(new SimplePbrMaterial { Key = "0", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 1f, Roughness = 0f, Metallic = 0f, Albedo = new Vector3(1f, 1f, 1f) });
        materials.Add(new SimplePbrMaterial { Key = "1", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 1f, Roughness = 0.5f, Metallic = 0.5f, Albedo = new Vector3(0f, 0f, 0f) });
        materials.Add(new SimplePbrMaterial { Key = "2", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 1f, Roughness = 0.5f, Metallic = 0.5f, Albedo = new Vector3(1f, 1f, 1f) });
        materials.Add(new SimplePbrMaterial { Key = "3", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 1f, Roughness = 0f, Metallic = 1f, Albedo = new Vector3(0.6117647f, 0.33333334f, 0.15294118f) });
        materials.Add(new SimplePbrMaterial { Key = "4", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.1254902f, 0.1254902f, 0.1254902f) });
        materials.Add(new SimplePbrMaterial { Key = "5", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.6627451f, 0.69803923f, 0.7647059f) });
        materials.Add(new SimplePbrMaterial { Key = "6", Emission = new Vector3(0.522353f, 0.4329412f, 0.3176471f), AmbientOcclusion = 0.30200002f, Roughness = 0f, Metallic = 0.051000003f, Albedo = new Vector3(0.87058824f, 0.72156864f, 0.5294118f) });
        materials.Add(new SimplePbrMaterial { Key = "7", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.7529412f, 0.7529412f, 0.7529412f) });
        materials.Add(new SimplePbrMaterial { Key = "8", Emission = new Vector3(0.5764706f, 0.5764706f, 0.5764706f), AmbientOcclusion = 0.30200002f, Roughness = 0f, Metallic = 0.051000003f, Albedo = new Vector3(0.9607843f, 0.9607843f, 0.9607843f) });
        materials.Add(new SimplePbrMaterial { Key = "9", Emission = new Vector3(0.07529412f, 0.03764706f, 0.03764706f), AmbientOcclusion = 0.30200002f, Roughness = 0f, Metallic = 0.051000003f, Albedo = new Vector3(0.1254902f, 0.0627451f, 0.0627451f) });
        materials.Add(new SimplePbrMaterial { Key = "10", Emission = new Vector3(0.45176473f, 0.45176473f, 0.45176473f), AmbientOcclusion = 0.30200002f, Roughness = 0f, Metallic = 0.051000003f, Albedo = new Vector3(0.7529412f, 0.7529412f, 0.7529412f) });
        materials.Add(new SimplePbrMaterial { Key = "11", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.09411765f, 0.09411765f, 0.09411765f) });
        materials.Add(new SimplePbrMaterial { Key = "12", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.6392157f, 0.6627451f, 0.6784314f) });
        materials.Add(new SimplePbrMaterial { Key = "13", Emission = new Vector3(0.50200003f, 0.42325494f, 0f), AmbientOcclusion = 0.30200002f, Roughness = 0f, Metallic = 0.051000003f, Albedo = new Vector3(1f, 0.84313726f, 0f) });
        materials.Add(new SimplePbrMaterial { Key = "14", Emission = new Vector3(0.33269808f, 0.33269808f, 0.33269808f), AmbientOcclusion = 0.30200002f, Roughness = 0f, Metallic = 0.051000003f, Albedo = new Vector3(0.6627451f, 0.6627451f, 0.6627451f) });
        materials.Add(new SimplePbrMaterial { Key = "15", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.9647059f, 0.8784314f, 0.6f) });
        materials.Add(new SimplePbrMaterial { Key = "16", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.4509804f, 0.61960787f, 0.4627451f) });
        materials.Add(new SimplePbrMaterial { Key = "17", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.60784316f, 0.84313726f, 0.64705884f) });
        materials.Add(new SimplePbrMaterial { Key = "18", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.9372549f, 0.7882353f, 0.5058824f) });
        materials.Add(new SimplePbrMaterial { Key = "19", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.61960787f, 0.28627452f, 0.06666667f) });
        materials.Add(new SimplePbrMaterial { Key = "20", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.30200002f, Roughness = 0f, Metallic = 0.051000003f, Albedo = new Vector3(0f, 0f, 0f) });
        materials.Add(new SimplePbrMaterial { Key = "21", Emission = new Vector3(0.3011765f, 0.3011765f, 0.3011765f), AmbientOcclusion = 0.30200002f, Roughness = 0f, Metallic = 0.051000003f, Albedo = new Vector3(0.5019608f, 0.5019608f, 0.5019608f) });
        materials.Add(new SimplePbrMaterial { Key = "22", Emission = new Vector3(0.50200003f, 0.3779765f, 0.3996314f), AmbientOcclusion = 0.30200002f, Roughness = 0f, Metallic = 0.051000003f, Albedo = new Vector3(1f, 0.7529412f, 0.79607844f) });
        materials.Add(new SimplePbrMaterial { Key = "23", Emission = new Vector3(0.3779765f, 0.3779765f, 0.3779765f), AmbientOcclusion = 0.30200002f, Roughness = 0f, Metallic = 0.051000003f, Albedo = new Vector3(0.7529412f, 0.7529412f, 0.7529412f) });
        materials.Add(new SimplePbrMaterial { Key = "24", Emission = new Vector3(0.3405726f, 0.42522356f, 0.45278436f), AmbientOcclusion = 0.30200002f, Roughness = 0f, Metallic = 0.051000003f, Albedo = new Vector3(0.6784314f, 0.84705883f, 0.9019608f) });
        materials.Add(new SimplePbrMaterial { Key = "25", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.47843137f, 0.47843137f, 0.47843137f) });
        materials.Add(new SimplePbrMaterial { Key = "26", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.3372549f, 0.3372549f, 0.3529412f) });
        materials.Add(new SimplePbrMaterial { Key = "27", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.7490196f, 0.7490196f, 0.7490196f) });
        materials.Add(new SimplePbrMaterial { Key = "28", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(1f, 0.8039216f, 0.49411765f) });
        materials.Add(new SimplePbrMaterial { Key = "29", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.003921569f, 0.23529412f, 0.8235294f) });
        materials.Add(new SimplePbrMaterial { Key = "30", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(1f, 1f, 1f) });
        materials.Add(new SimplePbrMaterial { Key = "31", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.6862745f, 0.60784316f, 0.5294118f) });
        materials.Add(new SimplePbrMaterial { Key = "32", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.14117648f, 0.47058824f, 0.19215687f) });
        materials.Add(new SimplePbrMaterial { Key = "33", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.79607844f, 0.8235294f, 0.9372549f) });
        materials.Add(new SimplePbrMaterial { Key = "34", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.85882354f, 0.8509804f, 0.78039217f) });
        materials.Add(new SimplePbrMaterial { Key = "35", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.6509804f, 0.5882353f, 0.54901963f) });
        materials.Add(new SimplePbrMaterial { Key = "36", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.06666667f, 0.05882353f, 0.03529412f) });
        materials.Add(new SimplePbrMaterial { Key = "37", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.2509804f, 0.2509804f, 0.2509804f) });
        materials.Add(new SimplePbrMaterial { Key = "38", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.8509804f, 0.8509804f, 0.8509804f) });
        materials.Add(new SimplePbrMaterial { Key = "39", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.29803923f, 0.29803923f, 0.29803923f) });
        materials.Add(new SimplePbrMaterial { Key = "40", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.8f, 0.8039216f, 0.8f) });
        materials.Add(new SimplePbrMaterial { Key = "41", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0f, 0f, 0f) });
        materials.Add(new SimplePbrMaterial { Key = "42", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.6627451f, 0.69803923f, 0.7607843f) });
        materials.Add(new SimplePbrMaterial { Key = "43", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.8901961f, 0.90588236f, 0.9254902f) });
        materials.Add(new SimplePbrMaterial { Key = "44", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.7921569f, 0.8156863f, 0.92941177f) });
        materials.Add(new SimplePbrMaterial { Key = "45", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.5019608f, 0.5019608f, 0.5019608f) });
        materials.Add(new SimplePbrMaterial { Key = "46", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 1f, Roughness = 0f, Metallic = 0f, Albedo = new Vector3(0.9607843f, 0.9607843f, 0.9607843f) });
        materials.Add(new SimplePbrMaterial { Key = "47", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.6901961f, 0.60784316f, 0.5294118f) });
        materials.Add(new SimplePbrMaterial { Key = "48", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 0.4f, Roughness = 0.19607843f, Metallic = 0.8f, Albedo = new Vector3(0.7529412f, 0.60784316f, 0.22352941f) });
        materials.Add(new SimplePbrMaterial { Key = "49", Emission = new Vector3(0f, 0f, 0f), AmbientOcclusion = 1f, Roughness = 0f, Metallic = 0f, Albedo = new Vector3(0.039215688f, 0.23529412f, 0.039215688f) });
    }
    Skybox CreateSkybox()
    {
        var image = CreateImage();
        return new Skybox
        {
            IsVisible = false,
            Left = image,
            Right = image,
            Top = image,
            Bottom = image,
            Front = image,
            Rear = image
        };
    }
    Bitmap CreateImage()
    {
        const int size = 1000;
        var bitmap = new RenderTargetBitmap(new PixelSize(size, size), new Vector(96, 96));
        using var context = bitmap.CreateDrawingContext();
        var brush = new RadialGradientBrush
        {
            GradientStops =
            {
                new GradientStop(Colors.White, 0.0f),
                new GradientStop(Colors.Gray, 0.8f),
            }
        };
        context.FillRectangle(brush, new Rect(0, 0, size, size));
        return bitmap;
    }
}
