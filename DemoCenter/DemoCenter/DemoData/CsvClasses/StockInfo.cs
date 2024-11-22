namespace DemoCenter.DemoData;

public class StockInfo
{
    public DateTime Date { get; }
    public double Close { get; }
    public double Open { get; }
    public double High { get; }
    public double Low { get; }
    public double Volume { get; }

    public StockInfo(DateTime date, double close, double open, double high, double low, double volume)
    {
        Date = date;
        Close = close;
        Open = open;
        High = high;
        Low = low;
        Volume = volume;
    }
}
