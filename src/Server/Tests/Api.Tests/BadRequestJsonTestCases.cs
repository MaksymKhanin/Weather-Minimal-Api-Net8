using Xunit.Abstractions;

namespace Api.Tests;
internal sealed class BadRequestJsonTestCases : TheoryData<JsonPayloadTestCaseItem>
{
    public BadRequestJsonTestCases()
    {
        Add(new("date property is absent", /*lang=json,strict*/ """
        {
         "weather": {
           "temperature": 20.5,
           "windDirection": 0,
           "windSpeed": 70.2,
           "name": "sunny",
           "description": "Warm sunny weather, sometimes cloudy"
         }
        }
        """));

        Add(new("date property is empty", /*lang=json,strict*/ """
        {
         "date": "",
         "weather": {
           "temperature": 20.5,
           "windDirection": 0,
           "windSpeed": 70.2,
           "name": "sunny",
           "description": "Warm sunny weather, sometimes cloudy"
         }
        }
        """));

        Add(new("date property is null", /*lang=json,strict*/ """
        {
         "date": null,
         "weather": {
           "temperature": 20.5,
           "windDirection": 0,
           "windSpeed": 70.2,
           "name": "sunny",
           "description": "Warm sunny weather, sometimes cloudy"
         }
        }
        """));

        Add(new("weather property is absent", /*lang=json,strict*/ """
        {
         "date": "2024-08-12"
        }
        """));

        Add(new("weather property is empty", /*lang=json,strict*/ """
        {
         "date": "2024-08-12",
         "weather": ""
        }
        """));

        Add(new("weather property is null", /*lang=json,strict*/ """
        {
         "date": "2024-08-12",
         "weather": null
        }
        """));

        Add(new("temperature property is absent", /*lang=json,strict*/ """
        {
         "date": "2024-08-12",
         "weather": {
           "windDirection": 0,
           "windSpeed": 70.2,
           "name": "sunny",
           "description": "Warm sunny weather, sometimes cloudy"
         }
        }
        """));

        Add(new("temperature property is null", /*lang=json,strict*/ """
        {
         "date": "2024-08-12",
         "weather": {
           "temperature": null,
           "windDirection": 0,
           "windSpeed": 70.2,
           "name": "sunny",
           "description": "Warm sunny weather, sometimes cloudy"
         }
        }
        """));

        Add(new("winddirection property is absent", /*lang=json,strict*/ """
        {
         "date": "2024-08-12",
         "weather": {
           "temperature": 20.5,
           "windSpeed": 70.2,
           "name": "sunny",
           "description": "Warm sunny weather, sometimes cloudy"
         }
        }
        """));

        Add(new("windspeed property is absent", /*lang=json,strict*/ """
        {
         "date": "2024-08-12",
         "weather": {
           "temperature": 20.5,
           "windDirection": 0,
           "name": "sunny",
           "description": "Warm sunny weather, sometimes cloudy"
         }
        }
        """));

        Add(new("name property is absent", /*lang=json,strict*/ """
        {
         "date": "2024-08-12",
         "weather": {
           "temperature": 20.5,
           "windDirection": 0,
           "windSpeed": 70.2,
           "description": "Warm sunny weather, sometimes cloudy"
         }
        }
        """));
    }
}

internal sealed class JsonPayloadTestCaseItem : IXunitSerializable
{
    public string Description { get; private set; }
    public string Payload { get; private set; }

    public JsonPayloadTestCaseItem()
        : this(string.Empty, string.Empty)
    {
    }

    public JsonPayloadTestCaseItem(string description, string payload)
    {
        Description = description;
        Payload = payload;
    }

    public void Deserialize(IXunitSerializationInfo info)
    {
        Description = info.GetValue<string>(nameof(Description));
        Payload = info.GetValue<string>(nameof(Payload));
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(Description), Description);
        info.AddValue(nameof(Payload), Payload);
    }

    public override string ToString() => Description;
}
