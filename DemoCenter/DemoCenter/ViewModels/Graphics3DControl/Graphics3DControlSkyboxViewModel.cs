using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Numerics;
using System.Reflection;
using Assimp;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlSkyboxViewModel : Graphics3DControlViewModel
{
    static Skybox LoadSkybox(Assembly assembly, string resourceName)
    {
        var stream = assembly!.GetManifestResourceStream(resourceName);
        using var archive = new ZipArchive(stream!, ZipArchiveMode.Read);
        var skybox = new Skybox();
        foreach (var entry in archive.Entries)
        {
            if (!entry.Name.EndsWith(".jpg"))
                continue;
            using var entryStream = entry.Open();
            using var memoryStream = new MemoryStream();
            entryStream.CopyTo(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var bitmap = new Bitmap(memoryStream);
            if (entry.Name.StartsWith("negx"))
                skybox.Left = bitmap;
            else if (entry.Name.StartsWith("negy"))
                skybox.Bottom = bitmap;
            else if (entry.Name.StartsWith("negz"))
                skybox.Rear = bitmap;
            else if (entry.Name.StartsWith("posx"))
                skybox.Right = bitmap;
            else if (entry.Name.StartsWith("posy"))
                skybox.Top = bitmap;
            else if (entry.Name.StartsWith("posz"))
                skybox.Front = bitmap;
        }
        return skybox;
    }
    
    [ObservableProperty] string materialKey;
    [ObservableProperty] uint[] indices;
    [ObservableProperty] Vertex3D[] vertices;
    [ObservableProperty] ObservableCollection<TexturedPbrMaterial> materials = new();
    [ObservableProperty] ObservableCollection<Skybox> skyboxes = new();
    [ObservableProperty] Skybox selectedSkybox = new();

    public Graphics3DControlSkyboxViewModel()
    {
        var assembly = Assembly.GetAssembly(typeof(Graphics3DControlViewModel));
        var material = Graphics3DControlTexturedMaterialsViewModel.LoadMaterial(assembly, "DemoCenter.Resources.Graphics3D.Materials.AlienPanels.zip");
        materialKey = material.Key;
        materials.Add(material);
        
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
        
        var skyboxNames = assembly!.GetManifestResourceNames().Where(name => name.StartsWith("DemoCenter.Resources.Graphics3D.Skyboxes."));
        foreach (var skyboxName in skyboxNames)
            skyboxes.Add(LoadSkybox(assembly, skyboxName));
        selectedSkybox = Skyboxes.First();
    }
}