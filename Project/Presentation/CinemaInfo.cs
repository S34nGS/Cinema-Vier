public static class CinemaInfo
{
    public static void Start()
    {
        while (true)
        {
            Console.Clear();

            List<string> menu = ["About Us", "Cinema Experience", "Events", "Prices", "Policies", "General Information"];

            int selected = UiHelper.SelectionMenu.WriteMenu(menu, "About Cinema Vier");

            if (selected == -1)
            {
                return;
            }

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
                // Back handled by UiHelper
                return;
            }
        }
    }

    public readonly static Action ShowAboutUs = UiHelper.GeneratePage(@"
=== About Us ===

Cinema Vier is a modern cinema located in Rotterdam.
We offer a comfortable and simple movie experience.
Users can browse movies, make reservations and enjoy their visit.
");

    public readonly static Action ShowCinemaExperience = UiHelper.GeneratePage(@"
=== Cinema Experience ===

Our cinema offers a comfortable viewing experience with modern screens and sound.
Before the movie, visitors can stay in the waiting lounge.
In the lounge, customers can buy popcorn and a variety of hot and cold drinks.
    ");

    public readonly static Action ShowEvents = UiHelper.GeneratePage(@"
=== Events ===

At the moment, we focus on providing a simple movie experience.
In the future, special events or themed movie nights may be added.
    ");

    public readonly static Action ShowPrices = UiHelper.GeneratePage(@"
=== Prices ===

Ticket prices may vary depending on the movie and time.
Food and drinks such as popcorn and beverages are sold separately.
    ");

    public readonly static Action ShowPolicies = UiHelper.GeneratePage(@"
=== Policies ===

Users must provide correct information when making a reservation.
Payment must be completed before a reservation is confirmed.
Customers should follow general cinema rules during their visit.
    ");

    public readonly static Action ShowGeneralInformation = UiHelper.GeneratePage(@"
=== General Information ===

Opening Hours:

Monday - Sunday: 10:00 - 23:00

Address:

Wijnhaven 107, 3011 WN Rotterdam, Netherlands

Phone: +31 10 123 4567

Email: info@cinemavier.nl
    ");
}
