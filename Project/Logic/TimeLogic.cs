using System.Globalization;

public static class TimeLogic
{
    public static Int64 ConvertDateToUnixTime(this DateTime dateTime)
    {
        return (Int64)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }

    public static DateTimeOffset ConvertUnixTimeToDateTime(this Int64 unixTimestamp)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
    }

    public static DateTime ConvertStringToDateTime(string dateString, string format = "dd-MM-yyyy")
    {
        return DateTime.ParseExact(dateString, format, null);
    }

    public static string ConvertDateString(this DateTimeOffset dateTime, string format = "dd/MM/yyyy HH:mm:ss")
    {
        return dateTime.ToString(format);
    }

    public static string ConvertUnixTimeToString(Int64 unixTimestamp)
    {
        return ConvertDateString(ConvertUnixTimeToDateTime(unixTimestamp));
    }

    public static DateTime ConvertUnixTimeToDateTimeValue(Int64 unixTimestamp)
    {
        return DateTimeOffset
            .FromUnixTimeSeconds(unixTimestamp)
            .DateTime;
    }

    public static bool ValidateDateStringAfterToday(string date)
    {
        if (!DateTime.TryParseExact(
            date,
            "dd-MM-yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime parsedDate))
        {
            return false;
        }

        return parsedDate.Date >= DateTime.Today;
    }
}
