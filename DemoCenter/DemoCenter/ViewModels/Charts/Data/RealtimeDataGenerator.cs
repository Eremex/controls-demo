using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels.DataAdapters;

public class RealtimeDataGenerator
{
    readonly Random random = new(1);
    readonly object sync = new();
    readonly int interval;
    readonly int pointsCount;
    readonly List<(TimeSpan, double)>[] buffers;
    readonly SortedTimeSpanDataAdapter[] adapters;
    readonly double[] yAdditions;
    bool enabled;
    Thread generatingThread;
    
    int AdaptersCount => adapters.Length;
    public SortedTimeSpanDataAdapter[] Adapters => adapters;
        
    public RealtimeDataGenerator(int adaptersCount, int pointsCount, int interval)
    {
        this.interval = interval;
        this.pointsCount = pointsCount;
        adapters = new SortedTimeSpanDataAdapter[adaptersCount];
        buffers = new List<(TimeSpan, double)>[adaptersCount];
        yAdditions = new double[adaptersCount];
        for (int i = 0; i < AdaptersCount; i++)
        {
            adapters[i] = new SortedTimeSpanDataAdapter();
            buffers[i] = new List<(TimeSpan, double)>();
        }
    }
    (TimeSpan, double) CreatePoint(int index, TimeSpan timeStamp)
    {
        double arg = timeStamp.TotalMilliseconds;
        yAdditions[index] += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
        if (yAdditions[index] < -30)
            yAdditions[index] += 10;
        if (yAdditions[index] > 30)
            yAdditions[index] -= 10;
        double indication = 5 * Math.Sin((index + 1) * Math.Cos(arg)) + 100 * index + (random.NextDouble() - 0.5) * 5 + yAdditions[index];
        return (timeStamp, indication);
    }
    void GeneratingLoop()
    {
        var timeStamp = TimeSpan.FromMilliseconds(pointsCount * interval);
        var addition = TimeSpan.FromMilliseconds(interval);
        while (enabled)
        {
            Thread.Sleep((int)addition.TotalMilliseconds);
            timeStamp += addition;
            for (int i = 0; i < AdaptersCount; i++)
            {
                var point = CreatePoint(i, timeStamp);
                lock (sync)
                {
                    buffers[i].Add(point);
                }
            }
        }
    }
    public void GenerateInitialData()
    {
        for (int i = 0; i < pointsCount - 1; i++)
        {
            var argument = TimeSpan.FromMilliseconds(i * interval);
            for (int j = 0; j < AdaptersCount; j++)
                adapters[j].Add(CreatePoint(j, argument));
        }
    }
    public void Start()
    {
        generatingThread ??= new Thread(GeneratingLoop);
        enabled = true;
        generatingThread.Start();
    }
    public void Stop()
    {
        enabled = false;
        generatingThread?.Join();
        generatingThread = null;
    }
    public void UpdateAdapters()
    {
        lock (sync)
        {
            for (int i = 0; i < AdaptersCount; i++)
            {
                adapters[i].AddRange(buffers[i]);
                adapters[i].RemoveFromStart(buffers[i].Count);
                buffers[i].Clear();
            }
        }
    }
}
