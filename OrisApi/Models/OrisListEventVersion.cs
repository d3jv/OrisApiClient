using System.Text.Json.Serialization;
using static OrisApi.Models.OrisListEventVersions;

namespace OrisApi.Models;

public class OrisListEventVersions : Dictionary<string, OrisListEventVersion>
{
    public class OrisListEventVersion
    {
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int ID { get; init; }

        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int Version { get; init; }
    }
}
