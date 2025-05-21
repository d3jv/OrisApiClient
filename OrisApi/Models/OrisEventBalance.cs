using System.Text.Json.Serialization;
using OrisApi.JsonConverters;

namespace OrisApi.Models;

public class OrisEventBalance
{
    public int EventID { get; set; }

    public string Currency { get; set; }

    [JsonConverter(typeof(IamFuckingDoneWithThisJsonConverterFactory))]
    public Dictionary<string, OrisClubBalance> Clubs { get; set; }

    public class OrisClubBalance
    {
        public int ClubID { get; set; }

        public string ClubAbbr { get; set; }

        public int FeeEntry { get; set; }

        public int FeeSI { get; set; }

        public int FeeService { get; set; }

        public int FeeTotal { get; set; }

        public int Paid { get; set; }

        public int ToBePaid { get; set; }
    }
}
