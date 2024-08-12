using Business.Domain_Objects;
using Core;

namespace Business.Services;
public interface IWeatherService
{
    Task<Result> AddWeatherForecastAsync(WeatherForecast weatherForecast, CancellationToken cancellationToken = default);
    Task<Result> ClearAsync(CancellationToken cancellationToken);
    Task<Result<WeatherForecast>> GetWeatherForecastByDateAsync(DateOnly date, CancellationToken cancellationToken = default);
}