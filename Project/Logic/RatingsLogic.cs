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
        (bool exists, RatingModel existingRating) = CheckForExistingRating(movie.Id);
        double ratingNumber = exists ? UiHelper.InputMenu.RateMenu(header, (int)existingRating.Rating) : UiHelper.InputMenu.RateMenu(header);
        
        if (exists)
        {
            existingRating.Rating = ratingNumber;
            _access.Update(existingRating);
        }
        else
        {
            RatingModel rating = new(-1, AccountsLogic.CurrentAccount.Id, movie.Id, ratingNumber);
            _access.Write(rating);
        }
    }

    public static (bool, RatingModel?) CheckForExistingRating(Int64 movieId)
    {
        Int64 userId = AccountsLogic.CurrentAccount.Id;
        List<RatingModel> existingRatings = _access.GetRatingsByUserId(userId);
        foreach(RatingModel rating in existingRatings)
        {
            if(rating.MovieId == movieId)
            {
                return (true, rating);
            }
        }
        return (false, null);
    }
}