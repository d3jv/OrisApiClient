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

        public double FeeEntry { get; set; }

        public double FeeSI { get; set; }

        public double FeeService { get; set; }

        public double FeeTotal { get; set; }

        public double Paid { get; set; }

        public double ToBePaid { get; set; }
    }
}
