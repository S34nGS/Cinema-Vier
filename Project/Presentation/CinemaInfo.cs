public static class CinemaInfo
{
    public static void Start()
    {
        while (true)
        {
            Console.Clear();

            string[] menu = ["About Us", "Cinema Experience", "Events", "Prices", "Policies"];

            int selected = UiHelper.SelectionMenu(menu, "About Rotterdam Pathé Cinema");

            if (selected == Array.IndexOf(menu, "About Us"))
            {
                ShowAboutUs();
            }
            else if (selected == Array.IndexOf(menu, "Cinema Experience"))
            {
                ShowCinemaExperience();
            }
            else if (selected == Array.IndexOf(menu, "Events"))
            {
                ShowEvents();
            }
            else if (selected == Array.IndexOf(menu, "Prices"))
            {
                ShowPrices();
            }
            else if (selected == Array.IndexOf(menu, "Policies"))
            {
                ShowPolicies();
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
        Console.WriteLine("Rotterdam Pathé Cinema is a modern cinema located in Rotterdam.");
        Console.WriteLine("We offer a comfortable and simple movie experience.");
        Console.WriteLine("Users can browse movies, make reservations and enjoy their visit.");
        UiHelper.HoldUser();
    }

    static void ShowCinemaExperience()
    {
        Console.Clear();
        Console.WriteLine("=== Cinema Experience ===");
        Console.WriteLine();
        Console.WriteLine("Our cinema offers a comfortable viewing experience with modern screens and sound.");
        Console.WriteLine("Before the movie, visitors can stay in the waiting lounge.");
        Console.WriteLine("In the lounge, customers can buy popcorn and a variety of hot and cold drinks.");
        UiHelper.HoldUser();
    }

    static void ShowEvents()
    {
        Console.Clear();
        Console.WriteLine("=== Events ===");
        Console.WriteLine();
        Console.WriteLine("At the moment, we focus on providing a simple movie experience.");
        Console.WriteLine("In the future, special events or themed movie nights may be added.");
        UiHelper.HoldUser();
    }

    static void ShowPrices()
    {
        Console.Clear();
        Console.WriteLine("=== Prices ===");
        Console.WriteLine();
        Console.WriteLine("Ticket prices may vary depending on the movie and time.");
        Console.WriteLine("Food and drinks such as popcorn and beverages are sold separately.");
        UiHelper.HoldUser();
    }

    static void ShowPolicies()
    {
        Console.Clear();
        Console.WriteLine("=== Policies ===");
        Console.WriteLine();
        Console.WriteLine("Users must provide correct information when making a reservation.");
        Console.WriteLine("Payment must be completed before a reservation is confirmed.");
        Console.WriteLine("Customers should follow general cinema rules during their visit.");
        UiHelper.HoldUser();
    }
}