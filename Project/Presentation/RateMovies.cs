static class RateMovies
{
    public static void Start()
    {
        if(RatingsLogic.GetWatchedMovies(AccountsLogic.CurrentAccount!.Id).Count == 0)
        {
            UiHelper.HoldUser("You can only rate movies that you've already seen.");
        }

        MovieModel movie = RatingsLogic.PickMovieToRate("Pick a movie to give a rating to");
        RatingsLogic.RateMovie(movie, "Choose your rating by using the left and right arrows");

        UiHelper.HoldUser("Rating has been successfully added");
        Menu.Start();
    }
}