using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        // var timeRange = new TimeRange(new DateOnly(2022, 1, 1), new DateOnly(2022, 1, 1));
        // var savingStrategy = new SavingStrategy(new Amount(500000, Currency.EGP), PercentageOrAmount.Amount);

        // var createBudgetResult = Budget.From(timeRange, new Amount(850000, Currency.EGP), savingStrategy);

        // if (createBudgetResult.IsError)
        // {
        //     Console.WriteLine(createBudgetResult.Errors.First().Description);
        //     return;
        // }

        // var budgetRepository = services.GetRequiredService<IBudgetRepository>();

        // await budgetRepository.SaveBudgetAsync(createBudgetResult.Value);

        // var fetchResult = await budgetRepository.GetBudgetAsync(Guid.Empty);

        // fetchResult.Switch(
        //     (budget) => Console.WriteLine(budget),
        //     (err) => Console.WriteLine(err.First().Description)
        // );
    }
}
