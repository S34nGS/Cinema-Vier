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

    public static List<string> GetRecommendedMovies()
    {
        List<ReservationModel> pastReservations = ReservationsLogic.GetPastReservations(AccountsLogic.CurrentAccount.Id);

        if (pastReservations.Count == 0) return new List<string>();

        List<string> userGenres = new List<string>();
        List<Int64> watchedMovieIds = new List<Int64>();

        foreach (ReservationModel reservation in pastReservations)
        {
            TimetableModel timetable = TimetablesLogic.GetById(reservation.TimeTableId);
            MovieModel movie = GetById(timetable.MovieId);
            watchedMovieIds.Add(movie.Id);

            string[] genres = movie.Genre.Split(',');
            foreach (string genre in genres)
            {
                string trimmedGenre = genre.Trim();
                if (!userGenres.Contains(trimmedGenre))
                {
                    userGenres.Add(trimmedGenre);
                }
            }
        }

        List<MovieModel> allMovies = _access.GetAllMovies();
        List<string> recommendedMovies = new List<string>();

        foreach (MovieModel movie in allMovies)
        {
            if (movie.IsActive != 1 || watchedMovieIds.Contains(movie.Id)) continue;

            string[] movieGenres = movie.Genre.Split(',');
            bool foundMatch = false;
            foreach (string movieGenre in movieGenres)
            {
                if (userGenres.Contains(movieGenre.Trim()))
                {
                    foundMatch = true;
                    break;
                }
            }

            if (foundMatch && !recommendedMovies.Contains(movie.Title))
            {
                recommendedMovies.Add(movie.Title);
            }
        }

        return recommendedMovies;
    }
}