using BudgetManager.Models;
using ErrorOr;

namespace BudgetManager.Infrastructure.Interfaces;

public interface IBudgetRepository
{
    Task<ErrorOr<Budget>> GetBudgetAsync(Guid budgetId);

    Task SaveBudgetAsync(Budget budget);
}