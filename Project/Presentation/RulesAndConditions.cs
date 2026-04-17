static class RulesAndConditions
{
    public static void Start()
    {
        while (true)
        {
            Console.Clear();

            List<string> menu = new()
            {
                "General Rules",
                "Reservations and Tickets",
                "Financial Policies",
                "Legal Policies",
                "Back"
            };

            int selected = UiLib.SelectionMenu(menu, "=== Rules and Conditions ===");

            if (selected == menu.IndexOf("General Rules"))
            {
                ShowGeneralRules();
            }
            else if (selected == menu.IndexOf("Reservations and Tickets"))
            {
                ShowReservationsAndTickets();
            }
            else if (selected == menu.IndexOf("Financial Policies"))
            {
                ShowFinancialPolicies();
            }
            else if (selected == menu.IndexOf("Legal Policies"))
            {
                ShowLegalPolicies();
            }
            else if (selected == menu.IndexOf("Back"))
            {
                Menu.Start();
                return;
            }
        }
    }

    static void ShowGeneralRules()
    {
        Console.Clear();
        Console.WriteLine("=== General Rules ===");
        Console.WriteLine();
        Console.WriteLine("- Users must provide correct information when using the app.");
        Console.WriteLine("- Users must follow the cinema rules and staff instructions.");
        Console.WriteLine("- Users should arrive on time for their movie.");
        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    static void ShowReservationsAndTickets()
    {
        Console.Clear();
        Console.WriteLine("=== Reservations and Tickets ===");
        Console.WriteLine();
        Console.WriteLine("- A reservation is only valid after successful payment.");
        Console.WriteLine("- Users are responsible for checking their reservation details.");
        Console.WriteLine("- Tickets and reservations are linked to the selected movie and time.");
        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    static void ShowFinancialPolicies()
    {
        Console.Clear();
        Console.WriteLine("=== Financial Policies ===");
        Console.WriteLine();
        Console.WriteLine("- Ticket prices may vary depending on the movie, room, or seat.");
        Console.WriteLine("- Payments must be completed through the available payment methods.");
        Console.WriteLine("- Refunds or changes may depend on the cinema policy.");
        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    static void ShowLegalPolicies()
    {
        Console.Clear();
        Console.WriteLine("=== Legal Policies ===");
        Console.WriteLine();
        Console.WriteLine("- Users must respect age restrictions for movies.");
        Console.WriteLine("- Personal data must be used in a lawful and respectful way.");
        Console.WriteLine("- The cinema reserves the right to refuse access if rules are not followed.");
        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }
}