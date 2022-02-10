using Answers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AnswersTests;

[TestClass]
public class LoanInfoTests
{
    // DynamicData tests are nice when you want to spin up a bunch of test cases quickly
    // I recognize that it can be hard to parse and that specific scenario tests are typically more desirable
    // You can also pass in names or some identifier along with the expected result
    // to make finding any failing cases easier as I did in WordConstructionTests
    
    #region CalculateMonthlyPayment
    
    [DynamicData(nameof(CalculateMonthlyPaymentData), DynamicDataSourceType.Method)]
    [TestMethod()]
    public void CalculateMonthlyPaymentTest(decimal principal, int durationInYears, decimal rate, decimal expected)
    {
        var result = LoanEstimation.CalculateMonthlyPayment(principal, durationInYears, rate);

        Assert.AreEqual(expected, result,
            $"Principal {principal:C} @ {rate}% for {durationInYears} years failed.");
    }

    private static IEnumerable<object[]> CalculateMonthlyPaymentData()
    {
        return new List<object[]>()
        {
            new object[]{ 200000m, 30, .03m,   843.21m },
            new object[]{ 200000m, 20, .025m, 1059.81m },
            new object[]{  10000m, 10, .05m,   106.07m },
            new object[]{   6000m,  2, .14m,   288.08m },
            new object[]{ 500000m, 27, .045m, 2668.60m },
        };
    }

    #endregion

    #region GenerateAmortizationTable

    // I opted not to test the actual table values, just the total interest (they're all slightly off anyways)
    [DynamicData(nameof(GenerateAmortizationTableData), DynamicDataSourceType.Method)]
    [TestMethod()]
    public void GenerateAmortizationTableTest(decimal principal, int durationInYears, decimal rate, decimal monthlyPayment, decimal expected)
    {
        var result = LoanEstimation.GenerateAmortizationTable(principal, durationInYears, rate, monthlyPayment);

        Assert.AreEqual(expected, result,
            $"Principal {principal:C} @ {rate}% for {durationInYears} years failed.");
    }

    private static IEnumerable<object[]> GenerateAmortizationTableData()
    {
        return new List<object[]>()
        {
            new object[]{ 200000m, 30, .03m,   843.21m, 103554.47m },
            new object[]{ 200000m, 20, .025m, 1059.81m,  54353.09m },
            new object[]{  10000m, 10, .05m,   106.07m,   2727.70m },
            new object[]{   6000m,  2, .14m,   288.08m,    913.85m },
            new object[]{ 500000m, 27, .045m, 2668.60m, 364627.94m },
        };
    }

    #endregion
}