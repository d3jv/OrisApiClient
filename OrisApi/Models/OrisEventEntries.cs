using static OrisApi.Models.OrisEventEntries;

namespace OrisApi.Models;

public class OrisEventEntries : Dictionary<string, OrisEventEntry>
{
    public class OrisEventEntry
    {
        public int ID { get; set; }

        public int ClassID { get; set; }

        public string ClassDesc { get; set; }

        public string RegNo { get; set; }

        public string Name { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public bool RentSI { get; set; }

        // This is entered by the user in oris and does not have
        // to be a number if the user is an idiot
        public string SI { get; set; }

        public string? Licence { get; set; }

        public string? RequestedStart { get; set; }

        public int? UserID { get; set; }

        public int? ClubUserID { get; set; }

        public int ClubID { get; set; }

        public string? Note { get; set; }

        public string Nationality { get; set; }

        public int? IOFID { get; set; }

        public int Fee { get; set; }

        public int EntryStop { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? CreatedByUserID { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int? UpdatedByUserID { get; set; }
    }
}
