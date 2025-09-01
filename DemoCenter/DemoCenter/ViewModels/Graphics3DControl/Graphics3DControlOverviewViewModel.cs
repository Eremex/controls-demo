using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;
using Microsoft.Extensions.Logging;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlOverviewViewModel : Graphics3DControlViewModel
{
    [ObservableProperty] ObservableCollection<GeometryModel3D> models = new();
    [ObservableProperty] CustomLogger logger;

    public Graphics3DControlOverviewViewModel()
    {
        var model = Model3DLoader.LoadModel("Teapot", "DemoCenter.Resources.Graphics3D.Models.Teapot.obj");
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