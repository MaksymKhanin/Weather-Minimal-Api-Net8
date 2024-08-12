using Business.Mappings;
using Business.Services;
using Persistence;
using Weather_Minimal_Api;
using Weather_Minimal_Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IStorage, InMemoryStorage>();
builder.Services.AddAutoMapper(typeof(RequestToDomainMapping));
builder.Services.AddAutoMapper(typeof(DomainToResponseMapping));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

WeatherEndpoints.MapWeatherEndpoints(app);

app.Run();


