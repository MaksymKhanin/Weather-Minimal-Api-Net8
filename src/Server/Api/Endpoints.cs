using AutoMapper;
using Business.Commands;
using Business.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

    private static async Task<IResult> GetWeatherForecastForDateAsync([FromBody] GetWeatherRequest getWeatherRequest, IMapper _mapper, IMediator _mediator, ILogger<WeatherEndpoints> _logger, CancellationToken cancellationToken = default)
    {
        _logger.BeginScope("Request: {@request}", getWeatherRequest);
        _logger.LogInformation("Received request to get weather forecast by date: {@date}", getWeatherRequest.Date);

        return (await _mediator.Send(_mapper.Map<GetWeatherQuery>(getWeatherRequest), cancellationToken)).Match(
            success => TypedResults.Ok(_mapper.Map<WeatherForecastResponse>(success)),
            error => error.ToBadRequestProblemDetails(),
            notFound => notFound.ToNotFoundProblemDetails());
    }

    private static async Task<IResult> AddWeatherAsync([FromBody] AddWeatherRequest addWeatherForecastRequest, IMediator _mediator, IMapper _mapper, ILogger<WeatherEndpoints> _logger, CancellationToken cancellationToken = default)
    {
        _logger.BeginScope("Request: {@request}", addWeatherForecastRequest);
        _logger.LogInformation("Received request to add weather forecast: {@weatherForecast}", addWeatherForecastRequest);

        var addWeatherResult = await _mediator.Send(_mapper.Map<AddWeatherCommand>(addWeatherForecastRequest), cancellationToken);

        return (addWeatherResult.IsSuccess)
            ? TypedResults.Ok()
            : addWeatherResult.ToProblemDetails();
    }

    private static async Task<IResult> ClearAsync(ILogger<WeatherEndpoints> _logger, IMediator _mediator, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Received request to clear storage");

        var result = await _mediator.Send(new ClearCommand(), cancellationToken);

        return (result.IsSuccess)
            ? TypedResults.Ok()
            : result.ToProblemDetails();
    }
}
