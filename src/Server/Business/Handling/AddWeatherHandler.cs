using AutoMapper;
using Business.Commands;
using Business.Domain_Objects;
using Business.Ports;
using Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Business.Handling;
internal class AddWeatherHandler(ILogger<AddWeatherHandler> logger, IMapper mapper, IStorage storage) : IRequestHandler<AddWeatherCommand, Result>
{
    private readonly ILogger<AddWeatherHandler> _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly IStorage _storage = storage;

    public async Task<Result> Handle(AddWeatherCommand request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Trying to add weather forecast to the storage.");

        var weather = Weather.Create(request.Temperature, request.WindDirection, request.WindSpeed, request.Name, request.Description, request.Recommendation);

        if (weather.IsFailure)
        {
            return weather;
        }

        var weatherForecast = WeatherForecast.Create(request.Date, weather.Value!);

        if (weatherForecast.IsFailure)
        {
            return weatherForecast;
        }

        var weatherResult = await _storage.AddWeatherForecastAsync(weatherForecast.Value!, cancellationToken);

        if (weatherResult.IsFailure)
        {
            return weatherResult;
        }

        _logger.LogInformation("Successfully added weather forecast to the storage.");

        return weatherResult;
    }
}
