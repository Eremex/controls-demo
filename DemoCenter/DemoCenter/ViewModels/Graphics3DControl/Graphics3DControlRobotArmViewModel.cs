using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using Assimp;
using Num = System.Numerics;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlRobotArmViewModel : Graphics3DControlViewModel
{
    List<Num.Vector3> offsets =
    [
        new(0, 0, 0), // Base
        new(0, 0, 5.15f), // Arm 1
        new(0, 0, 4.8f), // Arm 2
        new(0, 0, 4.6f), // Wrist
        new(-1.3f, 0, 1.7f), // Claw 1
        new(1.3f, 0, 1.7f) // Claw 2
    ];

    [ObservableProperty] float claws = 0;
    [ObservableProperty] float wrist = 0;
    [ObservableProperty] float arm2 = 0;
    [ObservableProperty] float arm1X = 0;
    [ObservableProperty] float arm1Y = 0;
    [ObservableProperty] float arm1Z = 0;
    [ObservableProperty] ObservableCollection<GeometryModel3D> models;

    public Graphics3DControlRobotArmViewModel()
    {
        models = LoadModels("DemoCenter.Resources.Graphics3D.Models.robot_arm_2.fbx");
        ApplyTransformations();
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(Claws) or nameof(Wrist) or nameof(Arm2) or nameof(Arm1X) or nameof(Arm1Y) or nameof(Arm1Z))
            ApplyTransformations();
        base.OnPropertyChanged(e);
    }
    void ApplyTransformations()
    {
        var transform = Num.Matrix4x4.CreateTranslation(0, 0, -11);
        for (int i = 0; i < Models.Count; i++)
        {
            var localTransform = GetTransform(i);
            Models[i].Transform = localTransform * Num.Matrix4x4.CreateTranslation(offsets[i]) * transform;
            if (i < 4)
                transform = Models[i].Transform;
        }
    }
    void ProcessNode(Node node, Scene scene, ObservableCollection<GeometryModel3D> result)
    {
        if (node.HasMeshes)
        {
            foreach (var index in node.MeshIndices)
                result.Add(GetModel(scene, node, index));
        }

        if (node.HasChildren)
        {
            foreach (var childNode in node.Children)
                ProcessNode(childNode, scene, result);
        }
    }
    Num.Matrix4x4 GetTransform(int index) => index switch
    {
        1 => Num.Matrix4x4.CreateFromYawPitchRoll(Arm1Y, Arm1X, Arm1Z), // Arm 1
        2 => Num.Matrix4x4.CreateRotationY(Arm2), // Arm 2: Y
        3 => Num.Matrix4x4.CreateRotationZ(Wrist), // Wrist: Z
        4 => Num.Matrix4x4.CreateRotationY(Claws), // Claw 1: +Y
        5 => Num.Matrix4x4.CreateRotationY(-Claws), // Claw 2: -Y
        _ => Num.Matrix4x4.Identity
    };
    ObservableCollection<GeometryModel3D> LoadModels(string path)
    {
        var result = new ObservableCollection<GeometryModel3D>();
        var assembly = Assembly.GetAssembly(typeof(Graphics3DControlRobotArmViewModel));
        var stream = assembly!.GetManifestResourceStream(path);
        using var context = new AssimpContext();
        var scene = context.ImportFileFromStream(stream);
        ProcessNode(scene.RootNode, scene, result);
        return result;
    }
    GeometryModel3D GetModel(Scene scene, Node node, int index)
    {
        var model = new GeometryModel3D { Name = node.Name };
        var mesh = scene.Meshes[index];
        var vertices = new Vertex3D[mesh.Vertices.Count];
        for (int i = 0; i < mesh.Vertices.Count; i++)
        {
            vertices[i] = new Vertex3D { Position = mesh.Vertices[i], Normal = mesh.Normals[i] };
        }
        model.Meshes.Add(new MeshGeometry3D
        {
            Hint = node.Name,
            Vertices = vertices,
            Indices = scene.Meshes[index].GetUnsignedIndices().ToArray(),
        });
        return model;
    }
}
