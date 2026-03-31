static class MoviesMenu
{
    static string header = "All available movies:";
    static public void Start()
    {
        int selected = UiLib.SelectionMenu(MoviesLogic.GetMovieTitles(), header);
        // System.Console.WriteLine(MoviesLogic.GetMovieData(selected));
    }
}