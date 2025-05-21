using static OrisApi.Models.GetEventList.OrisEventList_ClubEntries;

namespace OrisApi.Models.GetEventList;

public class OrisEventList_ClubEntries : Dictionary<string, OrisEvent_ClubEntries>
{
    public class OrisEvent_ClubEntries : OrisEvent
    {
        public int ClubEntryCount { get; set; }

        public int? ClubEntryLastModifiedTimeStamp { get; set; }

        public int ClubServiceEntryCount { get; set; }

        public int? ClubServiceEntryLastModifiedTimeStamp { get; set; }

        public int ClubStartCount { get; set; }

        public int ClubResultCount { get; set; }
    }
}
