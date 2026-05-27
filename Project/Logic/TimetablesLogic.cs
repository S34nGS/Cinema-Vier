using System.Globalization;

public static class TimetablesLogic
{
    private static TimetablesAccess _access = new();


    // Conversion methods
    public static Int64 ConvertDateToUnixTime(DateTime dateTime)
    {
        return (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }

    public static string ConvertDateTimeOffsetToString(DateTimeOffset dateTime)
    {
        return dateTime.ToString("dd/MM/yyyy HH:mm:ss");
    }

    public static DateTimeOffset ConvertUnixTimeToDateTime(Int64 unixTimestamp)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
    }

   public static DateTime ConvertUnixTimeToDateTimeValue(Int64 unixTimestamp)
    {
        return DateTimeOffset
            .FromUnixTimeSeconds(unixTimestamp)
            .DateTime;
    }

    public static string ConvertUnixTimeToString(Int64 unixTimestamp)
    {
        return ConvertDateTimeOffsetToString(ConvertUnixTimeToDateTime(unixTimestamp));
    }

    public static Int64 ConvertTimeStringToUnixTime(string time)
    {
        int hour = int.Parse(time[..2]) * 3600;
        int minute = int.Parse(time[3..]) * 60;
        return hour + minute;
    }

    public static DateTime ConvertStringToDateTime(string dateString)
    {
        return DateTime.Parse(dateString);
    }


    // Getting methods
    public static List<TimetableModel> GetTimeTablesByMovieId(Int64 movieId)
    {
        return _access.GetTimeTablesByMovieId(movieId);
    }

    public static RoomModel GetRoomByTimetableId(Int64 timetableId)
    {
        return _access.GetRoomByTimetableId(timetableId);
    }

    public static TimetableModel GetById(Int64 timetableId)
    {
        return _access.GetById(timetableId);
    }

    public static List<TimetableModel> GetTimetablesByDate(string dateString)
    {
        DateTime date = ConvertStringToDateTime(dateString);
        Int64 startUnixTime = ConvertDateToUnixTime(date.Date);
        Int64 endUnixTime = ConvertDateToUnixTime(date.Date.AddDays(1)) - 1;
        
        return _access.GetTimetablesByDateRange(startUnixTime, endUnixTime);
    }

    public static List<TimetableModel> GetTimetablesByDateRange(DateTime startDate, DateTime endDate)
    {
        Int64 startUnixTime = ConvertDateToUnixTime(startDate.Date);
        Int64 endUnixTime = ConvertDateToUnixTime(endDate.Date);
        
        return _access.GetTimetablesByDateRange(startUnixTime, endUnixTime);
    }

    public static List<TimetableModel> GetAllTimetablesFromToday()
    {
        Int64 today = ConvertDateToUnixTime(DateTime.Today);

        return _access.GetAllTimetablesFromDate(today);
    }

    public static List<TimetableModel> GetSpecificTimetablesFromToday(Int64 movieId)
    {
        Int64 today = ConvertDateToUnixTime(DateTime.Today);

        return _access.GetSpecificTimetablesFromDate(today, movieId);
    }

    public static string GetDateString(DateTimeOffset dateTime)
    {
        return dateTime.ToString("dd-MM-yyyy");
    }

    public static string GetTimeString(DateTimeOffset dateTime)
    {
        return dateTime.ToString("HH:mm");
    }

    public static List<string> GetDetailsAsList(TimetableModel timetable)
    {
        string movie = MoviesLogic.GetById(timetable.MovieId).Title;
        string date = GetDateString(ConvertUnixTimeToDateTime(timetable.StartTime));
        string time = GetTimeString(ConvertUnixTimeToDateTime(timetable.StartTime));
        return new List<string> 
        {
            timetable.RoomId.ToString(),
            date,
            time
        };
    }


    // Validation methods
    public static bool ValidateTitleString(string title)
    {
        if (MoviesLogic.GetMovieByTitle(title) == null)
        {
            return false;
        }
        return true;
    }

    public static bool ValidateRoomNumberString(string roomNumber)
    {
        if (RoomsLogic.GetRoomById(int.Parse(roomNumber)) == null)
        {
            return false;
        }
        return true;
    }

    public static bool ValidateDateString(string date)
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

