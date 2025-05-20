using static OrisApi.Models.GetEventList.OrisEventList_Versions;

namespace OrisApi.Models.GetEventList;

public class OrisEventList_Versions : Dictionary<string, OrisEvent_Versions>, IOrisResponseData
{
    public class OrisEvent_Versions
    {
        public int ID { get; init; }

        public int Version { get; init; }
    }
}
