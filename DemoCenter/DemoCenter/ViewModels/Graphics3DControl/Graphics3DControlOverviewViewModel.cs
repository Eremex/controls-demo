using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Reflection;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;
using JeremyAnsel.Media.WavefrontObj;
using Microsoft.Extensions.Logging;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlOverviewViewModel : Graphics3DControlViewModel
{
    static Vertex3D ToVertex3D(ObjVertex vertex, ObjVector3 normal) => new()
    {
        Position = new Vector3(vertex.Position.X, vertex.Position.Y, vertex.Position.Z),
        Normal = new Vector3(normal.X, normal.Y, normal.Z)
    };
    static GeometryModel3D LoadModel(string modelName, string resourceName)
    {
        var assembly = Assembly.GetAssembly(typeof(Graphics3DControlViewModel));
        var stream = assembly!.GetManifestResourceStream(resourceName);
        var obj = ObjFile.FromStream(stream);
        var vertices = new List<Vertex3D>();
        var indices = new List<uint>();
        uint index = 0;
        foreach (var face in obj.Faces)
        {
            foreach (var vertex in face.Vertices)
            {
                vertices.Add(ToVertex3D(obj.Vertices[vertex.Vertex - 1], obj.VertexNormals[vertex.Normal - 1]));
                indices.Add(index++);
            }
        }

        return new GeometryModel3D
        {
            Name = modelName,
            Meshes = { new MeshGeometry3D { Vertices = vertices.ToArray(), Indices = indices.ToArray() } }
        };
    }

    [ObservableProperty] ObservableCollection<GeometryModel3D> models = new();
    [ObservableProperty] CustomLogger logger;

    public Graphics3DControlOverviewViewModel()
    {
        var model = LoadModel("Teapot", "DemoCenter.Resources.Graphics3D.Models.Teapot.obj");
        model.TranslateToZero();
        models.Add(model);
        Logger = new CustomLogger();
    }
}

public class CustomLogger : ILogger, INotifyPropertyChanged
{
    readonly StringBuilder sb = new();
    
    public string Text => sb.ToString();
    
    public event PropertyChangedEventHandler PropertyChanged;

    public IDisposable BeginScope<TState>(TState state) => null;
    public bool IsEnabled(LogLevel logLevel) => true;
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (IsEnabled(logLevel))
        {
            var message = formatter(state, exception);
            sb.AppendLine($"[{logLevel}] {message}");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
        }
    }
}