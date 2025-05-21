using OrisApi.Models.Inner;
using static OrisApi.Models.OrisEventServiceEntries;

namespace OrisApi.Models;

public class OrisEventServiceEntries : Dictionary<string, OrisEventServiceEntry>, IOrisResponseData
{
    public class OrisEventServiceEntry
    {
        public int ID { get; set; }

        public string RegNo { get; set; }

        public int? UserID { get; set; }

        public int? ClubUserID { get; set; }

        public int ClubID { get; set; }

        public OrisService Service { get; set; }

        public string? Note { get; set; }

        public int Quantity { get; set; }

        public int TotalFee { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? CreatedByUserID { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int? UpdatedByUserID { get; set; }
    }
}
