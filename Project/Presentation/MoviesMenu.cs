static class MoviesMenu
{
    public static string header = "All available movies:";
    public static void Start()
    {
        int selected = UiLib.SelectionMenu(MoviesLogic.GetMovieTitles(), header);
        // System.Console.WriteLine(MoviesLogic.GetMovieData(selected));
    }
}