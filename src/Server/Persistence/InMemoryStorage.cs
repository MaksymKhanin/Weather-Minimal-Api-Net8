using Business.Domain_Objects;
using Business.Services;
using Core;
using Microsoft.Extensions.Logging;

namespace Persistence;
public class InMemoryStorage(ILogger<InMemoryStorage> logger) : IStorage
{
    private readonly ILogger<InMemoryStorage> _logger = logger;

    private static List<WeatherForecast> InMemory = new List<WeatherForecast>();

    public async Task<Result> AddWeatherForecastAsync(WeatherForecast weatherForecast, CancellationToken cancellationToken = default)
    {
        if (InMemory.Contains(weatherForecast))
        {
            return Result.FailAndLog(new WeatherForecastAlreadyExistsError(), _logger, LogLevel.Warning);
        }

        InMemory.Add(weatherForecast);

        return await Task.FromResult(Result.Success());
    }
    public async Task<Result<WeatherForecast>> GetWeatherForecastByDateAsync(DateOnly date, CancellationToken cancellationToken = default)
    {
        var weatherForecast = InMemory.FirstOrDefault(x => x.Date.Equals(date));

        if (weatherForecast is null)
        {
            return Result.FailAndLog(new WeatherForecastNotFoundError(date), _logger, LogLevel.Warning);
        }

        return await Task.FromResult(Result.Success(weatherForecast));
    }

    public async Task ClearAsync(CancellationToken cancellationToken)
    {
        InMemory.Clear();
        await Task.CompletedTask;
    }
}
