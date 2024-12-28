using CsvHelper.Configuration;
using CSVTest.DataAccess.Constants.Flags;
using CSVTest.DataAccess.Entities;

namespace CSVTest.DataAccess.CSV.Map;


public sealed class TripMap : ClassMap<Trip>
{
    private static DateTime ConvertToDateTime(string dateTime, string format = "yyyy-MM-dd HH:mm:ss")
    {
        if (string.IsNullOrWhiteSpace(dateTime))
            throw new ArgumentException("Invalid date string");

        return DateTime.ParseExact(dateTime, format, null);
    }
    private  static DateTime ConvertEstToUtc(DateTime dateTime)
    {
        return TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
    }
    public TripMap()
    {
        Map(t => t.PickupDatetime).Name("tpep_pickup_datetime")
            .Convert(x => ConvertEstToUtc(ConvertToDateTime(x.Row.GetField("tpep_pickup_datetime")!)));
        Map(t => t.DropoffDatetime).Name("tpep_dropoff_datetime")
            .Convert(x => ConvertEstToUtc(ConvertToDateTime(x.Row.GetField("tpep_dropoff_datetime")!)));;
        Map(t => t.PassengerCount).Name("passenger_count");
        Map(t => t.TripDistance).Name("trip_distance");
        Map(t => t.StoreAndFwdFlag).Name("store_and_fwd_flag")
            .Convert(row => row.Row.GetField("store_and_fwd_flag") == "Y" ? FwdFlag.Yes : FwdFlag.No);
        Map(t => t.PuLocationId).Name("PULocationID");
        Map(t => t.DoLocationId).Name("DOLocationID");
        Map(t => t.FareAmount).Name("fare_amount");
        Map(t => t.TipAmount).Name("tip_amount");
    }
}