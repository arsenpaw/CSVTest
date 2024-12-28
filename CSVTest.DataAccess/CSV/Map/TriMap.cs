using CsvHelper.Configuration;
using CSVTest.DataAccess.Constants.Flags;
using CSVTest.DataAccess.Entities;
using System.Globalization;

namespace CSVTest.DataAccess.CSV.Map;


public sealed class TripMap : ClassMap<Trip>
{
    private static DateTime ConvertToDateTime(string dateTime, string format = "MM/dd/yyyy hh:mm:ss tt")
    {
        if (string.IsNullOrWhiteSpace(dateTime))
        {
            throw new ArgumentException("Invalid date string");
        }
        var customFormat = new DateTimeFormatInfo
        {
            AMDesignator = "AM",
            PMDesignator = "PM"
        };

        if (!DateTime.TryParseExact(dateTime, format, customFormat, System.Globalization.DateTimeStyles.None, out var parsedDate))
        {
            throw new FormatException($"Unable to parse date string: {dateTime}");
        }

        return parsedDate;
    }
    private decimal TryParseDecimal(string field)
    {
        field = field.Replace(',', '.');
        return decimal.TryParse(field, NumberStyles.Any, CultureInfo.InvariantCulture, out var value) ? value : 0m;
    }
    private static DateTime ConvertEstToUtc(DateTime dateTime)
    {
        var easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        return TimeZoneInfo.ConvertTimeToUtc(dateTime, easternTimeZone);
    }
    public TripMap()
    {
        Map(t => t.PickupDatetime).Name("tpep_pickup_datetime")
            .Convert(x => ConvertEstToUtc(ConvertToDateTime(x.Row.GetField("tpep_pickup_datetime")!)));
        Map(t => t.DropoffDatetime).Name("tpep_dropoff_datetime")
            .Convert(x => ConvertEstToUtc(ConvertToDateTime(x.Row.GetField("tpep_dropoff_datetime")!))); ;
        Map(t => t.StoreAndFwdFlag).Name("store_and_fwd_flag")
            .Convert(row => row.Row.GetField("store_and_fwd_flag") == "Y" ? FwdFlag.Yes : FwdFlag.No);
        Map(t => t.PuLocationId).Name("PULocationID");
        Map(t => t.DoLocationId).Name("DOLocationID");
        Map(t => t.TripDistance).Name("trip_distance");
        Map(t => t.PassengerCount)
            .Name("passenger_count")
            .Validate(x => !string.IsNullOrEmpty(x.Field))
            .Convert(row =>
            {
                var field = row.Row.GetField("passenger_count");
                return int.TryParse(field, out var value) ? value : 0;
            });
        Map(t => t.FareAmount)
          .Name("fare_amount");

        Map(t => t.TipAmount)
           .Name("tip_amount");

    }
}