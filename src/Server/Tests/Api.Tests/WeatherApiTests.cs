using Api.Tests.Framework;
using FluentAssertions;
using System.Net.Mime;
using System.Text;

namespace Api.Tests;

[Trait("Category", "Unit")]
public sealed class WeatherApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public WeatherApiTests(CustomWebApplicationFactory factory) =>
        _factory = factory;
    
    [Fact(DisplayName = "Creating a weatherForecast should answer 200")]
    public async Task Test01()
    {
        var payload = /*lang=json,strict*/ """
        {
         "date": "2024-08-12",
         "weather": {
           "temperature": 20.5,
           "windDirection": 0,
           "windSpeed": 70.2,
           "name": "sunny",
           "description": "Warm sunny weather, sometimes cloudy"
         }
        }
        """;

        var client = _factory.CreateClient();

        var response = await client.PostAsync("api/Weather/AddWeather", new StringContent(payload, Encoding.UTF8, MediaTypeNames.Application.Json));

        response.Should().Be200Ok();
    }

    [Theory(DisplayName = "Creating a weatherForecast with wrong format should answer 400")]
    [ClassData(typeof(BadRequestJsonTestCases))]
    internal async Task Test02(JsonPayloadTestCaseItem item)
    {
        var client = _factory.CreateClient();

        var response = await client.PostAsync("api/Weather/AddWeather", new StringContent(item.Payload, Encoding.UTF8, MediaTypeNames.Application.Json));

        response.Should().Be400BadRequest();
    }
}
