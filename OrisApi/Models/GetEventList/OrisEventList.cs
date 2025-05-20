using System.Text.Json.Serialization;
using OrisApi.JsonConverters;
using OrisApi.Models.GetEventList.Inner;
using static OrisApi.Models.GetEventList.OrisEventList;
using static OrisApi.Models.GetEventList.OrisEventList_Versions;

namespace OrisApi.Models.GetEventList;

public class OrisEventList : Dictionary<string, OrisEvent>, IOrisResponseData
{
    public class OrisEvent : OrisEvent_Versions
    {
        public string Name { get; set; }

        public DateOnly Date { get; set; }

        public OrisEventOrg Org1 { get; set; }

        [JsonConverter(typeof(RetardedOrisResponseDataConverter))]
        public OrisEventOrg? Org2 { get; set; }

        /// <summary>
        ///     Region abbreviations split by a comma
        /// </summary>
        public string Region { get; set; }

        [JsonConverter(typeof(RetardedOrisResponseDataConverter))]
        public Dictionary<string, OrisRegion> Regions { get; set; }

        public OrisSport Sport { get; set; }

        public OrisDiscipline Discipline { get; set; }

        public OrisLevel Level { get; set; }

        public DateTime? EntryStart { get; set; }

        public DateTime? EntryDate1 { get; set; }

        public DateTime? EntryDate2 { get; set; }

        public DateTime? EntryDate3 { get; set; }

        public bool Ranking { get; set; }

        public OrisSIType SIType { get; set; }

        public bool Cancelled { get; set; }

        public double GPSLat { get; set; }

        public double GPSLon { get; set; }

        public string Place { get; set; }

        public int ClassesLastModifiedTimeStamp { get; set; }

        public int ServicesLastModifiedTimeStamp { get; set; }

        public int ParentID { get; set; }

        public string Status { get; set; }

        public string? OBPostupy { get; set; }
    }
}
