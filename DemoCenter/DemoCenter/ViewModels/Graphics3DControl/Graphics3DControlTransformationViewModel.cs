using System.Collections.ObjectModel;
using System.Numerics;
using System.Reflection;
using Avalonia.Media;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;
using JeremyAnsel.Media.WavefrontObj;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlTransformationViewModel : Graphics3DControlViewModel
{
    static Vertex3D ToVertex3D(ObjVertex vertex, ObjVector3 texture, ObjVector3 normal) => new()
    {
        Position = new Vector3(vertex.Position.X, vertex.Position.Y, vertex.Position.Z),
        TextureCoord = new Vector2(texture.X, texture.Y),
        Normal = new Vector3(normal.X, normal.Y, normal.Z)
    };
    static GeometryModel3D LoadModel(Assembly assembly, string resourceName, string name, string materialKey)
    {
        var stream = assembly.GetManifestResourceStream(resourceName);
        var obj = ObjFile.FromStream(stream);
        var vertices = new List<Vertex3D>();
        var indices = new List<uint>();
        uint index = 0;
        foreach (var face in obj.Faces)
        {
            foreach (var vertex in face.Vertices)
            {
                vertices.Add(ToVertex3D(obj.Vertices[vertex.Vertex - 1], obj.TextureVertices[vertex.Texture - 1], obj.VertexNormals[vertex.Normal - 1]));
                indices.Add(index++);
            }
        }
        var model = new GeometryModel3D { Name = name };
        model.Meshes.Add(new MeshGeometry3D { MaterialKey = materialKey,Vertices = vertices.ToArray(), Indices = indices.ToArray() });
        return model;
    }

    readonly DispatcherTimer timer = new(DispatcherPriority.Background);
    readonly Vector3[] offsets = new Vector3[] { new(0, -1, 1), new(0, -1, -1.9f), new(0, 1.8f, 1.9f) };
    readonly float[] rotationSpeed = new float[] { 4000, -2000, -2000 };
    readonly DateTime start = DateTime.Now;

    [ObservableProperty] ObservableCollection<SimplePbrMaterial> materials = new();
    [ObservableProperty] ObservableCollection<GeometryModel3D> models = new();

    public Graphics3DControlTransformationViewModel()
    {
        var assembly = Assembly.GetAssembly(typeof(Graphics3DControlViewModel))!;
        Materials.Add(new SimplePbrMaterial(Colors.Red, "Gear1"));
        Materials.Add(new SimplePbrMaterial(Colors.Green, "Gear2"));
        Materials.Add(new SimplePbrMaterial(Colors.Blue, "Gear3"));
        Models.Add(LoadModel(assembly, "DemoCenter.Resources.Graphics3D.Models.Gear_1.obj", "Gear 1", "Gear1"));
        Models.Add(LoadModel(assembly, "DemoCenter.Resources.Graphics3D.Models.Gear_2.obj", "Gear 2", "Gear2"));
        Models.Add(LoadModel(assembly, "DemoCenter.Resources.Graphics3D.Models.Gear_3.obj", "Gear 3", "Gear3"));
        UpdateTransformations();
        timer.Tick += TimerOnTick; 
        timer.Interval = TimeSpan.FromMilliseconds(5);
    }
    void TimerOnTick(object sender, EventArgs e) => UpdateTransformations();
    void UpdateTransformations()
    {
        float milliseconds = (float)Math.Floor((DateTime.Now - start).TotalMilliseconds);
        for (int i = 0; i < 3; i++)
        {
            float factor = (milliseconds % rotationSpeed[i]) / rotationSpeed[i];
            var transform = Matrix4x4.CreateFromYawPitchRoll(0, MathF.PI * 2 * factor, MathF.PI * 0.5f);
            transform.M41 = offsets[i].X;
            transform.M42 = offsets[i].Y;
            transform.M43 = offsets[i].Z;
            Models[i].Transform = transform;
        }
    }
    public void Start() => timer.Start();
    public void Stop() => timer.Stop();
}
