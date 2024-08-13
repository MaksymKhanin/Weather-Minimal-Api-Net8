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
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

WeatherEndpoints.MapWeatherEndpoints(app);

app.Run();


