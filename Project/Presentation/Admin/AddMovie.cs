public static class AddMovie
{
    public static void Start()
    {
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
            "Add Movie"
        );

        MoviesLogic.AddMovie(movieInput);

        UiHelper.HoldUser("Movie added successfully.");
    }
}
