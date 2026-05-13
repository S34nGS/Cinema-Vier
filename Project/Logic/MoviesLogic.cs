public static class MoviesLogic
{
    private static MoviesAccess _access = new();
    public static List<string> GetMovieTitles()
    {
        List<string> Titles = [];
        foreach (MovieModel Movie in _access.GetAllMovies())
        {
            if (Movie.IsActive == 1)
            {
                Titles.Add(Movie.Title);
            }
        }
        return Titles;
    }

    public static MovieModel GetMovieData(int MovieIndex)
    {
        return _access.GetByTitle(GetMovieTitles()[MovieIndex]);
    }

    public static List<string> GetByPartOfTitle(string pattern)
    {
        List<string> Titles = [];
        foreach (MovieModel Movie in _access.GetByPartOfTitle(pattern))
        {
            Titles.Add(Movie.Title);
        }
        return Titles;
    }

    public static MovieModel GetById(Int64 movieId)
    {
        return _access.GetById(movieId);
    }

    public static bool IsOldEnough(MovieModel movie, AccountModel account)
    {
        int age = AccountsLogic.CalculateAge(TimetablesLogic.ConvertUnixTimeToDateTimeValue(account.DateOfBirth));
        return age >= movie.AgeRating;
    }
    public static MovieModel? Start()
    {
        int movieIndex = MoviesMenu.Start();
        if (movieIndex < 0) return null;
        return GetMovieData(movieIndex);
    }

    public static MovieModel? GetMovieByTitle(string title)
    {
        return _access.GetByTitle(title);
    }

    public static void DisableMovie(MovieModel movie)
    {
        _access.Update(movie);
    }

    public static void AddMovie(MovieModel movie)
    {
        _access.Write(movie);
    }

    public static void EditMovie(MovieModel movie)
    {
        _access.Update(movie);
    }
}