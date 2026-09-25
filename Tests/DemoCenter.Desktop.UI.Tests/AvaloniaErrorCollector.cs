using Avalonia.Logging;

namespace DemoCenter.Desktop.UI.Tests;

/// <summary>
/// Collects what Avalonia writes to the log at Error level while something is exercised.
/// </summary>
/// <remarks>
/// Needed because a failed binding or a template that did not build throws nothing - the page
/// simply ends up empty. Warning level is deliberately left out: routine messages land there too,
/// and every test would be permanently red.
/// </remarks>
public sealed class AvaloniaErrorCollector : ILogSink, IDisposable
{
    private readonly ILogSink previous = Logger.Sink;

    public AvaloniaErrorCollector() => Logger.Sink = this;

    public List<string> Messages { get; } = new();

    /// <summary>Fails the test if anything was logged, naming what was being exercised.</summary>
    public void AssertQuiet(string what) =>
        Assert.True(
            Messages.Count == 0,
            what + ": Avalonia logged errors:" + Environment.NewLine + string.Join(Environment.NewLine, Messages));

    public bool IsEnabled(LogEventLevel level, string area) => level >= LogEventLevel.Error;

    public void Log(LogEventLevel level, string area, object source, string messageTemplate)
    {
        if (level >= LogEventLevel.Error)
            Messages.Add($"[{area}] {messageTemplate}");
    }

    public void Log(LogEventLevel level, string area, object source, string messageTemplate, params object[] propertyValues)
    {
        if (level >= LogEventLevel.Error)
            Messages.Add($"[{area}] {messageTemplate} {string.Join(", ", propertyValues)}");
    }

    public void Dispose() => Logger.Sink = previous;
}
