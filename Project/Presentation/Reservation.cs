// This class handles the user interface for creating a reservation
public static class Reservation
{
    public static void Start()
    {
        // Check if user is logged in
        if (AccountsLogic.CurrentAccount == null)
        {
            Console.WriteLine("Please log in first before making a reservation.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Menu.Start();
            return;
        }

        // Clear screen and show title
        Console.Clear();
        Console.WriteLine("=== Create Reservation ===");
        Console.WriteLine();

        // Show current logged-in user
        Console.WriteLine("Logged in as: " + AccountsLogic.CurrentAccount.FullName);
        Console.WriteLine();

        // Ask user for reservation date
        Console.Write("Enter reservation date (example: 2026-04-07): ");
        string? reservationDate = Console.ReadLine();

        // Ask user for total price
        Console.Write("Enter total price: ");
        string? totalPriceInput = Console.ReadLine();

        // Ask user for timetable id
        Console.Write("Enter timetable id: ");
        string? timeTableIdInput = Console.ReadLine();

        // Validate total price input
        if (!double.TryParse(totalPriceInput, out double totalPrice))
        {
            Console.WriteLine("Invalid total price.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        // Validate timetable id input
        if (!int.TryParse(timeTableIdInput, out int timeTableId))
        {
            Console.WriteLine("Invalid timetable id.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        // Get user id from current logged-in account
        int userId = (int)AccountsLogic.CurrentAccount.Id;

        // Call logic layer to create reservation
        bool success = ReservationsLogic.CreateReservation(userId, reservationDate!, totalPrice, timeTableId);

        Console.WriteLine();

        // Show result message
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