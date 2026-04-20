public static class TimetablesLogic
{
    private static TimetablesAccess _access = new();

    public static Int64 ConvertDateToUnixTime(DateTime dateTime)
    {
        Int64 unixTimestamp = (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        return unixTimestamp;
    }

    public static DateTimeOffset ConvertUnixTimeToDateTime(Int64 unixTimestamp)
    {
        DateTimeOffset dateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
        return dateTime;
    }

    public static string ConvertDateTimeOffsetToString(DateTimeOffset dateTime)
    {
        string date_str = dateTime.ToString("dd/MM/yyyy HH:mm:ss");
        return date_str;
    }

    public static string GetDateString(DateTimeOffset dateTime)
    {
        string date_str = dateTime.ToString("dd/MM/yyyy");
        return date_str;
    }

    public static string GetTimeString(DateTimeOffset dateTime)
    {
        string time_str = dateTime.ToString("HH:mm:ss");
        return time_str;
    }

    public static string ConvertUnixTimeToString(Int64 unixTimestamp)
    {
        return ConvertDateTimeOffsetToString(ConvertUnixTimeToDateTime(unixTimestamp));
    }

    public static List<TimetableModel> GetTimeTablesByMovieId(Int64 movieId)
    {
        List<TimetableModel> timetables = _access.GetTimeTablesByMovieId(movieId);
        return timetables;
    }
}