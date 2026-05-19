public static class SelectMovie
{
    private static List<MovieModel>? Movies;

    public static MovieModel? Start()
    {

        List<string> movies = MoviesLogic.GetMovieTitles();
        int selectedMovie = UiHelper.SelectionMenu(movies);
        if (selectedMovie == -1) return null;

        MovieModel? movie = MoviesLogic.GetMovieByTitle(movies[selectedMovie]);

        if (movie == null)
        {
            UiHelper.HoldUser("something went really wrong, press any key to try again");
            Console.WriteLine("[DisableMovie.cs L13]");

            movie = Start();
        }

        return movie;
    }
}
