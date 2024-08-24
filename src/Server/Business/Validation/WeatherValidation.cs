using Business.Commands;
using Business.Queries;
using FluentValidation;

namespace Business.Validation;

public class WeatherValidation : AbstractValidator<AddWeatherCommand>
{
    public WeatherValidation()
    {
        RuleFor(x => x.Date)
            .NotEmpty();

        RuleFor(x => x.Temperature)
            .NotEmpty();

        RuleFor(x => x.WindSpeed)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotEmpty();
    }
}

public class GetWeatherValidation : AbstractValidator<GetWeatherQuery>
{
    public GetWeatherValidation()
    {
        RuleFor(x => x.Date)
            .NotEmpty();
    }
}
