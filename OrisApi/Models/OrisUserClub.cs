using System.Text.Json.Serialization;
using OrisApi.JsonConverters;
using static OrisApi.Models.OrisUserClubs;

namespace OrisApi.Models;

public class OrisUserClubs : Dictionary<string, OrisUserClub>
{
    public class OrisUserClub
    {
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int ID { get; init; }

        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int ClubID { get; init; }

        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int UserID { get; init; }

        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime MemberFrom { get; init; }

        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime MemberTo { get; init; }

        public string RegNo { get; init; }
    }
}
