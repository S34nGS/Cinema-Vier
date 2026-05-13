public static class EditMovie
{
    public static void Start()
    {
        List<string> movies = MoviesLogic.GetMovieTitles();

        int selectedMovie = UiHelper.SelectionMenu(movies);

        MovieModel movie = MoviesLogic.GetMovieByTitle(movies[selectedMovie]);

        Dictionary<string, string> movieInput = UiHelper.InputForm(
            [
                "Title",
                "Duration",
                "Summary",
                "Director",
                "Age Rating",
                "Genre",
                "Release Year"
            ],
            "Edit Movie"
        );

        movie.Title = movieInput["Title"];
        movie.Duration = Convert.ToInt32(movieInput["Duration"]);
        movie.Summary = movieInput["Summary"];
        movie.Director = movieInput["Director"];
        movie.AgeRating = Convert.ToInt32(movieInput["Age Rating"]);
        movie.Genre = movieInput["Genre"];
        movie.ReleaseDate = Convert.ToInt32(movieInput["Release Year"]);

        MoviesLogic.EditMovie(movie);

        UiHelper.HoldUser("Movie updated successfully.");
    }
}