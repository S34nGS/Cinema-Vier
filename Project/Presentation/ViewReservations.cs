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
            List<string> menu = ["Upcoming Orders", "Previous Orders"];
            int selected = UiHelper.SelectionMenu(menu, "Reservations");

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
                return;
            }
        }
    }
    static void ShowFutureReservations()
    {
        long userId = AccountsLogic.CurrentAccount!.Id;
        List<ReservationModel> reservations = ReservationsLogic.GetFutureReservations(userId);

        // sort reservations from old date to new date
        reservations = reservations.OrderBy(reservation => reservation.ReservationDate).ToList();
        ShowReservationList(reservations, "Upcoming Orders");
    }

    static void ShowPastReservations()
    {
        long userId = AccountsLogic.CurrentAccount!.Id;
        List<ReservationModel> reservations = ReservationsLogic.GetPastReservations(userId);

        // sort reservations from old date to new date
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
            DateTimeOffset date = TimetablesLogic.ConvertUnixTimeToDateTime(reservation.ReservationDate);
            TimetableModel timetable = TimetablesLogic.GetById(reservation.TimeTableId);
            MovieModel movie = MoviesLogic.GetById(timetable.MovieId);

            // show short information first
            reservationMenu.Add($"{TimetablesLogic.GetDateString(date)} - {movie.Title}");
        }

        int selected = UiHelper.SelectionMenu(reservationMenu, title);

        if (selected == -1)
        {
            return;
        }

        ShowReservationDetails(reservations[selected]);
    }

    static void ShowReservationDetails(ReservationModel reservation)
    {
        Console.Clear();

        // get all information for this reservation
        DateTimeOffset date = TimetablesLogic.ConvertUnixTimeToDateTime(reservation.ReservationDate);
        TimetableModel timetable = TimetablesLogic.GetById(reservation.TimeTableId);
        MovieModel movie = MoviesLogic.GetById(timetable.MovieId);
        DateTimeOffset movieTime = TimetablesLogic.ConvertUnixTimeToDateTime(timetable.StartTime);
        RoomModel room = RoomsLogic.GetRoomById((int)timetable.RoomId);

        SeatReservationAccess seatReservationAccess = new();
        List<SeatModel> seats = seatReservationAccess.GetSeatsByReservationId(reservation.Id);

        Console.WriteLine("==================================");
        Console.WriteLine("      RESERVATION DETAILS");
        Console.WriteLine("==================================");
        Console.WriteLine();
    
        Console.WriteLine($"Reservation # : {reservation.Id}");
        Console.WriteLine($"Movie         : {movie.Title}");
        Console.WriteLine($"Date          : {TimetablesLogic.GetDateString(date)}");
        Console.WriteLine($"Time          : {TimetablesLogic.GetTimeString(movieTime)}");
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