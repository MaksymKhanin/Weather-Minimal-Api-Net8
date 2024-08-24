using Business.Domain_Objects;
using Core;
using MediatR;

namespace Business.Commands;
public sealed record AddWeatherCommand(DateOnly Date, double Temperature, WindDirection WindDirection, double WindSpeed, string Name, string Description, string? Recommendation) : IRequest<Result>;
public sealed record ClearCommand() : IRequest<Result>;
