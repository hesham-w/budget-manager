namespace BudgetManager.Models;

public class TaxCalculator
{
    public Income CaclulateTax(Income income, TaxStrategy taxStrategy)
    {
        income.Amount -= income.Amount * taxStrategy.DeductionPercentage;
        return income;
    }
}
