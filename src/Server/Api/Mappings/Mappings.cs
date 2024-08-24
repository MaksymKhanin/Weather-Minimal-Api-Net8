using AutoMapper;
using Business.Commands;
using Business.Domain_Objects;
using Business.Queries;
using Core;
using Weather_Minimal_Api.DTOs;

namespace Business.Mappings;
public class RequestToDomainMapping : Profile
{
    public RequestToDomainMapping()
    {
        CreateMap<AddWeatherRequest, AddWeatherCommand>();
        CreateMap<GetWeatherRequest, GetWeatherQuery>();
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