using Core;

namespace Business.Domain_Objects;

public class WeatherForecast : IEquatable<WeatherForecast>
{
    public DateOnly Date { get; private set; }
    public Weather Weather { get; private set; }

    private WeatherForecast(DateOnly date, Weather weather) =>
        (Date, Weather) = (date, weather);

    public static Result<WeatherForecast> Create(DateOnly date, Weather weather)
    {
        if (date == DateOnly.MinValue)
        {
            return new WeatherForecastValidationError(nameof(date), date.ToString());
        }

        if (weather is null)
        {
            return new WeatherForecastValidationError(nameof(weather), "null");
        }

        return Result.Success(new WeatherForecast(date, weather));
    }


    public bool Equals(WeatherForecast? other)
    {
        if (other == null)
        {
            return false;
        }

        if (Date == other.Date && Weather.Equals(other.Weather))
        {

            return true;
        }

        return false;
    }
}