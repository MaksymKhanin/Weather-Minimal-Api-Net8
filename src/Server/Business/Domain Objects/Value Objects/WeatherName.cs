using Business.Domain_Objects.Value_Objects;
using Core;

namespace Weather_Minimal_Api;

public sealed class WeatherName : ValueObject
{
    public const int MaxLength = 200;

    public string Value { get; }

    private WeatherName(string value)
    {
        Value = value;
    }

    public static Result<WeatherName> Create(string weatherName)
    {
        if (string.IsNullOrWhiteSpace(weatherName))
        {
            return new WeatherNameIsEmptyError();
        }

        if (weatherName.Length > MaxLength)
        {
            return new WeatherNameIsTooLongError();
        }

        return Result.Success(new WeatherName(weatherName));
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
