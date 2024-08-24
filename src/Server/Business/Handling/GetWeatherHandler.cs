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
        _logger.LogInformation("Trying to get weather forecast from the storage.");

        var weatherResult = await _storage.GetWeatherForecastByDateAsync(request.Date, cancellationToken);

        if (weatherResult.IsFailure)
        {
            return weatherResult;
        }

        _logger.LogInformation($"Found weather forecast: Date - {weatherResult.Value!.Date}; Temperature - {weatherResult.Value!.Weather.Temperature}");

        return weatherResult;
    }
}
