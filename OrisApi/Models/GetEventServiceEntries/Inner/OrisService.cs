namespace OrisApi.Models.Inner;

public class OrisService
{
    public int ID { get; set; }

    public string NameCZ { get; set; }

    public string? NameEN { get; set; }

    public DateTime? LastBookingDateTime { get; set; }

    public double UnitPrice { get; set; }

    public int QtyAvailable { get; set; }

    public int QtyAlreadyOrdered { get; set; }

    public int QtyRemaining { get; set; }
}
