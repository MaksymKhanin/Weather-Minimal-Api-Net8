using Business.Domain_Objects;
using Core;

namespace Business.Services;
public interface IStorage
{
    Task<Result> AddWeatherForecastAsync(WeatherForecast weatherForecast, CancellationToken cancellationToken = default);
    Task ClearAsync(CancellationToken cancellationToken);
    Task<Result<WeatherForecast>> GetWeatherForecastByDateAsync(DateOnly date, CancellationToken cancellationToken = default);
}
