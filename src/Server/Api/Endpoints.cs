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

    private static async Task<IResult> GetWeatherForecastForDateAsync([FromBody] GetWeatherRequest request, IMapper _mapper, IMediator _mediator, ILogger<WeatherEndpoints> _logger, CancellationToken cancellationToken = default)
    {
        using var _ = _logger.BeginScope("Request: {@Request}", request);
        _logger.LogInformation("Fetching weather forecast for date: {Date}", request.Date);

        var query = _mapper.Map<GetWeatherQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);

        return result.Match
        (
            success => TypedResults.Ok(_mapper.Map<WeatherForecastResponse>(success)),
            error => error.ToBadRequest(),
            notFound => notFound.ToNotFound()
        );
    }


    private static async Task<IResult> AddWeatherAsync([FromBody] AddWeatherRequest request, IMediator _mediator, IMapper _mapper, ILogger<WeatherEndpoints> _logger, CancellationToken cancellationToken = default)
    {
        using var _ = _logger.BeginScope("Request: {@Request}", request);
        _logger.LogInformation("Adding weather forecast: {@WeatherForecast}", request);

        var command = _mapper.Map<AddWeatherCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        return result.Match
        (
            success: () => TypedResults.Ok(),
            error: error => error.ToBadRequest()
        );
    }

    private static async Task<IResult> ClearAsync(ILogger<WeatherEndpoints> _logger, IMediator _mediator, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Clearing weather storage");

        var result = await _mediator.Send(new ClearCommand(), cancellationToken);

        return result.Match
        (
            success: () => TypedResults.Ok(),
            error: error => error.ToBadRequest()
        );
    }
}

