using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels.DataAdapters;

public class ClusterDataAdapter : ISeriesDataAdapter
{
    readonly int count;
    readonly List<double> arguments;
    readonly List<double> values;

    public int ItemCount => count;
    public bool IsDataSorted => false;
    public event SeriesDataAdapterDataChangedEventHandler DataChanged;

    public ClusterDataAdapter(int xMinus, int xPlus, int yMinus, int yPlus, int count)
    {
        this.count = count;
        arguments = new List<double>(count);
        values = new List<double>(count);
        
        var random = new Random(0);
        int deltaX = xMinus - xPlus;
        int deltaY = yMinus - yPlus;
        double centerX = 0.5 * xMinus + 0.5 * xPlus;
        double centerY = 0.5 * yMinus + 0.5 * yPlus;
        for (int i = 0; i < count; i++) {
            double half = 0.5 * i + 1;
            double ratio = Math.Max(2.1, count / half);
            int xOffset = (int)(deltaX / ratio);
            int yOffset = (int)(deltaY / ratio);
            double delta = xMinus - xOffset - centerX;
            int rx, ry;
            do {
                rx = random.Next(xPlus + xOffset, xMinus - xOffset);
                ry = random.Next(yPlus + yOffset, yMinus - yOffset);
            }
            while (delta * delta < Math.Pow(centerX - rx, 2) + Math.Pow(centerY - ry, 2));
            arguments.Add(rx);
            values.Add(ry);
        };
    }
    public Dictionary<AxisType, ScaleType> GetScaleTypes() => new() { { AxisType.Argument, ScaleType.Numeric }, { AxisType.Value, ScaleType.Numeric } };
    protected virtual void OnDataChanged(ISeriesDataAdapter adapter, SeriesDataAdapterDataChangedEventArgs e) => DataChanged?.Invoke(adapter, e);
    public double GetNumericalValue(int index, SeriesDataMemberType dataMember) => dataMember switch
    {
        SeriesDataMemberType.Argument => arguments[index],
        SeriesDataMemberType.Value => values[index],
        _ => throw new ArgumentOutOfRangeException(nameof(dataMember), dataMember, null)
    };
    public DateTime GetDateTimeValue(int index, SeriesDataMemberType dataMember) => throw new NotImplementedException();
    public TimeSpan GetTimeSpanValue(int index, SeriesDataMemberType dataMember) => throw new NotImplementedException();
    public string GetQualitativeValue(int index, SeriesDataMemberType dataMember) => throw new NotImplementedException();

    public string GetUnderlyingData(int index) => null;
}