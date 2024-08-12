using AutoMapper;
using Business.Domain_Objects;
using Weather_Minimal_Api.DTOs;

namespace Business.Mappings;
public class RequestToDomainMapping : Profile
{
    public RequestToDomainMapping()
    {
        CreateMap<WeatherForecastRequest, WeatherForecast>().ConstructUsing((dto, context) =>
        {
            var weather = context.Mapper.Map<Weather>(dto.Weather);
            return WeatherForecast.Create(dto.Date, weather);
        });

        CreateMap<WeatherRequest, Weather>().ConstructUsing((WeatherRequest dto) => Weather.Create(dto.Temperature, dto.WindDirection, dto.WindSpeed, dto.Name, dto.Description, dto.Recommendation));
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