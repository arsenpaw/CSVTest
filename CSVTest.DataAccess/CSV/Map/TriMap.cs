using System.Diagnostics;
using CsvHelper.Configuration;
using CSVTest.DataAccess.Constants.Flags;
using CSVTest.DataAccess.Entities;
using System.Globalization;
using System.Linq.Expressions;
using Microsoft.IdentityModel.Tokens;

namespace CSVTest.DataAccess.CSV.Map;


using CsvHelper.Configuration;
using CSVTest.DataAccess.Entities;

public sealed class TripMap : ClassMap<Trip>
{
    public TripMap()
    {
        MapWithValidation(t => t.PickupDatetime, "tpep_pickup_datetime", field => ConvertEstToUtc(ConvertToDateTime(field)));
        MapWithValidation(t => t.DropoffDatetime, "tpep_dropoff_datetime", field => ConvertEstToUtc(ConvertToDateTime(field)));
        MapWithValidation(t => t.StoreAndFwdFlag, "store_and_fwd_flag", field => field == "Y" ? FwdFlag.Yes : FwdFlag.No);
        MapWithValidation(t => t.PuLocationId, "PULocationID", field => int.Parse(field, CultureInfo.InvariantCulture));
        MapWithValidation(t => t.DoLocationId, "DOLocationID", field => int.Parse(field, CultureInfo.InvariantCulture));
        MapWithValidation(t => t.TripDistance, "trip_distance", field => float.Parse(field, CultureInfo.InvariantCulture));
        MapWithValidation(t => t.PassengerCount, "passenger_count", field => int.Parse(field, CultureInfo.InvariantCulture));
        MapWithValidation(t => t.FareAmount, "fare_amount", field => float.Parse(field, CultureInfo.InvariantCulture));
        MapWithValidation(t => t.TipAmount, "tip_amount", field => float.Parse(field, CultureInfo.InvariantCulture));
    }

    private void MapWithValidation<TProperty>(
        Expression<Func<Trip, TProperty>> member,
        string columnName,
        Func<string, TProperty> conversion)
    {
        Map(member)
            .Name(columnName)
            .Validate(row => !row.Field.IsNullOrEmpty())
            .Convert(row =>
            {
                var field = row.Row.GetField(columnName);
                
                if (string.IsNullOrWhiteSpace(field))
                {
                    Debug.WriteLine($"Validation failed for column: {columnName}, value: {field}");
                    return default(TProperty);
                }
                
                return conversion(field);
            });
    }

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

    private static DateTime ConvertEstToUtc(DateTime dateTime)
    {
        var easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        return TimeZoneInfo.ConvertTimeToUtc(dateTime, easternTimeZone);
    }
}
