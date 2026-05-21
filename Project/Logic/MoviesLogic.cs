public static class MoviesLogic
{
    private static MoviesAccess _access = new();
    private static List<MovieModel> _AvailableMovies = [];

    private static void InitializeMovies()
    {
        if (_AvailableMovies.Count == 0)
        {
            RefreshMovies();
        }
    }

    private static void RefreshMovies()
    {
        _AvailableMovies = _access.GetAllMovies();
    }

    public static List<string> GetMovieTitles(bool activeOnly = true)
    {
        InitializeMovies();

        List<string> Titles = [];
        _AvailableMovies.ForEach(movie =>
        {
            if (!activeOnly || activeOnly && movie.IsActive == 1)
            {
                Titles.Add(movie.Title);
            }
        });

        return Titles;
    }

    public static MovieModel GetMovieData(int MovieIndex)
    {
        InitializeMovies();
        return _AvailableMovies[MovieIndex];
    }

    public static List<string> GetByPartOfTitle(string pattern)
    {
        InitializeMovies();
        List<string> Titles = _AvailableMovies
            .Where(x => x.Title.Contains(pattern, StringComparison.OrdinalIgnoreCase))
            .Select(x => x.Title)
            .ToList();

        return Titles;
    }

    public static MovieModel? GetById(Int64 movieId)
    {
        InitializeMovies();
        return _AvailableMovies.FirstOrDefault(x => x.Id == movieId);
    }

    public static bool IsOldEnough(MovieModel movie, AccountModel account)
    {
        InitializeMovies();
        int age = AccountsLogic.CalculateAge(TimeLogic.ConvertUnixTimeToDateTimeValue(account.DateOfBirth));
        return age >= movie.AgeRating;
    }

    public static MovieModel? Start()
    {
        InitializeMovies();
        int movieIndex = MoviesMenu.Start();
        if (movieIndex < 0) return null;
        return GetMovieData(movieIndex);
    }

    public static MovieModel? GetMovieByTitle(string title)
    {
        InitializeMovies();
        return _AvailableMovies.FirstOrDefault(x => x.Title == title);
    }

    public static void DisableMovie(MovieModel movie)
    {
        _access.Update(movie);
        RefreshMovies();
    }

    public static void AddMovie(Dictionary<string, string> movie)
    {
        MovieModel movieModel = new(
            -1,
            movie["Title"],
            Convert.ToInt32(movie["Duration"]),
            movie["Summary"],
            movie["Director"],
            Convert.ToInt32(movie["Age Rating"]),
            movie["Genre"],
            Convert.ToInt32(movie["Release Year"]),
            1
        );

        AddMovie(movieModel);
    }

    public static void AddMovie(MovieModel movie)
    {
        _access.Write(movie);
        RefreshMovies();
    }

    public static void EditMovie(Int64 id, Dictionary<string, string> movieDict)
    {
        MovieModel movie = new(
            id,
            movieDict["Title"],
            Convert.ToInt32(movieDict["Duration"]),
            movieDict["Summary"],
            movieDict["Director"],
            Convert.ToInt32(movieDict["Age Rating"]),
            movieDict["Genre"],
            Convert.ToInt32(movieDict["Release Year"])
        );

        EditMovie(movie);
    }

    public static void EditMovie(MovieModel movie)
    {
        _access.Update(movie);
        RefreshMovies();
    }
}
