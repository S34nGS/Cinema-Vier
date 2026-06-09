static class Menu
{
    static public void Start()
    {
        string header = (AccountsLogic.CurrentAccount != null)
            ? $"Welcome {AccountsLogic.CurrentAccount.FirstName} (Pass point: {AccountsLogic.CurrentAccount.PassPoints})"
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
                
                if (AccountsLogic.CurrentAccount != null)
                {
                    bool isOldEnough = MoviesLogic.IsOldEnough(movie, AccountsLogic.CurrentAccount);
                    DateTime? availableDate = MoviesLogic.GetAvailableDate(movie, AccountsLogic.CurrentAccount);
                    
                    if (!isOldEnough && availableDate == null)
                    {
                        UiHelper.HoldUser($"You must be {movie.AgeRating}+ to watch this movie. You will not reach this age within the next 2 weeks.");
                        Start();
                    }
                    else if (!isOldEnough && availableDate != null)
                    {
                        string birthdayMessage = availableDate.Value.ToString("dd-MM-yyyy");
                        UiHelper.HoldUser($"You will turn {movie.AgeRating}+ on {birthdayMessage}. Dates before this day will not be available.");
                    }
                }

                while (true)
                {
                    TicketModel? purchaseTicket = PurchaseTicket.Start(movie);
                    if (purchaseTicket is null)
                    {
                        break;
                    }
                    else
                    {
                        Start();
                    }
                }
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
        else if (selected == Array.IndexOf(menu, "Top up Movie Pass"))
        {
            MoviePass.Start();
            Start();
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
        else if (selected == Array.IndexOf(menu, "Manage Timetable"))
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
            menu = ["Book Movie For Customer", "Add Movie", "Edit Movie", "Disable Movie", "Manage Timetable", "Logout"];
        }
        else
        {
            menu = ["Book Movie", "View Reservations", "Top up Movie Pass", "Cinema Info", "Logout", "Exit"];
        }

        return menu;
    }
}