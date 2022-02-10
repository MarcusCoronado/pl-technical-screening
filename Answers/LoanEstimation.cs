using System;

namespace Answers;

public static class LoanEstimation
{
    /// <summary>
    /// Gives a summary of the entire loan estimation, including monthly payment, amortization schedule, and total interest paid
    /// </summary>
    public static void GetLoanEstimation(decimal principal, int durationInYears, decimal rate)
    {
        Console.WriteLine($"\nFor a loan of {principal:C}, at {rate*100}% for {durationInYears} years...");
        var monthlyPayment = CalculateMonthlyPayment(principal, durationInYears, rate);
        var totalInterest = GenerateAmortizationTable(principal, durationInYears, rate, monthlyPayment);
    }

    public static decimal CalculateMonthlyPayment(decimal principal, int durationInYears, decimal rate)
    {
        // A = P * (r * (1 + r)^n) / ((1 + r)^n - 1)
        var numberOfPayments = durationInYears * 12; //n
        var periodicInterestRate = rate / 12; // r
        
        // I recognize casting as double probably is not ideal here
        var onePlusRToTheN = (decimal)Math.Pow((double)(1 + periodicInterestRate), (double)numberOfPayments); // x
        
        // A = P * (r * x) / (x - 1)
        var monthlyPayment = principal * (periodicInterestRate * onePlusRToTheN) / (onePlusRToTheN - 1);
        monthlyPayment = Math.Round(monthlyPayment, 2);
        
        Console.WriteLine($"\tMonthly Payment: {monthlyPayment:C}");
        
        return monthlyPayment;
    }

    /// <summary>
    /// Generates both the amortization schedule as well as the total interest paid
    /// </summary>
    public static decimal GenerateAmortizationTable(decimal principal, int durationInYears, decimal rate, decimal monthlyPayment)
    {
        var balance = principal;
        var monthlyRate = rate / 12;
        var numberOfPayments = durationInYears * 12;
        var totalInterest = 0m;

        Console.WriteLine($"Payment | Paid to Principal |  Paid to Interest |    Total Interest |           Balance");
        
        for (int i = 1; i <= numberOfPayments; i++)
        {
            var begBalance = balance;
            var interestPayment = balance * monthlyRate;
            var paidToPrincipal = Math.Min(monthlyPayment - interestPayment, begBalance);
            
            balance = balance - paidToPrincipal;
            totalInterest = totalInterest + interestPayment;
            
            // Rounding only for display
            Console.WriteLine($"{i, 7} | {Math.Round(paidToPrincipal, 2), 17:C} | {Math.Round(interestPayment, 2), 17:C} | {Math.Round(totalInterest, 2), 17:C} | {Math.Round(balance, 2), 17:C}");
        }

        totalInterest = Math.Round(totalInterest, 2);
        Console.WriteLine($"\nTotal Interest Paid: {totalInterest:C}\n");
        
        return totalInterest;
    }
}
