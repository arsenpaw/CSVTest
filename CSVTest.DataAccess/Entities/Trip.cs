using CSVTest.DataAccess.Constants.Flags;
using Microsoft.EntityFrameworkCore;

namespace CSVTest.DataAccess.Entities;

public class Trip
{
    public Guid Id { get; set; }
    public DateTime PickupDatetime { get; set; }
    public DateTime DropoffDatetime { get; set; }
    public int PassengerCount { get; set; }
    public float TripDistance { get; set; }
    public FwdFlag StoreAndFwdFlag { get; set; }
    public int PuLocationId { get; set; }
    public int DoLocationId { get; set; }
    public float FareAmount { get; set; }
    public float TipAmount { get; set; }
}