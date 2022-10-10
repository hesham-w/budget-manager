using BudgetManager.Infrastructure.Interfaces;
using BudgetManager.Domain;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace BudgetManager.API.Controllers;

public record APIBudget(Guid BudegtId, double TotalExpectedIncome, double TotalExpectedSavings);

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IBudgetRepository _budgetRepository;
    private readonly IMediator _mediator;
    public WeatherForecastController(ILogger<WeatherForecastController> logger, IBudgetRepository budgetRepository, IMediator mediator)
    {
        _logger = logger;
        _budgetRepository = budgetRepository;
        _mediator = mediator;
    }

    public async Task<IActionResult> FetchQuery<S, D>(Func<Task<ErrorOr<S>>> query, Func<S, D> map)
    where S : class
    where D : class
    {
        var result = await query();

        return result.Match<IActionResult>(
            (value) => Ok(map(value)),
            (errors) =>
            {
                var firstError = errors.First();

                switch (firstError.Type)
                {
                    case ErrorType.Validation: return BadRequest("Request is not valid");
                    case ErrorType.Unexpected: return NotFound("Resource not found");
                    default: return StatusCode(500, "Encountered an unexpected error");
                }
            });
    }

    public Task<IActionResult> ExecuteCommand<S, D>(Func<Task<ErrorOr<S>>> command, Func<S, D> map)
    where S : class
    where D : class
    {
        return FetchQuery(command, map);
    }

    [HttpGet(Name = "GetBudget")]
    public async Task<IActionResult> Get()
    {
        return await FetchQuery<Budget, APIBudget>(
        async () =>
        {
            return await _budgetRepository.GetBudgetAsync(Guid.NewGuid());
        },
            (s) =>
        {
            return new APIBudget(s.Id, s.ExpensesStrategy.Number, s.ExpensesStrategy.Number);
        });
    }

    [HttpGet(Name = "CreateeBudget")]
    public async Task<IActionResult> Post(APIBudget apiBudget)
    {
        return await ExecuteCommand<CreateBudgetResult, APIBudget>(async () =>
        {
            return await _mediator.Send<ErrorOr<CreateBudgetResult>>(new CreateBudgetCommand(0));
        }, (s) =>
        {
            return null;
        });
    }
}
