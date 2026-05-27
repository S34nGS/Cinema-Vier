static class Menu
{
    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        while (true)
        {
            string header = (AccountsLogic.CurrentAccount != null)
                ? $"Welcome {AccountsLogic.CurrentAccount.FirstName}"
                : "Welcome to Cinema Vier! Please select an option:";

            List<string> menu = [];

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

            int selected = UiHelper.SelectionMenu(menu, header, true);

            if (selected == menu.IndexOf("Login"))
            {
                UserLogin.Start();
            }
            else if (selected == menu.IndexOf("Register"))
            {
                UserRegistration.Start();
            }
            else if (selected == menu.IndexOf("Book Movie"))
            {
                MovieModel? movie = MoviesLogic.Start();

                if (movie is null)
                {
                    continue;
                }

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
    }
}