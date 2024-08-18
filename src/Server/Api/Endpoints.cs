using AutoMapper;
using Business.Domain_Objects;
using Business.Services;
using Core;
using Weather_Minimal_Api.DTOs;
using Weather_Minimal_Api.Extensions;

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
            error => error.ToBadRequestProblemDetails(),
            notFound => notFound.ToNotFoundProblemDetails());
    }

    public async static Task<IResult> AddWeatherAsync([AsParameters] AddWeatherRequest addWeatherForecastRequest, IMapper _mapper, ILogger<WeatherEndpoints> _logger, IWeatherService _weatherService, CancellationToken cancellationToken = default)
    {
        _logger.BeginScope("Request: {@request}", addWeatherForecastRequest);
        _logger.LogInformation("Received request to add weather forecast: {@weatherForecast}", addWeatherForecastRequest);

        var weatherForecastResult = _mapper.Map<Result<WeatherForecast>>(addWeatherForecastRequest.WeatherForecast);

        if (weatherForecastResult.IsFailure)
        {
            _logger.LogError(weatherForecastResult.Error!.Message);
            return weatherForecastResult.ToProblemDetails();
        }

        var addWeatherForecastResult = await _weatherService.AddWeatherForecastAsync(weatherForecastResult.Value!, cancellationToken);

        return (addWeatherForecastResult.IsSuccess)
            ? TypedResults.Ok()
            : addWeatherForecastResult.ToProblemDetails();
    }

    public async static Task<IResult> ClearAsync(ILogger<WeatherEndpoints> _logger, IWeatherService _weatherService, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Received request to clear storage");

        var result = await _weatherService.ClearAsync(cancellationToken);

        return (result.IsSuccess)
            ? TypedResults.Ok()
            : result.ToProblemDetails();
    }
}
