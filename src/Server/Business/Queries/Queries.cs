using Business.Domain_Objects;
using Core;
using MediatR;

namespace Business.Queries;
public sealed record GetWeatherQuery(DateOnly Date) : IRequest<Result<WeatherForecast>>;
