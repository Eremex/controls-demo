using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Numerics;
using System.Reflection;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;
using JeremyAnsel.Media.WavefrontObj;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlTexturedMaterialsViewModel : Graphics3DControlViewModel
{
    static Vertex3D ToVertex3D(ObjVertex vertex, ObjVector3 texture, ObjVector3 normal) => new()
    {
        Position = new Vector3(vertex.Position.X, vertex.Position.Y, vertex.Position.Z),
        TextureCoord = new Vector2(texture.X, texture.Y),
        Normal = new Vector3(normal.X, normal.Y, normal.Z)
    };
    static TexturedPbrMaterial LoadMaterial(Assembly assembly, string resourceName)
    {
        var stream = assembly!.GetManifestResourceStream(resourceName);
        using var archive = new ZipArchive(stream!, ZipArchiveMode.Read);
        var material = new TexturedPbrMaterial { Key = resourceName.Split('.')[^2] };
        foreach (var entry in archive.Entries)
        {
            using var entryStream = entry.Open();
            using var memoryStream = new MemoryStream();
            entryStream.CopyTo(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var bitmap = new Bitmap(memoryStream);
            if (entry.Name.StartsWith("Albedo"))
                material.Albedo = bitmap;
            else if (entry.Name.StartsWith("AO"))
                material.AmbientOcclusion = bitmap;
            else if (entry.Name.StartsWith("Metallic"))
                material.Metallic = bitmap;
            else if (entry.Name.StartsWith("Roughness"))
                material.Roughness = bitmap;
            else if (entry.Name.StartsWith("Normal"))
                material.Normal = bitmap;
            else if (entry.Name.StartsWith("Emissive"))
                material.Emission = bitmap;
        }
        return material;
    }

    [ObservableProperty] ObservableCollection<TexturedPbrMaterial> materials = new();
    [ObservableProperty] TexturedPbrMaterial selectedMaterial;
    [ObservableProperty] Vertex3D[] vertices;
    [ObservableProperty] uint[] indices;

    public Graphics3DControlTexturedMaterialsViewModel()
    {
        var assembly = Assembly.GetAssembly(typeof(Graphics3DControlViewModel));
        var textureNames = assembly!.GetManifestResourceNames().Where(name => name.StartsWith("DemoCenter.Resources.Graphics3D.Materials."));
        foreach (var textureName in textureNames)
            materials.Add(LoadMaterial(assembly, textureName));
        selectedMaterial = Materials.First();
        
        var stream = assembly!.GetManifestResourceStream("DemoCenter.Resources.Graphics3D.Models.Sphere.obj");
        var obj = ObjFile.FromStream(stream);
        var verticesList = new List<Vertex3D>();
        var indicesList = new List<uint>();
        uint index = 0;
        foreach (var face in obj.Faces)
        {
            foreach (var vertex in face.Vertices)
            {
                verticesList.Add(ToVertex3D(obj.Vertices[vertex.Vertex - 1], obj.TextureVertices[vertex.Texture - 1], obj.VertexNormals[vertex.Normal - 1]));
                indicesList.Add(index++);
            }
        }
        vertices = verticesList.ToArray();
        indices = indicesList.ToArray();
    }
}
