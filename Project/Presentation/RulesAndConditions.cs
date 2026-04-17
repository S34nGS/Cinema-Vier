static class RulesAndConditions
{
    public static void Start()
    {
        Console.Clear();

        Console.WriteLine("=== Rules and Conditions ===");
        Console.WriteLine();

        Console.WriteLine("1. General Rules");
        Console.WriteLine("- Users must provide correct information when making a purchase.");
        Console.WriteLine("- Users must follow cinema rules and staff instructions.");
        Console.WriteLine();

        Console.WriteLine("2. Reservations and Tickets");
        Console.WriteLine("- A reservation is only valid after payment is completed.");
        Console.WriteLine("- Users are responsible for checking their reservation details.");
        Console.WriteLine();

        Console.WriteLine("3. Financial Policies");
        Console.WriteLine("- Ticket prices may vary depending on the movie, room, or seat.");
        Console.WriteLine("- Payments must be made using the available payment methods in the app.");
        Console.WriteLine();

        Console.WriteLine("4. Legal Policies");
        Console.WriteLine("- Users must respect age restrictions for movies.");
        Console.WriteLine("- Personal information must not be used in an unlawful way.");
        Console.WriteLine();

        Console.WriteLine("Press any key to return...");
        Console.ReadKey();

        Menu.Start();
    }
}