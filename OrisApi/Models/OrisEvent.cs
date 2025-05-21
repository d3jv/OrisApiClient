using OrisApi.Models.GetEventList.Inner;

namespace OrisApi.Models;

// TODO: Add the rest of the fields
public class OrisEvent : IOrisResponseData
{
    public int ID { get; set; }

    public string Name { get; set; }

    public DateOnly Date { get; set; }

    public int EntryRentSIFee { get; set; }

    public bool Cancelled { get; set; }

    public Dictionary<string, OrisClass>? Classes { get; set; }

    public OrisLevel Level { get; set; }

    public class OrisClass
    {
        public int ID { get; set; }

        public int Fee { get; set; }
    }
}
