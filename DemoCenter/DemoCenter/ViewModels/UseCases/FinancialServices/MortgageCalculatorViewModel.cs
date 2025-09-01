using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.Models;
using Eremex.AvaloniaUI.Charts;
using System;

namespace DemoCenter.ViewModels;

public partial class MortgageCalculatorViewModel : PageViewModelBase
{
    [ObservableProperty] decimal principal = 300000m;
    [ObservableProperty] decimal annualInterestRate = 5;
    [ObservableProperty] int loanTermYears = 30;
    [ObservableProperty] decimal monthlyPayment;

    [ObservableProperty] List<PaymentDetail> amortizationSchedule = new();
    [ObservableProperty] PaymentDetail selectedPaymentDetail;
    [ObservableProperty] Bitmap housePreviewImage;

    [ObservableProperty] SortedNumericDataAdapter principalSeriesDataAdapter, interestSeriesDataAdapter, totalMonthlyPaymentSeriesDataAdapter;

    [ObservableProperty] FuncLabelFormatter currencyFormatter = new(o => String.Format("{0:c}", o));
    [ObservableProperty] FuncLabelFormatter argumentFormatter = new(o => String.Format("{0:0}", o));

    public MortgageCalculatorViewModel()
    {
        CalculateMortgage();
    }

    partial void OnPrincipalChanged(decimal value)
    {
        CalculateMortgage();
    }

    partial void OnAnnualInterestRateChanged(decimal value)
    {
        CalculateMortgage();
    }

    partial void OnLoanTermYearsChanged(int value)
    {
        CalculateMortgage();
    }


    private void CalculateMortgage()
    {
        try
        {
            MonthlyPayment = SampleMortgageCalculator.CalculateMonthlyPayment(
                Principal,
                AnnualInterestRate,
                LoanTermYears);

            AmortizationSchedule = SampleMortgageCalculator.GenerateAmortizationSchedule(
                Principal,
                AnnualInterestRate,
                LoanTermYears);
            HousePreviewImage = CalcHousePreviewImage(Principal);
        }
        catch (ArgumentOutOfRangeException)
        {
            // Reset to default values if invalid input
            MonthlyPayment = 0;
            AmortizationSchedule = new List<PaymentDetail>();
        }
        var argumentList = AmortizationSchedule.Select(s => (double)s.MonthNumber).ToList();
        InterestSeriesDataAdapter = new FormatedTextSortedNumericDataAdapter(argumentList, AmortizationSchedule.Select(s => (double)s.InterestAmount).ToList());
        PrincipalSeriesDataAdapter = new FormatedTextSortedNumericDataAdapter(argumentList, AmortizationSchedule.Select(s => (double)s.PrincipalAmount).ToList());
        TotalMonthlyPaymentSeriesDataAdapter = new FormatedTextSortedNumericDataAdapter(argumentList, AmortizationSchedule.Select(s => (double)s.PaymentAmount).ToList());

    }

    static Bitmap GetImage(string imageName) 
    {
        var imageUrl = "avares://DemoCenter/DemoData/HouseImages/" + imageName;
        return new Bitmap(AssetLoader.Open(new Uri(imageUrl)));
    }

    Dictionary<int, Bitmap> houseImages = new Dictionary<int, Bitmap>()
        {
            {0, GetImage("House-3-Level.jpg") },
            {1, GetImage("House-4-Level.jpg") },
            {2, GetImage("House-5-Level.jpg") },
            {3, GetImage("House-6-Level.jpg") },
            {4, GetImage("House-7-Level.jpg") },
            {5, GetImage("House-Vip.jpg") },

        };


    private Bitmap CalcHousePreviewImage(decimal principal)
    {

        var minimum = 100000;
        var maximum = 1000000;

        var grade = (maximum - minimum) / 5;
        int index = (int)(principal - minimum) / grade;
        return houseImages[index];
    }

}

public class FormatedTextSortedNumericDataAdapter : SortedNumericDataAdapter, ISeriesDataAdapter
{
    public FormatedTextSortedNumericDataAdapter(IList<double> arguments, IList<double> values) : base(arguments, values)
    {
    }

    public string GetDataMemberPrefix(SeriesDataMemberType dataMember)
    {
        switch (dataMember)
        {
            case SeriesDataMemberType.Value:
                return "Payment ";
            default:
                return "Month ";
        }
    }
}