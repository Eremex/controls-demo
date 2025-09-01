using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Numerics;
using System.Reflection;
using Assimp;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlTexturedMaterialsViewModel : Graphics3DControlViewModel
{
    public static TexturedPbrMaterial LoadMaterial(Assembly assembly, string resourceName)
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
        using var context = new AssimpContext();
        var scene = context.ImportFileFromStream(stream);

        var mesh = scene.Meshes.Single();
        vertices = new Vertex3D[mesh.VertexCount];
        for (int i = 0; i < mesh.VertexCount; ++i)
            vertices[i] = new Vertex3D
            {
                Normal = mesh.Normals[i], 
                Position = mesh.Vertices[i],
                TextureCoord = new Vector2(mesh.TextureCoordinateChannels[0][i].X, mesh.TextureCoordinateChannels[0][i].Y)
            };
        indices = mesh.GetUnsignedIndices().ToArray();
    }
}
