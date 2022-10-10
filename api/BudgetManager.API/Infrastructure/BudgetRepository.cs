using BudgetManager.Infrastructure.Interfaces;
using BudgetManager.Domain;
using ErrorOr;

public class BudgetRepository : IBudgetRepository
{

    private readonly Dictionary<Guid, Budget> _dictionary = new Dictionary<Guid, Budget>();

    public async Task<ErrorOr<Budget>> GetBudgetAsync(Guid budgetId)
    {
        if (!_dictionary.Keys.Contains(budgetId))
        {
            return Error.NotFound();
        }

        return _dictionary[budgetId];
    }

    public Task SaveBudgetAsync(Budget budget)
    {
        if (_dictionary.Keys.Contains(budget.Id))
        {
            _dictionary[budget.Id] = budget;
            return Task.CompletedTask;
        }

        _dictionary.Add(budget.Id, budget);
        return Task.CompletedTask;
    }

    Task IBudgetRepository.SaveBudgetAsync(Budget budget)
    {
        throw new NotImplementedException();
    }
}