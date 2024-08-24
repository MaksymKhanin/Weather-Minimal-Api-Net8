using Xunit.Abstractions;

namespace Api.Tests;
internal sealed class BadRequestJsonTestCases : TheoryData<JsonPayloadTestCaseItem>
{
    public BadRequestJsonTestCases()
    {
        Add(new("date property is absent", /*lang=json,strict*/ """
        {
            "temperature": 20.5,
            "windDirection": 0,
            "windSpeed": 70.2,
            "name": "sunny",
            "description": "Warm sunny weather, sometimes cloudy"
        }
        """));

        Add(new("date property is empty", /*lang=json,strict*/ """
        {
            "date": "",
            "temperature": 20.5,
            "windDirection": 0,
            "windSpeed": 70.2,
            "name": "sunny",
            "description": "Warm sunny weather, sometimes cloudy"
        }
        """));

        Add(new("date property is null", /*lang=json,strict*/ """
        {
            "date": null,
            "temperature": 20.5,
            "windDirection": 0,
            "windSpeed": 70.2,
            "name": "sunny",
            "description": "Warm sunny weather, sometimes cloudy"
        }
        """));

        Add(new("temperature property is absent", /*lang=json,strict*/ """
        {
            "date": null,
            "windDirection": 0,
            "windSpeed": 70.2,
            "name": "sunny",
            "description": "Warm sunny weather, sometimes cloudy"
         }
        }
        """));

        Add(new("temperature property is empty", /*lang=json,strict*/ """
        {
            "date": "2024-08-12",
            "temperature": ,
            "windDirection": 0,
            "windSpeed": 70.2,
            "name": "sunny",
            "description": "Warm sunny weather, sometimes cloudy"
        }
        """));

        Add(new("temperature property is null", /*lang=json,strict*/ """
        {
            "date": "2024-08-12",
            "temperature": null,
            "windDirection": 0,
            "windSpeed": 70.2,
            "name": "sunny",
            "description": "Warm sunny weather, sometimes cloudy"
        }
        """));

        Add(new("windspeed property is absent", /*lang=json,strict*/ """
        {
            "date": "2024-08-12",
            "temperature": 20.5,
            "windDirection": 0,
            "name": "sunny",
            "description": "Warm sunny weather, sometimes cloudy"
        }
        """));

        Add(new("windspeed property is empty", /*lang=json,strict*/ """
        {
            "date": "2024-08-12",
            "temperature": 20.5,
            "windDirection": 0,
            "windSpeed": ,
            "name": "sunny",
            "description": "Warm sunny weather, sometimes cloudy"
        }
        """));

        Add(new("windspeed property is null", /*lang=json,strict*/ """
        {
            "date": "2024-08-12",
            "temperature": 20.5,
            "windDirection": 0,
            "windSpeed": null,
            "name": "sunny",
            "description": "Warm sunny weather, sometimes cloudy"
        }
        """));

        Add(new("name property is absent", /*lang=json,strict*/ """
        {
            "date": "2024-08-12",
            "temperature": 20.5,
            "windDirection": 0,
            "windSpeed": 70.2,
            "description": "Warm sunny weather, sometimes cloudy"
        }
        """));

        Add(new("name property is empty", /*lang=json,strict*/ """
        {
            "date": "2024-08-12",
            "temperature": 20.5,
            "windDirection": 0,
            "windSpeed": 70.2,
            "name": "",
            "description": "Warm sunny weather, sometimes cloudy"
        }
        """));

        Add(new("name property is null", /*lang=json,strict*/ """
        {
            "temperature": 20.5,
            "windDirection": 0,
            "windSpeed": 70.2,
            "name": null,
            "description": "Warm sunny weather, sometimes cloudy"
        }
        """));

        Add(new("description property is absent", /*lang=json,strict*/ """
        {
            "date": "2024-08-12",
            "temperature": 20.5,
            "windDirection": 0,
            "windSpeed": 70.2,
            "name": "sunny"
        }
        """));

        Add(new("description property is empty", /*lang=json,strict*/ """
        {
            "date": "2024-08-12",
            "temperature": 20.5,
            "windDirection": 0,
            "windSpeed": 70.2,
            "name": "sunny",
            "description": ""
        }
        """));

        Add(new("description property is null", /*lang=json,strict*/ """
        {
            "date": "2024-08-12",
            "temperature": 20.5,
            "windDirection": 0,
            "windSpeed": 70.2,
            "name": "sunny",
            "description": null
        }
        """));

        Add(new("winddirection property is empty", /*lang=json,strict*/ """
        {
            "date": "2024-08-12",
            "temperature": 20.5,
            "windDirection": ,
            "windSpeed": 70.2,
            "name": "sunny",
            "description": "Warm sunny weather, sometimes cloudy"
        }
        """));

        Add(new("winddirection property is null", /*lang=json,strict*/ """
        {
            "date": "2024-08-12",
            "temperature": 20.5,
            "windDirection": null,
            "windSpeed": 70.2,
            "name": "sunny",
            "description": "Warm sunny weather, sometimes cloudy"
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
