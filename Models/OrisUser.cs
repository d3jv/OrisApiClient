using System.Text.Json.Serialization;

namespace OrisApi.Models;

public class OrisUser
{
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int ID { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string RefLicenceOB { get; init; }
}

