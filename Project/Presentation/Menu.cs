static class Menu
{
    static public void Start()
    {
        string header = (AccountsLogic.CurrentAccount != null)
            ? $"Welcome {AccountsLogic.CurrentAccount.FirstName}"
            : "Welcome to Cinema Vier! Please select an option:";

        string[] menu = BuildMenu();

        int selected = UiHelper.SelectionMenu.WriteMenu(menu, header, true);

        ActMenuOption(menu, selected);
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

        // TODO: Rework this to be its own seperate Book Movie functionality
        else if (selected == Array.IndexOf(menu, "Book Movie"))
        {
            while (true)
            {
                MovieModel? movie = MoviesLogic.Start();
                // if (movie is null)
                // {
                //     Start();
                // }

                PurchaseTicket.SetUpDateMenu(movie);
                UiHelper.HoldUser(movie.ToString());

                if (AccountsLogic.CurrentAccount != null && !MoviesLogic.IsOldEnough(movie, AccountsLogic.CurrentAccount))
                {
                    UiHelper.HoldUser($"You must be {movie.AgeRating}+ to watch this movie.");
                    continue;
                }

                PurchaseTicket.Start(movie);
            }
            else if (selected == menu.IndexOf("Book Movie For Customer"))
            {
                AdminBookMovie.Start();
            }
            else if (selected == menu.IndexOf("Cinema Info"))
            {
                CinemaInfo.Start();
            }
            else if (selected == menu.IndexOf("View Reservations"))
            {
                ViewReservations.Start();
            }
            else if (selected == menu.IndexOf("Add Movie"))
            {
                AddMovie.Start();
            }
            else if (selected == menu.IndexOf("Edit Movie"))
            {
                EditMovie.Start();
            }
            else if (selected == menu.IndexOf("Disable Movie"))
            {
                DisableMovie.Start();
            }
            else if (selected == menu.IndexOf("Manage Timetables"))
            {
                Timetables.Start();
            }
            else if (selected == menu.IndexOf("Logout"))
            {
                AccountsLogic.Logout();
                continue;
            }
            else if (selected == menu.IndexOf("Exit"))
            {
                Console.WriteLine("Thank you for using Cinema Vier! Goodbye!");
                Environment.Exit(0);
            }
        }
        else if (selected == Array.IndexOf(menu, "Cinema Info"))
        {
            CinemaInfo.Start();
        }
        else if (selected == Array.IndexOf(menu, "View Reservations"))
        {
            ViewReservations.Start();
        }
        else if (selected == Array.IndexOf(menu, "Add Movie"))
        {
            AddMovie.Start();
            Start();
        }
        else if (selected == Array.IndexOf(menu, "Edit Movie"))
        {
            EditMovie.Start();
            Start();
        }
        else if (selected == Array.IndexOf(menu, "Disable Movie"))
        {
            DisableMovie.Start();
            Start();
        }
        else if (selected == Array.IndexOf(menu, "Manage Timetables"))
        {
            Timetables.Start();
            Start();
        }
        else if (selected == Array.IndexOf(menu, "Book Movie For Customer"))
        {
            AdminBookMovie.Start();
            Start();
        }
        else if (selected == Array.IndexOf(menu, "Logout"))
        {
            AccountsLogic.Logout();
            Start();
        }
        else if (selected == Array.IndexOf(menu, "Exit"))
        {
            Console.WriteLine("Thank you for using Cinema Vier! Goodbye!");
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
            menu = ["Book Movie For Customer", "Add Movie", "Edit Movie", "Disable Movie", "Manage Timetables", "Logout"];
        }
        else
        {
            menu = ["Book Movie", "View Reservations", "Cinema Info", "Logout", "Exit"];
        }

        return menu;
    }
}
