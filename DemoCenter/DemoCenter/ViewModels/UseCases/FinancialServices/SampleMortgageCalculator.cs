using System;
using System.Collections.Generic;

namespace DemoCenter.Models;

public class SampleMortgageCalculator
{
    /// <summary>
    /// Calculates the monthly payment for a mortgage
    /// </summary>
    /// <param name="principal">The loan amount</param>
    /// <param name="annualInterestRate">Annual interest rate (as a percentage, e.g., 3.5 for 3.5%)</param>
    /// <param name="loanTermYears">Loan term in years</param>
    /// <returns>Monthly payment amount</returns>
    public static decimal CalculateMonthlyPayment(
        decimal principal,
        decimal annualInterestRate,
        int loanTermYears)
    {
        if (principal <= 0)
            throw new ArgumentOutOfRangeException(nameof(principal), "Principal must be positive");

        if (annualInterestRate < 0)
            throw new ArgumentOutOfRangeException(nameof(annualInterestRate), "Interest rate cannot be negative");

        if (loanTermYears <= 0)
            throw new ArgumentOutOfRangeException(nameof(loanTermYears), "Loan term must be positive");

        if (annualInterestRate == 0)
            return principal / (loanTermYears * 12);

        var monthlyRate = annualInterestRate / 100 / 12;
        var numberOfPayments = loanTermYears * 12;

        var payment = principal * monthlyRate * (decimal)Math.Pow(1 + (double)monthlyRate, numberOfPayments)
                    / (decimal)(Math.Pow(1 + (double)monthlyRate, numberOfPayments) - 1);

        return Math.Round(payment, 2);
    }

    /// <summary>
    /// Generates an amortization schedule for the mortgage
    /// </summary>
    /// <param name="principal">The loan amount</param>
    /// <param name="annualInterestRate">Annual interest rate (as a percentage)</param>
    /// <param name="loanTermYears">Loan term in years</param>
    /// <returns>List of monthly payment details</returns>
    public static List<PaymentDetail> GenerateAmortizationSchedule(
        decimal principal,
        decimal annualInterestRate,
        int loanTermYears)
    {
        var schedule = new List<PaymentDetail>();
        var monthlyPayment = CalculateMonthlyPayment(principal, annualInterestRate, loanTermYears);
        var monthlyRate = annualInterestRate / 100 / 12;
        var remainingBalance = principal;

        for (int month = 1; month <= loanTermYears * 12; month++)
        {
            var interestPayment = remainingBalance * monthlyRate;
            var principalPayment = monthlyPayment - interestPayment;

            if (month == loanTermYears * 12)
            {
                principalPayment = remainingBalance;
                interestPayment = monthlyPayment - principalPayment;
                remainingBalance = 0;
            }
            else
            {
                remainingBalance -= principalPayment;
            }

            schedule.Add(new PaymentDetail(
                month,
                monthlyPayment,
                principalPayment,
                interestPayment,
                remainingBalance > 0 ? remainingBalance : 0
            ));
        }

        return schedule;
    }
}

public class PaymentDetail
{
    public int MonthNumber { get; set; }
    public decimal PaymentAmount { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public decimal RemainingBalance { get; set; }

    public PaymentDetail(
        int monthNumber,
        decimal paymentAmount,
        decimal principalAmount,
        decimal interestAmount,
        decimal remainingBalance)
    {
        MonthNumber = monthNumber;
        PaymentAmount =  Math.Round(paymentAmount, 2);
        PrincipalAmount = Math.Round(principalAmount, 2);
        InterestAmount = Math.Round(interestAmount, 2);
        RemainingBalance = Math.Round(remainingBalance, 2);
    }

}