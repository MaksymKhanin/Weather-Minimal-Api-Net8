using Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Domain_Objects;
public class Weather : IEquatable<Weather>
{
    public double Temperature { get; private set; }
    public WindDirection WindDirection { get; private set; }
    public double WindSpeed { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string? Recommendation { get; private set; }

    private Weather(double temperature, WindDirection windDirection, double windSpeed, string name, string description, string? recommendation) =>
        (Temperature, WindDirection, WindSpeed, Name, Description, Recommendation) = (temperature, windDirection, windSpeed, name, description, recommendation);

    public static Result<Weather> Create(double temperature, WindDirection windDirection, double windSpeed, string name, string description, string? recommendation)
    {
        if (temperature == default)
        {
            return new WeatherValidationError(nameof(temperature), temperature.ToString());
        }

        if (windSpeed == default)
        {
            return new WeatherForecastValidationError(nameof(windSpeed), windSpeed.ToString());
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return new WeatherForecastValidationError(nameof(name), name.ToString());
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return new WeatherForecastValidationError(nameof(description), description.ToString());
        }

        return Result.Success(new Weather(temperature, windDirection, windSpeed, name, description, recommendation));
    }

    public bool Equals(Weather? other)
    {
        if (other == null)
        {
            return false;
        }

        if (Temperature == other.Temperature &&
            WindDirection == other.WindDirection &&
            WindSpeed == other.WindSpeed &&
            Name == other.Name &&
            Description == other.Description &&
            Recommendation == other.Recommendation)
        {

            return true;
        }

        return false;
    }
}
