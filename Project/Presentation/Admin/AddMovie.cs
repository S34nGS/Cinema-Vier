public static class AddMovie
{
    public static void Start()
    {
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
            "Add Movie"
        );

        MovieModel movie = new(
            0,
            movieInput["Title"],
            Convert.ToInt32(movieInput["Duration"]),
            movieInput["Summary"],
            movieInput["Director"],
            Convert.ToInt32(movieInput["Age Rating"]),
            movieInput["Genre"],
            Convert.ToInt32(movieInput["Release Year"]),
            1
        );

        MoviesLogic.AddMovie(movie);

        UiHelper.HoldUser("Movie added successfully.");
    }
}