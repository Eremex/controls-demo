using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.ViewModels;

public partial class CartesianCandlestickAggregationViewModel : ChartsPageViewModel
{
    const double MinPrice = 5.0;
    const double StartPrice = 24.0;
    const int ItemsCount = 30 * 24 * 60; 
    
    [ObservableProperty] SummaryCandlestickDataAdapter stockData;
    [ObservableProperty] DateTimeUnit measureUnit = DateTimeUnit.Hour;
    [ObservableProperty] int measureUnitFactor = 1;
    
    public CartesianCandlestickAggregationViewModel()
    {
        var random = new Random(3);
        var arguments = new List<DateTime>(ItemsCount);
        var values = new List<double>(ItemsCount);
        var now = DateTime.Now;
        double prevValue = StartPrice;
        for (int i = 0; i < ItemsCount; i++)
        {
            double value = prevValue + (random.NextDouble() - 0.5) / 8d;
            if (value <= MinPrice)
                value = 2 * MinPrice - value;
            arguments.Add(now.AddMinutes(i - ItemsCount));
            values.Add(value);
            prevValue = value;
        }

        stockData = new SummaryCandlestickDataAdapter(arguments, values) { MeasureUnit = MeasureUnit, MeasureUnitFactor = MeasureUnitFactor };
    }

    [RelayCommand]
    void SetMeasureUnit(DateTimeUnitItem unit)
    {
        MeasureUnit = unit.Unit;
        MeasureUnitFactor = unit.Factor;
        StockData.MeasureUnit = unit.Unit;
        StockData.MeasureUnitFactor = unit.Factor;
    }
}

public class DateTimeUnitItem
{
    public DateTimeUnit Unit { get; set; }
    public int Factor { get; set; }
}