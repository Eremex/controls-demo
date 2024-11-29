using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;

namespace DemoCenter.ViewModels;

public partial class MeshViewModel : ObservableObject
{
    [ObservableProperty] string name;
    [ObservableProperty] string materialKey;
    [ObservableProperty] Vertex3D[] vertices;
    [ObservableProperty] uint[] indices;
    [ObservableProperty] MeshFillType type;
    [ObservableProperty] uint size;

    public MeshViewModel(Vertex3D[] vertices, uint[] indices)
    {
        this.vertices = vertices;
        this.indices = indices;
    }
}
