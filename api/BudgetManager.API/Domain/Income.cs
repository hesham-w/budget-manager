namespace BudgetManager.Domain;

public class Income
{
    public double Amount { get; set; }
    public double AmountAfterTaxes { get; set; }
    public Currency Currency { get; set; }
    public AnnualOrMonthly AnnualOrMonthly { get; set; }
}
