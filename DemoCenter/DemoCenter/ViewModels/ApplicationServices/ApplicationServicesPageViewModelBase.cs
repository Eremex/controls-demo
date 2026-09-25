using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;

using Eremex.AvaloniaUI.Controls.ApplicationServices;

namespace DemoCenter.ViewModels.ApplicationServices;

/// <summary>
/// Base class for the Application Services demo pages.
/// </summary>
/// <remarks>
/// Services are resolved once from the ambient context that App sets up at startup.
/// In a real application the services are injected into view model constructors instead —
/// the ambient context exists for places where injection cannot reach.
/// </remarks>
public abstract partial class ApplicationServicesPageViewModelBase : PageViewModelBase
{
    /// <summary>
    /// What each button did, newest entry first.
    /// </summary>
    public ObservableCollection<DemoLogEntry> Log { get; } = new();

    /// <summary>
    /// Removes every log entry.
    /// </summary>
    [RelayCommand]
    protected void ClearLog() => Log.Clear();

    /// <summary>
    /// Adds a log entry describing what a scenario returned.
    /// </summary>
    protected void Report(string scenario, string result) => Log.Insert(0, new DemoLogEntry(scenario, result));

    protected static T Service<T>()
        where T : class
        => ApplicationServicesContext.GetRequiredService<T>();
}

/// <summary>
/// A single line in the demo result log.
/// </summary>
public sealed class DemoLogEntry
{
    public DemoLogEntry(string scenario, string result)
    {
        Scenario = scenario;
        Result = result;
        Time = DateTime.Now.ToString("HH:mm:ss");
    }

    public string Scenario { get; }

    public string Result { get; }

    public string Time { get; }
}
