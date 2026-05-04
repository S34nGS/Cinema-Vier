public static class CinemaInfo
{
    public static void Start()
    {
        while (true)
        {
            Console.Clear();

            List<string> menu = ["About Us", "Cinema Experience", "Events", "Prices", "Policies", "General Information"];

            int selected = UiLib.SelectionMenu(menu, "About Cinema Vier");

            if (selected == menu.IndexOf("About Us"))
            {
                ShowAboutUs();
            }
            else if (selected == menu.IndexOf("Cinema Experience"))
            {
                ShowCinemaExperience();
            }
            else if (selected == menu.IndexOf("Events"))
            {
                ShowEvents();
            }
            else if (selected == menu.IndexOf("Prices"))
            {
                ShowPrices();
            }
            else if (selected == menu.IndexOf("Policies"))
            {
                ShowPolicies();
            }
            else if (selected == menu.IndexOf("General Information"))
            {
                ShowGeneralInformation();
            }
            else
            {
                // Back handled by UiLib
                Menu.Start();
                return;
            }
        }
    }

    static void ShowAboutUs()
    {
        Console.Clear();
        Console.WriteLine("=== About Us ===");
        Console.WriteLine();
        Console.WriteLine("Cinema Vier is a modern cinema located in Rotterdam.");
        Console.WriteLine("We offer a comfortable and simple movie experience.");
        Console.WriteLine("Users can browse movies, make reservations and enjoy their visit.");
        UiLib.HoldUser();
    }

    static void ShowCinemaExperience()
    {
        Console.Clear();
        Console.WriteLine("=== Cinema Experience ===");
        Console.WriteLine();
        Console.WriteLine("Our cinema offers a comfortable viewing experience with modern screens and sound.");
        Console.WriteLine("Before the movie, visitors can stay in the waiting lounge.");
        Console.WriteLine("In the lounge, customers can buy popcorn and a variety of hot and cold drinks.");
        UiLib.HoldUser();
    }

    static void ShowEvents()
    {
        Console.Clear();
        Console.WriteLine("=== Events ===");
        Console.WriteLine();
        Console.WriteLine("At the moment, we focus on providing a simple movie experience.");
        Console.WriteLine("In the future, special events or themed movie nights may be added.");
        UiLib.HoldUser();
    }

    static void ShowPrices()
    {
        Console.Clear();
        Console.WriteLine("=== Prices ===");
        Console.WriteLine();
        Console.WriteLine("Ticket prices may vary depending on the movie and time.");
        Console.WriteLine("Food and drinks such as popcorn and beverages are sold separately.");
        UiLib.HoldUser();
    }

    static void ShowPolicies()
    {
        Console.Clear();
        Console.WriteLine("=== Policies ===");
        Console.WriteLine();
        Console.WriteLine("Users must provide correct information when making a reservation.");
        Console.WriteLine("Payment must be completed before a reservation is confirmed.");
        Console.WriteLine("Customers should follow general cinema rules during their visit.");
        UiLib.HoldUser();
    }
    static void ShowGeneralInformation()
{
    Console.Clear();
    Console.WriteLine("=== General Information ===");
    Console.WriteLine();
    Console.WriteLine("Opening Hours:");
    Console.WriteLine("Monday - Sunday: 10:00 - 23:00");
    Console.WriteLine();
    Console.WriteLine("Address:");
    Console.WriteLine("Wijnhaven 107, 3011 WN Rotterdam, Netherlands");
    Console.WriteLine();
    Console.WriteLine("Phone: +31 10 123 4567");
    Console.WriteLine("Email: info@cinemavier.nl");
    UiLib.HoldUser();
}
}