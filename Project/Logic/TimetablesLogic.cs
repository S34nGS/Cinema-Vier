public static class TimetablesLogic
{
    private static TimetablesAccess _access = new();
    public static List<TimetableModel>? CurrentTimeTables { get; set; }

    public static List<TimetableModel> GetTimeTablesByMovieId(Int64 movieId)
    {
        return _access.GetTimeTablesByMovieId(movieId);
    }

    public static RoomModel? GetRoomByTimetableId(Int64 timetableId)
    {
        return _access.GetRoomByTimetableId(timetableId);
    }


    public static TimetableModel? GetById(Int64 timetableId)
    {
        return _access.GetById(timetableId);
    }

    public static List<TimetableModel> GetTimetablesByDate(string dateString)
    {
        DateTime date = TimeLogic.ConvertStringToDateTime(dateString);
        Int64 startUnixTime = TimeLogic.ConvertDateToUnixTime(date.Date);
        Int64 endUnixTime = TimeLogic.ConvertDateToUnixTime(date.Date.AddDays(1)) - 1;

        return _access.GetTimetablesByDateRange(startUnixTime, endUnixTime);
    }

    public static List<TimetableModel> GetTimetablesByDateRange(DateTime startDate, DateTime endDate)
    {
        Int64 startUnixTime = TimeLogic.ConvertDateToUnixTime(startDate.Date);
        Int64 endUnixTime = TimeLogic.ConvertDateToUnixTime(endDate.Date);

        return _access.GetTimetablesByDateRange(startUnixTime, endUnixTime);
    }
}
