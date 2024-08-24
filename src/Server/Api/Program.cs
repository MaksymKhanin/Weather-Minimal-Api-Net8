using Business.Mappings;
using Business.Ports;
using Persistence;
using Weather_Minimal_Api;
using Weather_Minimal_Api.Configuratins;
using Weather_Minimal_Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IStorage, InMemoryStorage>();
builder.Services.AddAutoMapper(typeof(RequestToDomainMapping));
builder.Services.AddAutoMapper(typeof(DomainToResponseMapping));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddMediatorPipelineBehavior();

var app = builder.Build();

app.UseExceptionHandler();

WeatherEndpoints.MapWeatherEndpoints(app);

app.Urls.Add("http://*:5000");

app.Run();

public partial class Program
{
    protected Program() { }
}


