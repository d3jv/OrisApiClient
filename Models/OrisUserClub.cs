using System.Text.Json.Serialization;
using OrisApi.JsonConverters;

namespace OrisApi.Models;

public class OrisUserClub
{
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public int ClubID { get; init; }

    public string RegNo { get; init; }

    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime MemberFrom { get; init; }

    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime MemberTo { get; init; }

    [JsonIgnore]
    public string Club => RegNo.Substring(0, 3);
}
