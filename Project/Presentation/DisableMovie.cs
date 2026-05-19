public static class DisableMovie
{
    public static void Start()
    {
        List<string> movies = MoviesLogic.GetMovieTitles();
        int selectedMovie = UiHelper.SelectionMenu(movies);
        if (selectedMovie == -1) return;

        MovieModel? movie = SelectMovie.Start();

        if (movie == null) return;

        movie.IsActive = 0;
        MoviesLogic.DisableMovie(movie);
    }
}
