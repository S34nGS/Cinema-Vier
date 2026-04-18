public static class MoviesLogic
{
    private static MoviesAccess _access = new();
    public static List<string> GetMovieTitles(){
        List<string> Titles = [];
        foreach(MovieModel Movie in _access.GetAllMovies())
        {
            Titles.Add(Movie.Title);
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
        foreach(MovieModel Movie in _access.GetByPartOfTitle(pattern))
        {
            Titles.Add(Movie.Title);
        }
        return Titles;
    }

    public static MovieModel? Start()
    {
        int movieIndex = MoviesMenu.Start();
        if (movieIndex < 0) return null;
        return GetMovieData(movieIndex);
    }
}