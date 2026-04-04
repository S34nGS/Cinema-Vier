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

    static public MovieModel GetMovieData(int MovieIndex)
    {
        return _access.GetByTitle(GetMovieTitles()[MovieIndex]);
    }
}