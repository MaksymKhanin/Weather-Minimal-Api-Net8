namespace Weather_Minimal_Api.DTOs;

public record WeatherForecastResponse(DateOnly Date, WeatherResponse Weather);
public record WeatherResponse(double Temperature, string Description, double WindSpeed);
