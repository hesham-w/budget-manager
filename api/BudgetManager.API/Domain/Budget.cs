using System.ComponentModel.DataAnnotations;
using ErrorOr;

namespace BudgetManager.Domain;

public record Budget
{
    public Guid Id { get; set; }

    public TimeRange TimeRange { get; set; }
    public SavingStrategy SavingStrategy { get; set; }
    public ExpensesStrategy ExpensesStrategy { get; set; }

    public Amount TotalExpectedIncome { get; set; }
    public Amount TotalActualIncome { get; set; }
    public Amount TotalExpectedExpenses { get; set; }
    public Amount TotalActualExpenses { get; set; }
    public Amount TotalExpectedSavings { get; set; }
    public Amount TotalActualSavings { get; set; }


    private Budget(TimeRange timeRange, Amount totalExpectedIncome, SavingStrategy savingStrategy)
    {
        if (savingStrategy.PercentageOrAmount == PercentageOrAmount.Amount && totalExpectedIncome.LessThan(savingStrategy.Amount))
        {
            throw new ArgumentException("Amount provided in Savings should be less than or equal the total expected income");
        }

        TimeRange = timeRange;
        TotalExpectedIncome = totalExpectedIncome;
        SavingStrategy = savingStrategy;
    }

    public static ErrorOr<Budget> From(TimeRange timeRange, Amount totalExpectedIncome, SavingStrategy savingStrategy)
    {
        var budget = new Budget(timeRange, totalExpectedIncome, savingStrategy);

        return Plan(budget);
    }

    private static Budget Plan(Budget budget)
    {
        var currentDate = DateTime.Now;

        return budget;
    }

    public override string ToString()
    {
        return $"TotalExpectedIncome : {TotalExpectedIncome}";
    }
}

public record Amount([Range(0, Int32.MaxValue)] double Value, Currency Currency)
{
    public bool GreaterThan(Amount ammount)
    {
        return Value > ammount.Value;
    }

    public bool LessThan(Amount money)
    {
        return Value < money.Value;
    }
};

public record SavingStrategy(Amount Amount, PercentageOrAmount PercentageOrAmount)
{
    internal double CalculateAnnualSavings(Amount annualIncome)
    {
        return annualIncome.Value * Amount.Value;
    }
};

public record ExpensesStrategy([Range(0, Int32.MaxValue)] double Number, PercentageOrAmount PercentageOrAmount);

public record TimeRange(DateOnly StartDate, DateOnly EndDate);