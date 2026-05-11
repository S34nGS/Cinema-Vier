public static class ViewReservations
{
    public static void Start()
    {
        // User must be logged in
        if (AccountsLogic.CurrentAccount is null)
        {
            Console.WriteLine("Please log in first to view your reservations.");
            UiHelper.HoldUser();
            Menu.Start();
            return;
        }

        while (true)
        {
            List<string> menu = ["Future Reservations", "Past Reservations"];
            int selected = UiHelper.SelectionMenu(menu, "Reservations");

            if (selected == menu.IndexOf("Future Reservations"))
            {
                ShowFutureReservations();
            }
            else if (selected == menu.IndexOf("Past Reservations"))
            {
                ShowPastReservations();
            }
            else
            {
                Menu.Start();
                return;
            }
        }
    }
    static void ShowFutureReservations()
    {
        Console.Clear();
        Console.WriteLine("=== Future Reservations ===");
        Console.WriteLine();

        long userId = AccountsLogic.CurrentAccount!.Id;
        List<ReservationModel> reservations = ReservationsLogic.GetFutureReservations(userId);

        if (reservations.Count == 0)
        {
            Console.WriteLine("No future reservations found.");
        }
        else
        {
            foreach (ReservationModel reservation in reservations)
            {
                DateTimeOffset date = TimetablesLogic.ConvertUnixTimeToDateTime(reservation.ReservationDate);

                TimetableModel timetable = TimetablesLogic.GetById(reservation.TimeTableId);

                MovieModel movie = MoviesLogic.GetById(timetable.MovieId);

                DateTimeOffset movieTime = TimetablesLogic.ConvertUnixTimeToDateTime(timetable.StartTime);

                RoomModel room = RoomsLogic.GetRoomById((int)timetable.RoomId);

                Console.WriteLine($"Movie: {movie.Title}");
                Console.WriteLine($"Date: {TimetablesLogic.GetDateString(date)}");
                Console.WriteLine($"Time: {TimetablesLogic.GetTimeString(movieTime)}");
                Console.WriteLine($"Price: €{reservation.TotalPrice}");
                Console.WriteLine($"Room: {room.ScreenType}");
                Console.WriteLine("----------------------------");
            }
        }

        UiHelper.HoldUser();
    }

    static void ShowPastReservations()
    {
        Console.Clear();
        Console.WriteLine("=== Past Reservations ===");
        Console.WriteLine();

        long userId = AccountsLogic.CurrentAccount!.Id;
        List<ReservationModel> reservations = ReservationsLogic.GetPastReservations(userId);

        if (reservations.Count == 0)
        {
            Console.WriteLine("No past reservations found.");
        }
        else
        {
            foreach (ReservationModel reservation in reservations)
            {
                DateTimeOffset date = TimetablesLogic.ConvertUnixTimeToDateTime(reservation.ReservationDate);

                TimetableModel timetable = TimetablesLogic.GetById(reservation.TimeTableId);

                MovieModel movie = MoviesLogic.GetById(timetable.MovieId);

                DateTimeOffset movieTime = TimetablesLogic.ConvertUnixTimeToDateTime(timetable.StartTime);

                RoomModel room = RoomsLogic.GetRoomById((int)timetable.RoomId);

                Console.WriteLine($"Movie: {movie.Title}");
                Console.WriteLine($"Date: {TimetablesLogic.GetDateString(date)}");
                Console.WriteLine($"Time: {TimetablesLogic.GetTimeString(movieTime)}");
                Console.WriteLine($"Price: €{reservation.TotalPrice}");
                Console.WriteLine($"Room: {room.ScreenType}");
                Console.WriteLine("----------------------------");
            }
        }

        UiHelper.HoldUser();
    }
}