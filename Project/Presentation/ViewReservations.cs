public static class ViewReservations
{
    public static void Start()
    {
        // User must be logged in
        if (AccountsLogic.CurrentAccount == null)
        {
            Console.WriteLine("Please log in first to view your reservations.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Menu.Start();
            return;
        }

        while (true)
        {
            List<string> menu = new() { "Future Reservations", "Past Reservations", "Back" };
            int selected = UiLib.SelectionMenu(menu, "Reservations");

            if (selected == menu.IndexOf("Future Reservations"))
            {
                ShowFutureReservations();
            }
            else if (selected == menu.IndexOf("Past Reservations"))
            {
                ShowPastReservations();
            }
            else if (selected == menu.IndexOf("Back"))
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

        int userId = (int)AccountsLogic.CurrentAccount!.Id;
        List<ReservationModel> reservations = ReservationsLogic.GetFutureReservations(userId);

        if (reservations.Count == 0)
        {
            Console.WriteLine("No future reservations found.");
        }
        else
        {
            foreach (ReservationModel reservation in reservations)
            {
                Console.WriteLine($"Date: {reservation.ReservationDate}");
                Console.WriteLine($"Price: {reservation.TotalPrice}");
                Console.WriteLine($"Timetable ID: {reservation.TimeTableId}");
                Console.WriteLine("----------------------------");
            }
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void ShowPastReservations()
    {
        Console.Clear();
        Console.WriteLine("=== Past Reservations ===");
        Console.WriteLine();

        int userId = (int)AccountsLogic.CurrentAccount!.Id;
        List<ReservationModel> reservations = ReservationsLogic.GetPastReservations(userId);

        if (reservations.Count == 0)
        {
            Console.WriteLine("No past reservations found.");
        }
        else
        {
            foreach (ReservationModel reservation in reservations)
            {
                Console.WriteLine($"Date: {reservation.ReservationDate}");
                Console.WriteLine($"Price: {reservation.TotalPrice}");
                Console.WriteLine($"Timetable ID: {reservation.TimeTableId}");
                Console.WriteLine("----------------------------");
            }
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}