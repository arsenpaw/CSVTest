using Microsoft.EntityFrameworkCore;

namespace CSVTest.DataTransfer.Trips;

public class LocationTipAmountDto
{
    public int PuLocationId { get; set; }
    public float AvgTipAmount { get; set; }
}