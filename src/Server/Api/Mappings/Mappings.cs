using AutoMapper;
using Business.Domain_Objects;
using Core;
using Weather_Minimal_Api.DTOs;

namespace Business.Mappings;
public class RequestToDomainMapping : Profile
{
    public RequestToDomainMapping()
    {
        CreateMap<WeatherForecastRequest, Result<WeatherForecast>>().ConstructUsing((dto, context) =>
        {
            if (dto.Weather is null)
            {
                return new WeatherForecastValidationError(nameof(dto.Weather), "null");
            }

            var weatherResult = context.Mapper.Map<Result<Weather>>(dto.Weather);

            if (weatherResult.IsFailure)
            {
                return new ValidationError(weatherResult.Error!.ErrorCode, weatherResult.Error.Message);
            }

            return WeatherForecast.Create(dto.Date, weatherResult.Value!);
        });

        CreateMap<WeatherRequest, Result<Weather>>().ConstructUsing((WeatherRequest dto) => Weather.Create(dto.Temperature, dto.WindDirection, dto.WindSpeed, dto.Name, dto.Description, dto.Recommendation));
    }
}

public class DomainToResponseMapping : Profile
{
    public DomainToResponseMapping()
    {
        CreateMap<WeatherForecast, WeatherForecastResponse>();
        CreateMap<Weather, WeatherResponse>();
    }
}