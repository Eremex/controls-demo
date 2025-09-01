using System.Collections.ObjectModel;
using System.Numerics;
using Avalonia.Media;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlTransformationViewModel : Graphics3DControlViewModel
{
    readonly DispatcherTimer timer = new(DispatcherPriority.Background);
    readonly Vector3[] offsets = new Vector3[] { new(0, -1, 1), new(0, -1, -1.9f), new(0, 1.8f, 1.9f) };
    readonly float[] rotationSpeed = new float[] { 4000, -2000, -2000 };
    readonly DateTime start = DateTime.Now;

    [ObservableProperty] ObservableCollection<SimplePbrMaterial> materials = new();
    [ObservableProperty] ObservableCollection<GeometryModel3D> models = new();

    public Graphics3DControlTransformationViewModel()
    {
        Materials.Add(new SimplePbrMaterial(Colors.Red, "Gear1"));
        Materials.Add(new SimplePbrMaterial(Colors.Green, "Gear2"));
        Materials.Add(new SimplePbrMaterial(Colors.Blue, "Gear3"));
        Models.Add(Model3DLoader.LoadModel("Gear 1", "DemoCenter.Resources.Graphics3D.Models.Gear_1.obj", "Gear1"));
        Models.Add(Model3DLoader.LoadModel("Gear 2", "DemoCenter.Resources.Graphics3D.Models.Gear_2.obj", "Gear2"));
        Models.Add(Model3DLoader.LoadModel("Gear 3", "DemoCenter.Resources.Graphics3D.Models.Gear_3.obj", "Gear3"));
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
