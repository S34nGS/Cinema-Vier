public static class Reservation
{
    public static void Start()
    {
        if (AccountsLogic.CurrentAccount == null)
        {
            Console.WriteLine("Please log in first before making a reservation.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Menu.Start();
            return;
        }

        Console.Clear();
        Console.WriteLine("=== Create Reservation ===");
        Console.WriteLine();

        Console.WriteLine("Logged in as: " + AccountsLogic.CurrentAccount.FullName);
        Console.WriteLine();

        Console.Write("Enter reservation date (example: 2026-04-07): ");
        string? reservationDate = Console.ReadLine();

        Console.Write("Enter total price: ");
        string? totalPriceInput = Console.ReadLine();

        Console.Write("Enter timetable id: ");
        string? timeTableIdInput = Console.ReadLine();

        if (!double.TryParse(totalPriceInput, out double totalPrice))
        {
            Console.WriteLine("Invalid total price.");
            Console.ReadKey();
            return;
        }

        if (!int.TryParse(timeTableIdInput, out int timeTableId))
        {
            Console.WriteLine("Invalid timetable id.");
            Console.ReadKey();
            return;
        }

        int userId = (int)AccountsLogic.CurrentAccount.Id;

        bool success = ReservationsLogic.CreateReservation(userId, reservationDate!, totalPrice, timeTableId);

        Console.WriteLine();

        if (success)
        {
            Console.WriteLine("Reservation created successfully.");
        }
        else
        {
            Console.WriteLine("Failed to create reservation.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}