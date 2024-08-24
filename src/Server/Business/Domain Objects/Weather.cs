using Core;

namespace Business.Domain_Objects;
public sealed class Weather : IEquatable<Weather>
{
    public double Temperature { get; private init; }
    public WindDirection WindDirection { get; private init; }
    public double WindSpeed { get; private init; }
    public string Name { get; private init; }
    public string Description { get; private init; }
    public string? Recommendation { get; private init; }

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
            return new WeatherValidationError(nameof(windSpeed), windSpeed.ToString());
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return new WeatherValidationError(nameof(name), name.ToString());
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return new WeatherValidationError(nameof(description), description.ToString());
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
