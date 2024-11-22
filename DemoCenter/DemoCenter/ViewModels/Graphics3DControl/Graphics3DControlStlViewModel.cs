using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Numerics;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;
using STLDotNet6.Formats.StereoLithography;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlStlViewModel : Graphics3DControlViewModel
{
    const int MaterialsCount = 20;
    
    static Vertex3D ToVertex3D(Vertex vertex, Normal normal) => new()
    {
        Position = new Vector3(vertex.X, vertex.Y, vertex.Z),
        Normal = new Vector3(normal.X, normal.Y, normal.Z)
    };
    static GeometryModel3D LoadModel(string modelName, string resourceName)
    {
        var assembly = Assembly.GetAssembly(typeof(Graphics3DControlViewModel));
        var stream = assembly!.GetManifestResourceStream(resourceName);
        using var archive = new ZipArchive(stream!, ZipArchiveMode.Read);
        using var stlStream = archive.Entries.Single().Open();
        using var reader = new StreamReader(stlStream);

        var model = new GeometryModel3D { Name = modelName };
        STLDocument document;
        int solidIndex = 0;
        while (!reader.EndOfStream && (document = STLDocument.Read(reader)) != null)
        {
            var vertices = new List<Vertex3D>();
            var indices = new List<uint>();
            uint index = 0;
            foreach (var facet in document!.Facets)
            {
                for (int i = 0; i < 3; i++)
                {
                    vertices.Add(ToVertex3D(facet.Vertices[i], facet.Normal));
                    indices.Add(index++);
                }
            }

            model.Meshes.Add(new MeshGeometry3D
            {
                Name = string.IsNullOrEmpty(document.Name) ? $"Solid {solidIndex++}" : document.Name,
                Vertices = vertices.ToArray(), Indices = indices.ToArray(),
                MaterialKey = $"{model.Meshes.Count % MaterialsCount}"
            });
        }
        return model;
    }

    [ObservableProperty] ObservableCollection<GeometryModel3D> models = new();
    [ObservableProperty] ObservableCollection<Material> materials = new();

    public Graphics3DControlStlViewModel()
    {
        var model = LoadModel("ddBox", "DemoCenter.Resources.Graphics3D.Models.ddBox-C1.zip");
        models.Add(model);
        var random = new Random(3);
        for (int i = 0; i < MaterialsCount; i++)
            materials.Add(new SimplePbrMaterial { Albedo = new Vector3(random.NextSingle(), random.NextSingle(), random.NextSingle()), Key = $"{i}" });
    }
}
