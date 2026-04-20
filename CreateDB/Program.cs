public static class Program {
    public static void Main() {
        CreateMoviesTable();
        CreateAccountsTable();
        CreateRoomsTable();
        CreateTimetablesTable();
    }

    public static void CreateMoviesTable()
    {
        List<MovieModel> moviesList = [
            new MovieModel(1, "The Shawshank Redemption", 142, "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", "Frank Darabont", 15, "Drama", 1994),
            new MovieModel(2, "The Godfather", 175, "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", "Francis Ford Coppola", 18, "Crime, Drama", 1972),
            new MovieModel(3, "The Dark Knight", 152, "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.", "Christopher Nolan", 15, "Action, Crime, Drama", 2008)
        ];

        MoviesAccess movies = new();
        movies.CreateTable();

        foreach (MovieModel movie in moviesList)
        {
            movies.Write(movie);
        }
    }

    public static void CreateAccountsTable()
    {
        AccountsAccess accounts = new();
        accounts.CreateTable();

        List<AccountModel> accountsList = [
            new AccountModel(1, "john@example.com","demo_password" , "John Doe"),
            new AccountModel(2, "jane@example.com", "demo_password", "Jane Smith")
        ];

        foreach (AccountModel account in accountsList)
        {
            account.Password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(account.Password));
            accounts.Write(account);
        }
    }

    public static void CreateRoomsTable()
    {
        RoomsAccess rooms = new();
        rooms.CreateTable();

        List<RoomModel> roomsList = [
            new RoomModel(1, "Standard", "7.1 Surround Sound"),
            new RoomModel(2, "IMAX", "IMAX Sound System"),
            new RoomModel(3, "Dolby Cinema", "Dolby Atmos"),
        ];

        foreach (RoomModel room in roomsList)
        {
            rooms.Write(room);
        }
    }

    public static void CreateTimetablesTable()
    {
        TimetablesAccess timetables = new();
        timetables.CreateTable();

        // Movie 1
        DateTime date1 = new(2026, 1, 1, 13, 0, 0);
        DateTime date2 = new(2026, 2, 2, 14, 0, 0);
        DateTime date3 = new(2026, 3, 3, 15, 0, 0);
        DateTime date4 = new(2026, 5, 7, 15, 0, 0);
        DateTime date5 = new(2026, 5, 9, 15, 0, 0);
        DateTime date6 = new(2026, 5, 14, 15, 0, 0);

        // Movie 2
        DateTime date7 = new(2026, 1, 1, 15, 0, 0);
        DateTime date8 = new(2026, 5, 7, 15, 0, 0);
        DateTime date9 = new(2026, 5, 9, 15, 0, 0);
        DateTime date10 = new(2026, 5, 14, 15, 0, 0);
        DateTime date11 = new(2026, 5, 15, 15, 0, 0);
        DateTime date12 = new(2026, 5, 17, 15, 0, 0);

        // Movie 3
        DateTime date13 = new(2026, 5, 7, 15, 0, 0);
        DateTime date14 = new(2026, 5, 8, 15, 0, 0);
        DateTime date15 = new(2026, 5, 9, 15, 0, 0);
        DateTime date16 = new(2026, 5, 10, 15, 0, 0);
        DateTime date17 = new(2026, 5, 11, 15, 0, 0);
        DateTime date18 = new(2026, 5, 12, 15, 0, 0);

        List<TimetableModel> timetablesList = [
            // Movie 1
            new TimetableModel(1, 1, 1, TimetablesLogic.ConvertDateToUnixTime(date1)),
            new TimetableModel(2, 1, 1, TimetablesLogic.ConvertDateToUnixTime(date2)),
            new TimetableModel(3, 1, 1, TimetablesLogic.ConvertDateToUnixTime(date3)),
            new TimetableModel(4, 1, 1, TimetablesLogic.ConvertDateToUnixTime(date4)),
            new TimetableModel(5, 1, 1, TimetablesLogic.ConvertDateToUnixTime(date5)),
            new TimetableModel(6, 1, 1, TimetablesLogic.ConvertDateToUnixTime(date6)),

            // Movie 2
            new TimetableModel(7, 2, 2, TimetablesLogic.ConvertDateToUnixTime(date7)),
            new TimetableModel(8, 2, 2, TimetablesLogic.ConvertDateToUnixTime(date8)),
            new TimetableModel(9, 2, 2, TimetablesLogic.ConvertDateToUnixTime(date9)),
            new TimetableModel(10, 2, 2, TimetablesLogic.ConvertDateToUnixTime(date10)),
            new TimetableModel(11, 2, 2, TimetablesLogic.ConvertDateToUnixTime(date11)),
            new TimetableModel(12, 2, 2, TimetablesLogic.ConvertDateToUnixTime(date12)),

            // Movie 3
            new TimetableModel(13, 3, 3, TimetablesLogic.ConvertDateToUnixTime(date13)),
            new TimetableModel(14, 3, 3, TimetablesLogic.ConvertDateToUnixTime(date14)),
            new TimetableModel(15, 3, 3, TimetablesLogic.ConvertDateToUnixTime(date15)),
            new TimetableModel(16, 3, 3, TimetablesLogic.ConvertDateToUnixTime(date16)),
            new TimetableModel(17, 3, 3, TimetablesLogic.ConvertDateToUnixTime(date17)),
            new TimetableModel(18, 3, 3, TimetablesLogic.ConvertDateToUnixTime(date18)),
        ];

        foreach (TimetableModel timetable in timetablesList)
        {
            timetables.Write(timetable);
        }
    }
}