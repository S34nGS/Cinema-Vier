public static class ViewReservations
{
    public static void Start()
    {
        // User must be logged in
        if (AccountsLogic.CurrentAccount is null)
        {
            Console.WriteLine("Please log in first to view your reservations.");
            UiHelper.HoldUser();
            return;
        }

        while (true)
        {
            string[] menu = ["Upcoming Orders", "Previous Orders"];
            int selected = UiHelper.SelectionMenu.WriteMenu(menu, "Reservations");

            if (selected == -1)
            {
                return;
            }

            if (selected == Array.IndexOf(menu, "Upcoming Orders"))
            {
                ShowFutureReservations();
            }
            else if (selected == Array.IndexOf(menu, "Previous Orders"))
            {
                return;
            }
        }
    }

    static void ShowFutureReservations()
    {
        long userId = AccountsLogic.CurrentAccount!.Id;
        List<ReservationModel> reservations = ReservationsLogic.GetFutureReservations(userId);

        reservations = reservations.OrderBy(reservation => reservation.ReservationDate).ToList();
        ShowReservationList(reservations, "Upcoming Orders");
    }

    static void ShowPastReservations()
    {
        long userId = AccountsLogic.CurrentAccount!.Id;
        List<ReservationModel> reservations = ReservationsLogic.GetPastReservations(userId);

        reservations = reservations.OrderBy(reservation => reservation.ReservationDate).ToList();
        ShowReservationList(reservations, "Previous Orders");
    }

    static void ShowReservationList(List<ReservationModel> reservations, string title)
    {
        Console.Clear();

        if (reservations.Count == 0)
        {
            Console.WriteLine($"No {title.ToLower()} found.");
            UiHelper.HoldUser();
            return;
        }

        List<string> reservationMenu = [];

        foreach (ReservationModel reservation in reservations)
        {
            DateTimeOffset date = TimeLogic.ConvertUnixTimeToDateTime(reservation.ReservationDate);
            TimetableModel timetable = TimetablesLogic.GetById(reservation.TimeTableId);
            MovieModel movie = MoviesLogic.GetById(timetable.MovieId);

            reservationMenu.Add($"{TimeLogic.ConvertDateString(date, "dd-MM-yyyy")} - {movie.Title}");
        }

        int selected = UiHelper.SelectionMenu.WriteMenu(reservationMenu.ToArray(), title);

        if (selected == -1)
        {
            return;
        }

        ShowReservationDetails(reservations[selected]);
    }

    static void ShowReservationDetails(ReservationModel reservation)
    {
        Console.Clear();

        DateTimeOffset date = TimeLogic.ConvertUnixTimeToDateTime(reservation.ReservationDate);
        TimetableModel timetable = TimetablesLogic.GetById(reservation.TimeTableId);
        MovieModel movie = MoviesLogic.GetById(timetable.MovieId);
        DateTimeOffset movieTime = TimeLogic.ConvertUnixTimeToDateTime(timetable.StartTime);
        RoomModel room = RoomsLogic.GetRoomById((int)timetable.RoomId);

        SeatReservationAccess seatReservationAccess = new();
        List<SeatModel> seats = seatReservationAccess.GetSeatsByReservationId(reservation.Id);

        Console.WriteLine("==================================");
        Console.WriteLine("      RESERVATION DETAILS");
        Console.WriteLine("==================================");
        Console.WriteLine();

        Console.WriteLine($"Reservation # : {reservation.Id}");
        Console.WriteLine($"Movie         : {movie.Title}");
        Console.WriteLine($"Date          : {TimeLogic.ConvertDateString(date, "dd-MM-yyyy")}");
        Console.WriteLine($"Time          : {TimeLogic.ConvertDateString(movieTime, "HH:mm")}");
        Console.WriteLine($"Room          : {room.ScreenType}");
        Console.WriteLine($"Total         : €{reservation.TotalPrice:F2}");

        Console.WriteLine();
        Console.WriteLine("Seats");
        Console.WriteLine("----------------------------------");

        if (seats.Count == 0)
        {
            Console.WriteLine("No seats reserved.");
        }
        else
        {
            foreach (SeatModel seat in seats)
            {
                var roomSeatInfo = SeatLogic.GetRoomSeatInfo(seat.RoomId);
                Int64 displayRow = roomSeatInfo.MaxRow - seat.Row + 1;

                Console.WriteLine($"Row {displayRow}, Seat {seat.SeatNumber}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("==================================");

        UiHelper.HoldUser();
    }
}