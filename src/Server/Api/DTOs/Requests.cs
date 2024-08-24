using Business.Domain_Objects;

namespace Weather_Minimal_Api.DTOs;

internal sealed record AddWeatherRequest(DateOnly Date, double Temperature, WindDirection WindDirection, double WindSpeed, string Name, string Description, string? Recommendation);
internal sealed record GetWeatherRequest(DateOnly Date);
