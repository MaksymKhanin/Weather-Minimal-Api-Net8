using AutoMapper;
using Business.Domain_Objects;
using Business.Services;
using Weather_Minimal_Api.DTOs;

namespace Weather_Minimal_Api;


public class WeatherEndpoints
{
    public static void MapWeatherEndpoints(IEndpointRouteBuilder app)
    {
        var weather = app.MapGroup("api/Weather");

        weather.MapGet("/GetWeatherForecastForDate", GetWeatherForecastForDateAsync);
        weather.MapPost("/AddWeather", AddWeatherAsync);
        weather.MapPost("/Clear", ClearAsync);
    }

    public async static Task<IResult> GetWeatherForecastForDateAsync(DateOnly date, ILogger<WeatherEndpoints> _logger, IMapper _mapper, IWeatherService _weatherService, CancellationToken cancellationToken = default)
    {
        _logger.BeginScope("Request: {@request}", date);
        _logger.LogInformation("Received request to get weather forecast by date: {@date}", date);

        return (await _weatherService.GetWeatherForecastByDateAsync(date, cancellationToken)).Match<IResult>(
            success => TypedResults.Ok(_mapper.Map<WeatherForecastResponse>(success)),
            error => TypedResults.BadRequest(error),
            notFound => TypedResults.NotFound(notFound));
    }

    public async static Task<IResult> AddWeatherAsync([AsParameters] AddWeatherRequest addWeatherForecastRequest, IMapper _mapper, ILogger<WeatherEndpoints> _logger, IWeatherService _weatherService, CancellationToken cancellationToken = default)
    {
        _logger.BeginScope("Request: {@request}", addWeatherForecastRequest);
        _logger.LogInformation("Received reuest to add weather forecast: {@weatherForecast}", addWeatherForecastRequest);

        var weatherForecast = _mapper.Map<WeatherForecast>(addWeatherForecastRequest.WeatherForecast);

        var result = await _weatherService.AddWeatherForecastAsync(weatherForecast, cancellationToken);

        return (result.IsSuccess)
            ? TypedResults.Ok()
            : TypedResults.BadRequest(result.Error);
    }

    public async static Task<IResult> ClearAsync(ILogger<WeatherEndpoints> _logger, IWeatherService _weatherService, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Received reuest to clear storage");

        var result = await _weatherService.ClearAsync(cancellationToken);

        return (result.IsSuccess)
            ? TypedResults.Ok()
            : TypedResults.BadRequest(result.Error);
    }
}