    public static bool ValidateTimeString(string time)
    {
        if (time.Length != 5 || time[2] != ':')
            return false;

        if (!int.TryParse(time[..2], out int hour) || !int.TryParse(time[3..], out int minute))
            return false;

        return hour > 0 && hour < 24 && minute >= 0 && minute < 60;
    }

    public static bool ValidateExistingTimetable(TimetableModel newTimetable)
    {
        DateTime today = DateTime.Today;
        List<TimetableModel> timetables = GetTimetablesByDateRange(today, today.AddDays(14));
        foreach (var timetable in timetables)
        {
            Int64 movie_plus_duration = timetable.StartTime + MoviesLogic.GetById(timetable.MovieId).Duration * 60;

            if (
                timetable.RoomId == newTimetable.RoomId
                && newTimetable.StartTime >= timetable.StartTime
                && newTimetable.StartTime < movie_plus_duration
                )
            {
                return false;
            }
        }

        return true;
    }


    // CRUD methods
    public static void AddTimetable(TimetableModel timetable)
    {
        _access.Write(timetable);
    }

    public static void EditTimetable(TimetableModel timetable)
    {
        _access.Update(timetable);
    }


    // Admin methods
    public static (int, MovieModel) ManageTimetables(string movieMenuHeader, List<string> menu)
    {
        MovieModel movie = MoviesLogic.PickMovieToManage(movieMenuHeader);
        int selected = UiHelper.SelectionMenu(menu, movie.Title);

        if (selected >= 0 && selected < menu.Count)
            return (selected, movie);

        return (-1, movie);
    }

    public static int CreateTimetableAsAdmin(MovieModel movie, string roomNumber, string date, string time)
    {
        if (!ValidateRoomNumberString(roomNumber))
        {
            return 1;
        }
        else if (!ValidateDateString(date))
        {
            return 2;
        }
        else if (!ValidateTimeString(time))
        {
            return 3;
        }

        Int64 unixDate = ConvertDateToUnixTime(ConvertStringToDateTime(date));
        Int64 unixTime = ConvertTimeStringToUnixTime(time);
        Int64 startTime = unixDate + unixTime;

        TimetableModel timetable = new(
            -1,
            movie.Id,
            int.Parse(roomNumber),
            startTime
        );

        if (!ValidateExistingTimetable(timetable))
        {
            return 4;
        }

        AddTimetable(timetable);

        return 0;
    }

    public static TimetableModel ChooseTimeTableAsAdmin(string header, MovieModel movie)
    {
        List<TimetableModel> timetables = GetSpecificTimetablesFromToday(movie.Id);
        List<string> timetableIds = [];
        foreach (TimetableModel timetable in timetables)
        {
            if (timetable.IsActive)
            {
                string id = timetable.Id.ToString();
                string date = GetDateString(ConvertUnixTimeToDateTime(timetable.StartTime));
                string time = GetTimeString(ConvertUnixTimeToDateTime(timetable.StartTime));
                timetableIds.Add($"Timetable ID: {id}, Date: {date}, Time: {time}");
            }
        }
        int selected = UiHelper.SelectionMenu(timetableIds, header);

        return timetables[selected];
    }

    public static int EditTimeTableAsAdmin(Int64 id, MovieModel movie, string roomNumber, string date, string time)
    {
        if (!ValidateRoomNumberString(roomNumber))
        {
            return 1;
        }
        else if (!ValidateDateString(date))
        {
            return 2;
        }
        else if (!ValidateTimeString(time))
        {
            return 3;
        }

        Int64 unixDate = ConvertDateToUnixTime(ConvertStringToDateTime(date));
        Int64 unixTime = ConvertTimeStringToUnixTime(time);
        Int64 startTime = unixDate + unixTime;

        TimetableModel timetable = new(
            id,
            movie.Id,
            int.Parse(roomNumber),
            startTime
        );

        EditTimetable(timetable);

        return 0;
        //TODO When entering wrong info don't exit the inputmenu but let me fix the info
    }

    public static int DeleteTimetableAsAdmin(TimetableModel timetable, List<string> deletionMenuOptions, string deletionMenuHeader)
    {
        int selected = UiHelper.SelectionMenu(deletionMenuOptions, deletionMenuHeader);

        if (selected == 0)
        {
            timetable.IsActive = false;
            _access.Update(timetable);
            return selected;
        }

        return 1;
    }
}