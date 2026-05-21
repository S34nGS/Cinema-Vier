using System.Globalization;

public static class TimeLogic
{
    public static Int64 ConvertDateToUnixTime(DateTime dateTime)
    {
        return (Int64)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }

    public static DateTimeOffset ConvertUnixTimeToDateTime(Int64 unixTimestamp)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
    }

    public static DateTime ConvertStringToDateTime(string dateString, string format = "dd-MM-yyyy")
    {
        // return DateTime.Parse(dateString);
        return DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
    }

    public static string ConvertDateString(DateTimeOffset dateTime, string format = "dd/MM/yyyy HH:mm:ss")
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
}
