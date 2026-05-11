static class Menu
{
    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        string header = (AccountsLogic.CurrentAccount != null)
            ? $"Welcome {AccountsLogic.CurrentAccount.FirstName}"
            : "Welcome to Cinema Vier! Please select an option:";
        List<string> menu = [];

        if (AccountsLogic.CurrentAccount is null)
        {
            menu = ["View Movies", "Login", "Register", "Cinema Info", "Exit"];
        }
        else if (AccountsLogic.CurrentAccount.IsAdmin == 1)
        {
            menu = ["Add Movie", "Edit Movie", "Disable Movie", "Logout"];
        }
        else
        {
            menu = ["View Movies","View Reservations", "Cinema Info", "Logout", "Exit"];
        }

        int selected = UiHelper.SelectionMenu(menu, header, true);

        if (selected == menu.IndexOf("Login"))
        {
            UserLogin.Start();
        }
        else if (selected == menu.IndexOf("Register"))
        {
            UserRegistration.Start();
        }
        else if (selected == menu.IndexOf("View Movies"))
        {
            while (true)
            {
                MovieModel? movie = MoviesLogic.Start();
                if (movie is null) 
                {
                    Start();
                }

                if (AccountsLogic.CurrentAccount != null)
                {
                    PurchaseTicket.SetUpDateMenu(movie);
                    UiHelper.HoldUser(movie.ToString());
                    
                    if (!MoviesLogic.IsOldEnough(movie, AccountsLogic.CurrentAccount))
                    {
                        UiHelper.HoldUser($"You must be {movie.AgeRating}+ to watch this movie.");
                        Start();
                    }
                }

                while (true)
                {
                    TicketModel? purchaseTicket = PurchaseTicket.Start(movie);
                    if (purchaseTicket is null) break;
                }
            }
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
            
        }
        else if (selected == menu.IndexOf("Edit Movie"))
        {
            
        }
        else if (selected == menu.IndexOf("Disable Movie"))
        {
            DisableMovie.Start();
            Start();
        }
        else if (selected == menu.IndexOf("Logout"))
        {
            AccountsLogic.Logout();
            Start();
        }
        else if (selected == menu.IndexOf("Exit"))
        {
            Console.WriteLine("Thank you for using Cinema Vier! Goodbye!");
        }
    }
}