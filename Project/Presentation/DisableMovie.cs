public static class DisableMovie
{
    public static void Start()
    {
        List<string> movies = MoviesLogic.GetMovieTitles();
        int selectedMovie = UiHelper.SelectionMenu(movies);

        MovieModel movie = MoviesLogic.GetMovieByTitle(movies[selectedMovie]);
        movie.IsActive = 0;
        MoviesLogic.DisableMovie(movie);
    }
}