public static class ViewReservations
{
    public static void Start()
    {
        while (true)
        {
            List<string> menu = ["Upcoming Orders", "Previous Orders"];
            int selected = UiHelper.SelectionMenu.WriteMenu(menu, "Reservations");

            if (selected == menu.IndexOf("Upcoming Orders"))
            {
                ShowFutureReservations();
            }
            else if (selected == menu.IndexOf("Previous Orders"))
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
        Console.WriteLine("=== Upcoming Orders ===");
        Console.WriteLine();

        long userId = AccountsLogic.CurrentAccount!.Id;
        List<ReservationModel> reservations = ReservationsLogic.GetFutureReservations(userId);

        if (reservations.Count == 0)
        {
            Console.WriteLine("No upcoming orders found.");
        }
        else
        {
            foreach (ReservationModel reservation in reservations)
            {
                DateTimeOffset date = TimeLogic.ConvertUnixTimeToDateTime(reservation.ReservationDate);

                TimetableModel timetable = TimetablesLogic.GetById(reservation.TimeTableId);

                MovieModel movie = MoviesLogic.GetById(timetable.MovieId);

                DateTimeOffset movieTime = TimeLogic.ConvertUnixTimeToDateTime(timetable.StartTime);

                RoomModel room = RoomsLogic.GetRoomById((int)timetable.RoomId);

                Console.WriteLine($"Reservation Number: {reservation.Id}");
                Console.WriteLine($"Movie: {movie.Title}");
                Console.WriteLine($"Date: {TimeLogic.ConvertDateString(date, "dd-MM-yyyy")}");
                Console.WriteLine($"Time: {TimeLogic.ConvertDateString(movieTime, "HH:mm")}");
                Console.WriteLine($"Total amount: €{reservation.TotalPrice}");
                Console.WriteLine($"Room: {room.ScreenType}");
                Console.WriteLine("----------------------------");
            }
        }

        UiHelper.HoldUser();
    }

    static void ShowPastReservations()
    {
        Console.Clear();
        Console.WriteLine("=== Previous Orders ===");
        Console.WriteLine();

        long userId = AccountsLogic.CurrentAccount!.Id;
        List<ReservationModel> reservations = ReservationsLogic.GetPastReservations(userId);

        if (reservations.Count == 0)
        {
            Console.WriteLine("No previous orders found.");
        }
        else
        {
            foreach (ReservationModel reservation in reservations)
            {
                DateTimeOffset date = TimeLogic.ConvertUnixTimeToDateTime(reservation.ReservationDate);

                TimetableModel timetable = TimetablesLogic.GetById(reservation.TimeTableId);

                MovieModel movie = MoviesLogic.GetById(timetable.MovieId);

                DateTimeOffset movieTime = TimeLogic.ConvertUnixTimeToDateTime(timetable.StartTime);

                RoomModel room = RoomsLogic.GetRoomById((int)timetable.RoomId);

                Console.WriteLine($"Movie: {movie.Title}");
                Console.WriteLine($"Date: {TimeLogic.ConvertDateString(date, "dd-MM-yyyy")}");
                Console.WriteLine($"Time: {TimeLogic.ConvertDateString(movieTime, "HH:mm")}");
                Console.WriteLine($"Total amount: €{reservation.TotalPrice}");
                Console.WriteLine($"Room: {room.ScreenType}");
                Console.WriteLine("----------------------------");
            }
        }

        UiHelper.HoldUser();
    }
}
