using AutoMapper;
using Business.Domain_Objects;
using Business.Ports;
using Business.Queries;
using Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Business.Handling;
internal class GetWeatherHandler(ILogger<GetWeatherHandler> logger, IMapper mapper, IStorage storage) : IRequestHandler<GetWeatherQuery, Result<WeatherForecast>>
{
    private readonly ILogger<GetWeatherHandler> _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly IStorage _storage = storage;

    public async Task<Result<WeatherForecast>> Handle(GetWeatherQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to retrieve weather forecast from the storage for Date: {Date}", request.Date);

        var weatherResult = await _storage.GetWeatherForecastByDateAsync(request.Date, cancellationToken);

        if (weatherResult.IsFailure)
        {
            return weatherResult;
        }

        _logger.LogInformation("Weather forecast found for Date: {Date}; Temperature: {Temperature}°C", weatherResult.Value!.Date, weatherResult.Value!.Weather.Temperature);

        return weatherResult;
    }
}
