using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class HeatmapRealTimeViewModel : ChartsPageViewModel
{
    const int BandSize = 1000;
    const int TimeSize = 500;
    const int StartFrequency = 2000;
    const int TimeInterval = 20;
    const double Amplitude = -50;
    
    static double Interpolate(double y1, double y2, double mu)
    {
        double mu2 = (1 - Math.Cos(mu * Math.PI)) * 0.5;
        return y1 * (1 - mu2) + y2 * mu2;
    }
    static string ToYLabel(DateTime dateTime) => dateTime.ToString("HH:mm:ss.ffff");

    readonly DispatcherTimer timer = new(DispatcherPriority.Background);
    readonly double[,] values = new double[TimeSize, BandSize];
    readonly double[] signalValues = new double[BandSize];
    readonly double[] arguments = new double[BandSize];
    readonly double[] bands = new double[] { 0, 0.3, 0, 0, 0.2, 0, 0, 0.9, 0, 0, 0.3, 0, 0.01, 0, 0 };
    readonly Random random = new(0);
    List<string> yArguments = new(TimeSize);

    [ObservableProperty] HeatmapDataAdapter waterfallAdapter;
    [ObservableProperty] SortedNumericDataAdapter signalAdapter;

    public HeatmapRealTimeViewModel()
    {
        var argumentsX = new string[BandSize];
        for (int i = 0; i < argumentsX.Length; i++)
        {
            arguments[i] = StartFrequency + i;
            argumentsX[i] = $"{((StartFrequency + i) * 0.001):#.###}k";
        }
        for (int i = 0; i < TimeSize; i++)
        {
            for (int j = 0; j < BandSize; j++)
                values[i, j] = Amplitude;
        }
        var now = DateTime.Now;
        for (int i = 0; i < TimeSize; i++)
            yArguments.Add(ToYLabel(now.AddMilliseconds((i - TimeSize) * TimeInterval)));
        GenerateData();
        waterfallAdapter = new HeatmapDataAdapter(argumentsX, yArguments, values);
        signalAdapter = new SortedNumericDataAdapter(arguments, signalValues);

        timer.Tick += UpdateAdapter;
        timer.Interval = TimeSpan.FromMilliseconds(TimeInterval);
    }
    void UpdateAdapter(object sender, EventArgs e)
    {
        GenerateData();
        WaterfallAdapter.UpdateValues(values);
        yArguments.Add(ToYLabel(DateTime.Now));
        yArguments.RemoveAt(0);
        WaterfallAdapter.UpdateYArguments(yArguments);
        SignalAdapter.Clear();
        for (int i = 0; i < BandSize; i++)
            SignalAdapter.Add(arguments[i], signalValues[i]);
    }
    void GenerateData()
    {
        for (int i = 0; i < bands.Length; i++)
            bands[i] = Math.Max(0, Math.Min(1, bands[i] + (random.NextDouble() - 0.5) * 0.05));
        Array.Copy(values, BandSize, values, 0, values.Length - BandSize);
        for (int i = 0; i < signalValues.Length; i++)
        {
            double relativePosition = (double)i * (bands.Length - 1) / signalValues.Length;
            double value = Interpolate(bands[(int)relativePosition], bands[(int)relativePosition + 1], relativePosition - Math.Floor(relativePosition));
            value = Math.Round((1 - Math.Min(1, Math.Max(0, value + (random.NextDouble() - 0.5) * 0.05))) * Amplitude);
            signalValues[i] = value;
            values[TimeSize - 1, i] = value;
        }
    }
    public void Start() => timer.Start();
    public void Stop() => timer.Stop();
}
