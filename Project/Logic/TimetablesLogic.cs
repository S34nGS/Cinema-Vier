public static class TimetablesLogic
{
    private static TimetablesAccess _access = new();

    public static Int64 ConvertDateToUnixTime(DateTime dateTime)
    {
        return (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        
    }

    public static DateTimeOffset ConvertUnixTimeToDateTime(Int64 unixTimestamp)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
    }

    public static string ConvertDateTimeOffsetToString(DateTimeOffset dateTime)
    {
        return dateTime.ToString("dd/MM/yyyy HH:mm:ss");
    }

    public static string GetDateString(DateTimeOffset dateTime)
    {
        return dateTime.ToString("dd-MM-yyyy");
    }

    public static string GetTimeString(DateTimeOffset dateTime)
    {
        return dateTime.ToString("HH:mm");
    }

    public static string ConvertUnixTimeToString(Int64 unixTimestamp)
    {
        return ConvertDateTimeOffsetToString(ConvertUnixTimeToDateTime(unixTimestamp));
    }

    public static List<TimetableModel> GetTimeTablesByMovieId(Int64 movieId)
    {
        return _access.GetTimeTablesByMovieId(movieId);
    }

    public static DateTime ConvertUnixTimeToDateTimeValue(Int64 unixTimestamp)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).DateTime;
    }
}