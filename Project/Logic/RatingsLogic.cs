public static class RatingsLogic
{
    private static RatingsAccess _access = new();

    public static List<MovieModel> GetWatchedMovies(Int64 customerId)
    {
        List<MovieModel> watchedMovies = [];

        foreach(ReservationModel reservation in ReservationsLogic.GetPastReservations(customerId))
        {
            TimetableModel timetable = TimetablesLogic.GetById(reservation.TimeTableId);
            MovieModel movie = MoviesLogic.GetById(timetable.MovieId);
            watchedMovies.Add(movie);
        }

        return watchedMovies.Distinct().ToList();
    }

    public static MovieModel PickMovieToRate(string header)
    {
        List<MovieModel> watchedMovies = GetWatchedMovies(AccountsLogic.CurrentAccount.Id);
        List<string> movieTitles = [];
        foreach (MovieModel movie in watchedMovies)
        {
            movieTitles.Add(movie.Title);
        }
        int selected = UiHelper.SelectionMenu.WriteMenu(movieTitles, header);
        return watchedMovies[selected];
    }

    public static void RateMovie(MovieModel movie, string header)
    {
        int ratingNumber = UiHelper.InputMenu.RateMenu(header);
        RatingModel rating = new(-1, AccountsLogic.CurrentAccount.Id, movie.Id, ratingNumber);
        _access.Write(rating);
    }
}