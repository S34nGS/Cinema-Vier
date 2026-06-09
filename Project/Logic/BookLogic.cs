public static class BookLogic
{
    public static void BookAsAdmin(string header)
    {
        AccountsLogic accountsLogic = new();
        AccountModel account = accountsLogic.GetCustomerAsAdmin(header);

        while (true)
        {
            MovieModel? movie = MoviesLogic.Start();

            PurchaseTicket.SetUpDateMenu(movie);
            UiHelper.HoldUser(MoviesLogic.GetMovieDetails(movie));
            
            // Check if user is old enough now or will be within 2 weeks
            bool isOldEnough = MoviesLogic.IsOldEnough(movie, account);
            DateTime? availableDate = MoviesLogic.GetAvailableDate(movie, account);
            
            if (!isOldEnough && availableDate == null)
            {
                UiHelper.HoldUser($"The customer must be {movie.AgeRating}+ to watch this movie. They will not reach this age within the next 2 weeks.");
                BookAsAdmin(header);
            }
            else if (!isOldEnough && availableDate != null)
            {
                string birthdayMessage = availableDate.Value.ToString("dd-MM-yyyy");
                UiHelper.HoldUser($"The customer will turn {movie.AgeRating}+ on {birthdayMessage}. Dates before this day will not be available.");
            }

            while (true)
            {
                TicketModel? purchaseTicket = PurchaseTicket.Start(movie, account);
                if (purchaseTicket is null)
                {
                    break;
                }
                else
                {
                    Menu.Start();
                }
            }
        }
    }
}
