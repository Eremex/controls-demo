using System.Numerics;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlPointsViewModel : Graphics3DControlViewModel
{
    const int Count = 1_000_000;
    const int Radius = 10_000;
        
    [ObservableProperty] Vertex3D[] vertices = new Vertex3D[Count];
    [ObservableProperty] uint[] indices = new uint[Count];
    [ObservableProperty] float pointSize = 1;
    [ObservableProperty] Color color = Color.FromArgb(255, 0, 120, 122);
    public Vector3 Albedo => Vector3.Zero;

    public Graphics3DControlPointsViewModel()
    {

        var random = new Random(0);
        for (uint i = 0; i < Count; i++) {
            float half = 0.5f * i + 1;
            float ratio = MathF.Max(2.1f, Count / half);
            int offset = (int)(2 * Radius / ratio);
            int rx = random.Next(-Radius -offset, Radius + offset);
            int ry = random.Next(-Radius -offset, Radius + offset);
            int rz = random.Next(-Radius -offset, Radius + offset);
            vertices[i] = new Vertex3D { Position = new Vector3(rx, ry, rz) };
            indices[i] = i;
        }
    }
}
