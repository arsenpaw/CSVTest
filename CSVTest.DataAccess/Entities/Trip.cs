using CSVTest.DataAccess.Constants.Flags;

namespace CSVTest.DataAccess.Entities;

public class Trip
{
    public Guid Id { get; set; }
    public DateTime PickupDatetime { get; set; }
    public DateTime DropoffDatetime { get; set; }
    public int PassengerCount { get; set; }
    public double TripDistance { get; set; }
    public FwdFlag StoreAndFwdFlag { get; set; }
    public int PuLocationId { get; set; }
    public int DoLocationId { get; set; }
    public decimal FareAmount { get; set; }
    public decimal TipAmount { get; set; }
}