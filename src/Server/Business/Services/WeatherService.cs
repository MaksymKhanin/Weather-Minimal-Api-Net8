using AutoMapper;
using Business.Domain_Objects;
using Core;
using Microsoft.Extensions.Logging;

namespace Business.Services;
public class WeatherService(ILogger<WeatherService> logger, IMapper mapper, IStorage storage) : IWeatherService
{
    private readonly ILogger<WeatherService> _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly IStorage _storage = storage;

    public async Task<Result<WeatherForecast>> GetWeatherForecastByDateAsync(DateOnly date, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Trying to get weather forecast from the storage.");

        var result = await _storage.GetWeatherForecastByDateAsync(date, cancellationToken);

        if (result.IsFailure)
        {
            return result;
        }

        _logger.LogInformation($"Found weather forecast: Date - {result.Value!.Date}; Temperature - {result.Value!.Weather.Temperature}");

        return result;
    }

    public async Task<Result> AddWeatherForecastAsync(WeatherForecast weatherForecast, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Trying to add weather forecast to the storage.");
        
        var result = await _storage.AddWeatherForecastAsync(weatherForecast, cancellationToken);

        if (result.IsFailure)
        {
            return result;
        }

        _logger.LogInformation("Successfully added weather forecast.");

        return result;
    }

    public async Task<Result> ClearAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Trying to clear the storage.");

        await _storage.ClearAsync(cancellationToken);

        _logger.LogInformation("Storaged was cleared successfully.");

        return Result.Success();
    }
}