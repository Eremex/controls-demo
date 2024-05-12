using System.Globalization;

namespace DemoCenter.DemoData;

public abstract class CsvColumn
{
    public string Header { get; }

    public CsvColumn(string header)
    {
        Header = header;
    }
    public abstract void AddValue(string value);
}

public abstract class CsvColumn<T> : CsvColumn
{
    public List<T> Data { get; } = new();

    protected CsvColumn(string header) : base(header)
    {
    }
}

public class CsvDoubleColumn : CsvColumn<double>
{
    public CsvDoubleColumn(string header) : base(header)
    {
    }
    public override void AddValue(string value) => Data.Add(double.Parse(value.Trim(), CultureInfo.InvariantCulture));
}
