static class Menu
{
    static public void Start()
    {
        while (true)
        {
            string header = (AccountsLogic.CurrentAccount != null)
                ? $"Welcome {AccountsLogic.CurrentAccount.FirstName} (Pass point: {AccountsLogic.CurrentAccount.PassPoints})"
                : "Welcome to Cinema Vier! Please select an option:";

            string[] menu = BuildMenu();

            int selected = UiHelper.SelectionMenu.WriteMenu(menu, header, true);

            if (selected == -1)
            {
                continue;
            }

            ActMenuOption(menu, selected);
        }
    }

    public static void ActMenuOption(string[] menu, int selected)
    {
        if (selected == Array.IndexOf(menu, "Login"))
        {
            UserLogin.Start();
        }
        else if (selected == Array.IndexOf(menu, "Register"))
        {
            UserRegistration.Start();
        }
        else if (selected == Array.IndexOf(menu, "Book Movie"))
        {
            MovieModel? movie = MoviesLogic.Start();

            if (movie is null)
            {
                return;
            }

            PurchaseTicket.SetUpDateMenu(movie);
            UiHelper.HoldUser(movie.ToString());

            if (AccountsLogic.CurrentAccount != null && !MoviesLogic.IsOldEnough(movie, AccountsLogic.CurrentAccount))
            {
                UiHelper.HoldUser($"You must be {movie.AgeRating}+ to watch this movie.");
                return;
            }

            PurchaseTicket.Start(movie);
        }
        else if (selected == Array.IndexOf(menu, "Book Movie For Customer"))
        {
            AdminBookMovie.Start();
        }
        else if (selected == Array.IndexOf(menu, "Cinema Info"))
        {
            CinemaInfo.Start();
        }
        else if (selected == Array.IndexOf(menu, "View Reservations"))
        {
            ViewReservations.Start();
        }
        else if (selected == Array.IndexOf(menu, "Top up Movie Pass"))
        {
            MoviePass.Start();
        }
        else if (selected == Array.IndexOf(menu, "Add Movie"))
        {
            AddMovie.Start();
        }
        else if (selected == Array.IndexOf(menu, "Edit Movie"))
        {
            EditMovie.Start();
        }
        else if (selected == Array.IndexOf(menu, "Disable Movie"))
        {
            DisableMovie.Start();
        }
        else if (selected == Array.IndexOf(menu, "Manage Timetable"))
        {
            Timetables.Start();
        }
        else if (selected == Array.IndexOf(menu, "Logout"))
        {
            AccountsLogic.Logout();
        }
        else if (selected == Array.IndexOf(menu, "Exit"))
        {
            Console.WriteLine("Thank you for using Cinema Vier! Goodbye!");
            Environment.Exit(0);
        }
    }

    public static string[] BuildMenu()
    {
        string[] menu;

        if (AccountsLogic.CurrentAccount is null)
        {
            menu = ["Book Movie", "Login", "Register", "Cinema Info", "Exit"];
        }
        else if (AccountsLogic.CurrentAccount.IsAdmin == 1)
        {
            menu = ["Book Movie For Customer", "Add Movie", "Edit Movie", "Disable Movie", "Manage Timetable", "Logout"];
        }
        else
        {
            menu = ["Book Movie", "View Reservations", "Top up Movie Pass", "Cinema Info", "Logout", "Exit"];
        }

        return menu;
    }
}