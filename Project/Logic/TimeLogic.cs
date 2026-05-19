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

    public static DateTime ConvertStringToDateTime(string dateString)
    {
        return DateTime.Parse(dateString);
    }

    public static string ConvertDateString(DateTimeOffset dateTime, string format = "dd/MM/yyyy HH:mm:ss")
    {
        return dateTime.ToString(format);
    }

    [Obsolete("ConvertDateTimeOffsetToString is deprecated, please use ConvertDateString instead.")]
    public static string ConvertDateTimeOffsetToString(DateTimeOffset dateTime)
    {
        return ConvertDateString(dateTime);
    }

    [Obsolete("GetDateString is deprecated, please use ConvertDateString instead.")]
    public static string GetDateString(DateTimeOffset dateTime)
    {
        return ConvertDateString(dateTime, "dd-MM-yyyy");
    }

    [Obsolete("GetTimeString is deprecated, please use ConvertDateString instead.")]
    public static string GetTimeString(DateTimeOffset dateTime)
    {
        return ConvertDateString(dateTime, "HH:mm");
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
