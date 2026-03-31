public static class MoviesLogic
{
    static private MoviesAccess _access = new();
    static public List<string> GetMovieTitles(){
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