using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.ViewModels.DataAdapters;

namespace DemoCenter.ViewModels;

public partial class CartesianScatterLineSeriesViewViewModel : ChartsPageViewModel
{
    const int ItemsCount = 500;
    
    static double ArgumentFormula(int index) => index * 0.1 * Math.Cos(index * 0.1);
    static double ValueFormula(int index) => index * 0.1 * Math.Sin(index * 0.1);

    [ObservableProperty] SeriesViewModel series = new() { Color = Color.FromArgb(255, 189, 20, 54), DataAdapter = new ScatterFormulaDataAdapter(ItemsCount, ArgumentFormula, ValueFormula) };
}
