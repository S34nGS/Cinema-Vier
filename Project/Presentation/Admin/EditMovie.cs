public static class EditMovie
{
    public static void Start()
    {
        MovieModel? movie = SelectMovie.Start();

        if (movie is null) return;

        Dictionary<string, string> movieInput = UiHelper.InputFormMenu.WriteMenu(
            new Dictionary<string, string>
            {
                {"Title", movie.Title},
                {"Duration", movie.Duration.ToString()},
                {"Summary", movie.Summary},
                {"Director", movie.Director},
                {"Age Rating", movie.AgeRating.ToString()},
                {"Genre", movie.Genre},
                {"Release Year", movie.ReleaseDate.ToString()},
            },
            "Edit Movie",
            50
        );

        MoviesLogic.EditMovie(movie.Id, movieInput);

        UiHelper.HoldUser("Movie updated successfully.");
    }
}
