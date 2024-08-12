namespace Business.Domain_Objects;
public class WeatherForecast : IEquatable<WeatherForecast>
{
    public DateOnly Date { get; set; }
    public Weather Weather { get; set; }

    private WeatherForecast(DateOnly date, Weather weather) =>
        (Date, Weather) = (date, weather);

    public static WeatherForecast Create(DateOnly date, Weather weather) =>
        new(date, weather);

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