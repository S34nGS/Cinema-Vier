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
            UiHelper.HoldUser(movie.ToString());
            if (!MoviesLogic.IsOldEnough(movie, account))
            {
                    UiHelper.HoldUser($"The customer must be {movie.AgeRating}+ to watch this movie.");
                    BookAsAdmin(header);
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