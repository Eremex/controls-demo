using System.Collections.ObjectModel;
using System.Numerics;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Common;
using Eremex.AvaloniaUI.Controls3D;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlLightsViewModel : Graphics3DControlViewModel
{
    const float LightDistance = 40;
    const float LightHeight = 0;
    const float PlaneSize = 80;
    const float PlaneZ = -20;

    static float Normalize(byte value) => (float)value / byte.MaxValue;
    static Vector3 ToVector3(Color color) => new(Normalize(color.R), Normalize(color.G), Normalize(color.B));

    [ObservableProperty] ObservableCollection<GeometryModel3D> models = new();
    [ObservableProperty] ObservableCollection<Material> materials = new();
    [ObservableProperty] ObservableCollection<LightViewModel> lights = new();

    public Graphics3DControlLightsViewModel()
    {
        AddLight(Colors.White, new Vector3(LightDistance, LightHeight, 0), "White");
        AddLight(Colors.Red, new Vector3(-LightDistance, LightHeight, 0), "Red");
        AddLight(Colors.Lime, new Vector3(0, LightHeight, -LightDistance), "Green");
        AddLight(Colors.Blue, new Vector3(0, LightHeight, LightDistance), "Blue");
        AddSphere(15);
        AddPlane();
    }
    void AddSphere(float scale)
    {
        var model = Model3DLoader.LoadModel(string.Empty, "DemoCenter.Resources.Graphics3D.Models.Sphere.obj");
        model.Transform = Matrix4x4.CreateScale(scale);
        Models.Add(model);
    }
    void AddLight(Color color, Vector3 position, string materialKey)
    {
        var model = Model3DLoader.LoadModel(string.Empty, "DemoCenter.Resources.Graphics3D.Models.Sphere.obj");
        model.Transform = CreateTransform(LightViewModel.DefaultRadius);
        model.Meshes.Single().MaterialKey = materialKey;
        model.Meshes.Single().Hint = materialKey;
        var lightMaterial = new SimplePbrMaterial
        {
            Key = materialKey,
            Albedo = LightViewModel.DefaultColorIntensity * ToVector3(color),
            AmbientOcclusion = 0,
            Roughness = 0,
            Metallic = 0,
            Emission = LightViewModel.DefaultColorIntensity * ToVector3(color)
        }; 
        Materials.Add(lightMaterial);
        Models.Add(model);
        var light = new LightViewModel(position, color, materialKey);
        light.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(LightViewModel.Radius))
                model.Transform = CreateTransform(((LightViewModel)s)!.Radius);
            else if (e.PropertyName == nameof(LightViewModel.ColorIntensity))
            {
                var colorVector = ToVector3(light.Color) * light.ColorIntensity;
                lightMaterial.Albedo = colorVector;
                lightMaterial.Emission = colorVector;
            }
        };
        Lights.Add(light);
        
        Matrix4x4 CreateTransform(float radius) => Matrix4x4.CreateScale(radius / model.BoundingBox!.Value.Size.X) * Matrix4x4.CreateTranslation(position);
    }
    void AddPlane()
    {
        const string materialKey = "Plane";

        Materials.Add(new SimplePbrMaterial { Key = materialKey, Albedo = Vector3.One, AmbientOcclusion = 0 });
        var mesh = new MeshGeometry3D();
        mesh.Vertices = new[]
        {
            new Vertex3D(new Vector3(-PlaneSize, PlaneZ, -PlaneSize), Vector3.UnitY),
            new Vertex3D(new Vector3(-PlaneSize, PlaneZ, PlaneSize), Vector3.UnitY),
            new Vertex3D(new Vector3(PlaneSize, PlaneZ, PlaneSize), Vector3.UnitY),
            new Vertex3D(new Vector3(PlaneSize, PlaneZ, -PlaneSize), Vector3.UnitY),
        };
        mesh.Indices = new uint[] { 0, 1, 2, 0, 2, 3 };
        mesh.MaterialKey = materialKey;
        Models.Add(new GeometryModel3D { Meshes = { mesh } });
    }
}

public partial class LightViewModel : ViewModelBase
{
    public const float DefaultRadius = 10;
    public const float DefaultColorIntensity = 1;
    
    [ObservableProperty] Vector3 position;
    [ObservableProperty] Color color;
    [ObservableProperty] float radius = DefaultRadius;
    [ObservableProperty] float colorIntensity = DefaultColorIntensity;
    public string Name { get; }

    public LightViewModel(Vector3 position, Color color, string name)
    {
        Position = position;
        Color = color;
        Name = name;
    }
}
