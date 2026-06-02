public static class DisableMovie
{
    public static void Start()
    {
        MovieModel? movie = SelectMovie.Start();

        if (movie == null) return;

        movie.IsActive = 0;
        MoviesLogic.DisableMovie(movie);
    }
}
