using AutoMapper;
using Business.Commands;
using Business.Domain_Objects;
using Business.Ports;
using Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Weather_Minimal_Api;

namespace Business.Handling;
internal class AddWeatherHandler(ILogger<AddWeatherHandler> logger, IMapper mapper, IStorage storage) : IRequestHandler<AddWeatherCommand, Result>
{
    private readonly ILogger<AddWeatherHandler> _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly IStorage _storage = storage;

    public async Task<Result> Handle(AddWeatherCommand request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Trying to add weather forecast to the storage.");

        var weatherNameResult = WeatherName.Create(request.Name);

        if (weatherNameResult.IsFailure)
        {
            _logger.LogError(weatherNameResult.Error!.ErrorCode + weatherNameResult.Error!.Message);
            return weatherNameResult;
        }

        var weather = Weather.Create(request.Temperature, request.WindDirection, request.WindSpeed, weatherNameResult.Value!, request.Description, request.Recommendation);

        if (weather.IsFailure)
        {
            _logger.LogError(weatherNameResult.Error!.ErrorCode + weatherNameResult.Error!.Message);
            return weather;
        }

        var weatherForecastResult = WeatherForecast.Create(request.Date, weather.Value!);

        if (weatherForecastResult.IsFailure)
        {
            _logger.LogError(weatherNameResult.Error!.ErrorCode + weatherNameResult.Error!.Message);
            return weatherForecastResult;
        }

        var weatherResult = await _storage.AddWeatherForecastAsync(weatherForecastResult.Value!, cancellationToken);

        if (weatherResult.IsFailure)
        {
            _logger.LogError(weatherNameResult.Error!.ErrorCode + weatherNameResult.Error!.Message);
            return weatherResult;
        }

        _logger.LogInformation("Successfully added weather forecast to the storage.");

        return weatherResult;
    }
}
