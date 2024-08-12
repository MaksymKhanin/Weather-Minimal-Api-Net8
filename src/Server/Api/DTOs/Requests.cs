using Business.Domain_Objects;

namespace Weather_Minimal_Api.DTOs;

public record AddWeatherRequest(WeatherForecastRequest WeatherForecast);
public record WeatherForecastRequest(DateOnly Date, WeatherRequest Weather);
public record WeatherRequest(double Temperature, WindDirection WindDirection, double WindSpeed, string Name, string Description, string? Recommendation);