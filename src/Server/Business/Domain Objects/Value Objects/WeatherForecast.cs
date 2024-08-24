using Business.Domain_Objects.Value_Objects;
using Core;

namespace Business.Domain_Objects;

public sealed class WeatherForecast : ValueObject
{
    public DateOnly Date { get; private init; }
    public Weather Weather { get; private init; }

    private WeatherForecast(DateOnly date, Weather weather) =>
        (Date, Weather) = (date, weather);

    internal static Result<WeatherForecast> Create(DateOnly date, Weather weather)
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

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Date;
        yield return Weather;
    }
}