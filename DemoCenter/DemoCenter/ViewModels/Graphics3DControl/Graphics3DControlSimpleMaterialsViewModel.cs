#nullable enable
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlSimpleMaterialsViewModel : Graphics3DControlViewModel
{
    [ObservableProperty] ObservableCollection<GeometryModel3D> models = new();
    [ObservableProperty] Skybox? skybox = null;

    public Graphics3DControlSimpleMaterialsViewModel()
    {
        var model = Model3DLoader.LoadModel(string.Empty, "DemoCenter.Resources.Graphics3D.Models.Sphere.obj");
        models.Add(model);
    }
}
