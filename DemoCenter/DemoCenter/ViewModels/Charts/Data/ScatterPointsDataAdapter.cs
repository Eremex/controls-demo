using Avalonia;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels.DataAdapters;

public class ScatterPointsDataAdapter: ISeriesDataAdapter
{
	readonly List<Point> points; 
	SeriesDataAdapterDataChangedEventHandler dataChanged;

	public List<Point> Points => points; 

	public ScatterPointsDataAdapter(List<Point> points)
	{
		this.points = points;
	}
	
	#region ISeriesDataAdapter
	
	bool ISeriesDataAdapter.IsDataSorted => false;
	int ISeriesDataAdapter.ItemCount => Points.Count;

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
		SeriesDataMemberType.Argument => Points[index].X,
		SeriesDataMemberType.Value => Points[index].Y,
		_ => throw new ArgumentOutOfRangeException(nameof(dataMember), dataMember, null)
	};
	DateTime ISeriesDataAdapter.GetDateTimeValue(int index, SeriesDataMemberType dataMember) => throw new NotImplementedException();
	TimeSpan ISeriesDataAdapter.GetTimeSpanValue(int index, SeriesDataMemberType dataMember) => throw new NotImplementedException();
	string ISeriesDataAdapter.GetQualitativeValue(int index, SeriesDataMemberType dataMember) => throw new NotImplementedException();

	#endregion
}
