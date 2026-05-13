public static class Program {
    public static void Main() {
        CreateMoviesTable();
        CreateAccountsTable();
        CreateRoomsTable();
        CreateTimetablesTable();
        CreateSeatsTable();
        CreateReservationTable();
        CreateTicketTable();
        CreateConsumableTable();
        CreateConsumableOrderTable();
        CreateMenuItemTable();
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
            new AccountModel(1, "john@example.com","demo_password" , "John", "Doe" , TimetablesLogic.ConvertDateToUnixTime(new DateTime(2000, 1, 1))),
            new AccountModel(2, "jane@example.com", "demo_password", "Jane", "Smith", TimetablesLogic.ConvertDateToUnixTime(new DateTime(2010, 1, 1))),
            new AccountModel(3, "admin@example.com", "demo_password", "Admin", "Admin", TimetablesLogic.ConvertDateToUnixTime(new DateTime(2000, 1, 1)), 1)
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
            new RoomModel(1, "Standard", "7.1 Surround Sound", 14, 12),
            new RoomModel(2, "IMAX", "IMAX Sound System", 20, 30),
            new RoomModel(3, "Dolby Cinema", "Dolby Atmos", 19, 18),
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
        
        DateTime baseDate = DateTime.Today;

        // Movie 1
        DateTime date1 = baseDate.AddDays(1).AddHours(13);
        DateTime date2 = baseDate.AddDays(2).AddHours(14);
        DateTime date3 = baseDate.AddDays(3).AddHours(15);
        DateTime date4 = baseDate.AddDays(7).AddHours(15);
        DateTime date5 = baseDate.AddDays(9).AddHours(15);
        DateTime date6 = baseDate.AddDays(14).AddHours(15);

        // Movie 2
        DateTime date7 = baseDate.AddDays(1).AddHours(15);
        DateTime date8 = baseDate.AddDays(2).AddHours(15);
        DateTime date9 = baseDate.AddDays(4).AddHours(15);
        DateTime date10 = baseDate.AddDays(5).AddHours(15);
        DateTime date11 = baseDate.AddDays(6).AddHours(15);
        DateTime date12 = baseDate.AddDays(8).AddHours(15);

        // Movie 3
        DateTime date13 = baseDate.AddDays(9).AddHours(15);
        DateTime date14 = baseDate.AddDays(10).AddHours(15);
        DateTime date15 = baseDate.AddDays(11).AddHours(15);
        DateTime date16 = baseDate.AddDays(12).AddHours(15);
        DateTime date17 = baseDate.AddDays(13).AddHours(15);
        DateTime date18 = baseDate.AddDays(14).AddHours(15);

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

    public static void CreateSeatsTable()
    {
        SeatAccess seats = new();
        seats.CreateTable();

        int small_theatre_height = 14;
        int small_theatre_width = 12;

        for (int y_axis = 0; y_axis < small_theatre_height; y_axis++)
        {
            for (int x_axis = 0; x_axis < small_theatre_width; x_axis++)
            {

                // public SeatModel(Int64 id, Int64 roomId, Int64 row, Int64 seatNumber, Int64 seatPriority)
                SeatModel seat = new(-1, 1, y_axis + 1, x_axis + 1, 1);
                seats.Write(seat);
            }
        }
    }

    public static void CreateReservationTable()
    {
        ReservationAccess reservation = new();
        reservation.CreateTable();

        List<ReservationModel> reservationList = [
            new ReservationModel(0, 1, TimetablesLogic.ConvertDateToUnixTime(new DateTime(2026, 4, 29)), 10.5, 1, 1),
            new ReservationModel(0, 1, TimetablesLogic.ConvertDateToUnixTime(new DateTime(2026, 4, 10)), 15.0, 2, 2),
            new ReservationModel(0, 2, TimetablesLogic.ConvertDateToUnixTime(new DateTime(2026, 4, 30)), 20.0, 3, 3),
            new ReservationModel(0, 2, TimetablesLogic.ConvertDateToUnixTime(new DateTime(2026, 4, 11)), 12.5, 1, 4),
        ];

        foreach (ReservationModel item in reservationList)
        {
            reservation.Write(item);
        }
    }

    public static void CreateTicketTable()
    {
        TicketAccess ticket = new();
        ticket.CreateTable();
    }

    public static void CreateConsumableTable()
    {
        ConsumableAccess consumable = new();
        consumable.CreateTable();
    }

    public static void CreateConsumableOrderTable()
    {
        ConsumableOrderAccess consumable = new();
        consumable.CreateTable();
    }

    public static void CreateMenuItemTable()
    {
        MenuItemsAccess menuItem = new();
        menuItem.CreateTable();

        // added default menu items in CreateDB
        List<MenuItemModel> menuItemsList = [
            new MenuItemModel(0, "Popcorn", "Snack", 2.00m),
            new MenuItemModel(0, "Nachos", "Snack", 3.50m),
            new MenuItemModel(0, "Chips", "Snack", 1.50m),
            new MenuItemModel(0, "Water", "Drink", 1.00m),
            new MenuItemModel(0, "Cola", "Drink", 2.00m),
            new MenuItemModel(0, "Juice", "Drink", 2.50m)
        ];

        foreach (MenuItemModel item in menuItemsList)
        {
            menuItem.Write(item);
        }
    }
}
