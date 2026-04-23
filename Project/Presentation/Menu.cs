static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        string header = (AccountsLogic.CurrentAccount != null) ? $"Welcome {AccountsLogic.CurrentAccount.FirstName}" : "Welcome to Cinema Vier! Please select an option:";
        List<string> menu = ["Order Movie", "Login", "Register", "View Reservations", "Cinema Info", "Exit"];
        int selected = UiLib.SelectionMenu(menu, header, true);

        if (selected == menu.IndexOf("Login"))
        {
            UserLogin.Start();
        }
        else if (selected == menu.IndexOf("Register"))
        {
            UserRegistration.Start();
        }
        else if (selected == menu.IndexOf("Order Movie"))
        {
            while (true){
                MovieModel? movie = MoviesLogic.Start();
                if (movie is null)
                {
                    Start();
                    return;
                }

                if (AccountsLogic.CurrentAccount != null)
                {
                    if (!MoviesLogic.IsOldEnough(movie, AccountsLogic.CurrentAccount))
                    {
                        UiLib.HoldUser($"You must be {movie.AgeRating}+ to watch this movie.");
                        Start();
                        return;
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
        else if (selected == menu.IndexOf("Exit"))
        {
            Console.WriteLine("Thank you for using Cinema Vier! Goodbye!");
        }
    }
}