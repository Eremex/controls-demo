using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels.DataAdapters;

public class ScatterFormulaDataAdapter: ISeriesDataAdapter
{
	readonly Func<int, double> argumentFormula;
	readonly Func<int, double> valueFormula;
	int itemCount;
	SeriesDataAdapterDataChangedEventHandler dataChanged;

	public int ItemCount
	{
		get => itemCount;
		set
		{
			if (itemCount != value)
			{
				itemCount = value;
				dataChanged?.Invoke(this, new SeriesDataAdapterDataChangedEventArgs(SeriesDataUpdateType.Reset, null));
			}
		}
	}

	public ScatterFormulaDataAdapter(int itemCount, Func<int, double> argumentFormula, Func<int, double> valueFormula)
	{
		this.itemCount = itemCount;
		this.argumentFormula = argumentFormula;
		this.valueFormula = valueFormula;
	}
	
	#region ISeriesDataAdapter
	
	bool ISeriesDataAdapter.IsDataSorted => false;

	event SeriesDataAdapterDataChangedEventHandler ISeriesDataAdapter.DataChanged
	{
		add => dataChanged += value;
		remove => dataChanged -= value;
	}

	Dictionary<AxisType, ScaleType> ISeriesDataAdapter.GetScaleTypes() => new()
	{
		{ AxisType.Argument, ScaleType.Numeric },
		{ AxisType.Value, ScaleType.Numeric },
	};
	double ISeriesDataAdapter.GetNumericalValue(int index, SeriesDataMemberType dataMember) => dataMember switch
	{
		SeriesDataMemberType.Argument => argumentFormula(index),
		SeriesDataMemberType.Value => valueFormula(index),
		_ => throw new ArgumentOutOfRangeException(nameof(dataMember), dataMember, null)
	};
	DateTime ISeriesDataAdapter.GetDateTimeValue(int index, SeriesDataMemberType dataMember) => throw new NotImplementedException();
	TimeSpan ISeriesDataAdapter.GetTimeSpanValue(int index, SeriesDataMemberType dataMember) => throw new NotImplementedException();
	string ISeriesDataAdapter.GetQualitativeValue(int index, SeriesDataMemberType dataMember) => throw new NotImplementedException();
	
	#endregion
}
