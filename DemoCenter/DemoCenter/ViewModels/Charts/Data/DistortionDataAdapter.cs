using Eremex.Avalonia.Charts;

namespace DemoCenter.ViewModels.DataAdapters;

public class DistortionDataAdapter : ISeriesDataAdapter
{
	const int StartFreq = 10;
	const int EndFreq = 10000;

	double[] arguments;
	double[] values;

	public DistortionDataAdapter(int seed)
	{
		arguments = new double[ItemCount];
		values = new double[ItemCount];
		var random = new Random(seed);

		int offset = random.Next(10);
		double step1 = 30 + offset;
		double step2 = 300 + offset * 10;
		for (int i = 0; i < ItemCount; i++)
		{
			double arg = i + StartFreq;
			arguments[i] = arg;
			double baseValue = i < step1 ? 20 - 0.3 * i : i < step2 ? 11 * (1d - i / step2) : 0.03;
			double addition = i < step1 ? 2 * (step1 / i) : i < step2 ? 0.5 * (step2 / i) : 0.05 * (EndFreq / arg);
			values[i] = baseValue * (1 + random.NextDouble() * addition);
		}
	}
	#region ISeriesDataAdapter
	
	public bool IsDataSorted => true;
	public int ItemCount => EndFreq - StartFreq;

	event SeriesDataAdapterDataChangedEventHandler ISeriesDataAdapter.DataChanged
	{
		add { }
		remove { }
	}

	Dictionary<SeriesDataMemberType, ScaleType> ISeriesDataAdapter.GetScaleTypes() => new()
	{
		{ SeriesDataMemberType.Argument, ScaleType.Numeric },
		{ SeriesDataMemberType.Value, ScaleType.Numeric },
	};
	double ISeriesDataAdapter.GetNumericalValue(int index, SeriesDataMemberType dataMember) => dataMember switch
	{
		SeriesDataMemberType.Argument => arguments[index],
		SeriesDataMemberType.Value => values[index],
		_ => throw new ArgumentOutOfRangeException(nameof(dataMember), dataMember, null)
	};
	DateTime ISeriesDataAdapter.GetDateTimeValue(int index, SeriesDataMemberType dataMember) => throw new NotImplementedException();
	TimeSpan ISeriesDataAdapter.GetTimeSpanValue(int index, SeriesDataMemberType dataMember) => throw new NotImplementedException();
	string ISeriesDataAdapter.GetQualitativeValue(int index, SeriesDataMemberType dataMember) => throw new NotImplementedException();
	
	#endregion
}
