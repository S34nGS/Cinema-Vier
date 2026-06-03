public static class EditMovie
{
    public static void Start()
    {
        MovieModel? movie = SelectMovie.Start();

        if (movie is null) return;

        Dictionary<string, string> movieInput = UiHelper.InputFormMenu.WriteMenu(
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

        MoviesLogic.EditMovie(movie.Id, movieInput);

        UiHelper.HoldUser("Movie updated successfully.");
    }
}
