using static OrisApi.Models.OrisUserClubs;

namespace OrisApi.Models;

public class OrisUserClubs : Dictionary<string, OrisUserClub>
{
    public class OrisUserClub
    {
        public int ID { get; init; }

        public int ClubID { get; init; }

        public int UserID { get; init; }

        public DateTime MemberFrom { get; init; }

        public DateTime MemberTo { get; init; }

        public string RegNo { get; init; }
    }
}
