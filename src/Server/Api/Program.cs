using Business.Mappings;
using Business.Services;
using Persistence;
using Weather_Minimal_Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IStorage, InMemoryStorage>();
builder.Services.AddAutoMapper(typeof(RequestToDomainMapping));
builder.Services.AddAutoMapper(typeof(DomainToResponseMapping));

var app = builder.Build();

WeatherEndpoints.MapWeatherEndpoints(app);

app.Run();


