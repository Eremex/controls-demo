using System.Reflection;
using Assimp;
using Eremex.AvaloniaUI.Controls3D;

namespace DemoCenter.ViewModels;

public static class Model3DLoader
{
    public static GeometryModel3D LoadModel(string modelName, string resourceName, string materialKey = null)
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
            var indices = mesh.GetUnsignedIndices().ToArray();
            var meshGeometry = new MeshGeometry3D { Vertices = vertices, Indices = indices };
            if (!string.IsNullOrEmpty(materialKey))
                meshGeometry.MaterialKey = materialKey;
            model.Meshes.Add(meshGeometry);
        }

        return model;
    }
}