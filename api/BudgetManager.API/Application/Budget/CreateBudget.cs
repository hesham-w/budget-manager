using BudgetManager.Infrastructure.Interfaces;
using ErrorOr;
using MediatR;

public record CreateBudgetCommand(double totalExpenses) : IRequest<ErrorOr<CreateBudgetResult>>;
public record CreateBudgetResult(Guid BudgetId);
public class CreateBudgetHandler : IRequestHandler<CreateBudgetCommand, ErrorOr<CreateBudgetResult>>
{
    private readonly IBudgetRepository _budgetRepository;

    public CreateBudgetHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public Task<ErrorOr<CreateBudgetResult>> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

