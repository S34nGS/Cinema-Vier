using System.Linq;

static class MoviesMenu
{
    public static string header = "All available movies:";
    public static int Start()
    {
        int selected = UiLib.SelectionMenu(MoviesLogic.GetMovieTitles().Prepend("Search").ToList(), header);
        if (selected == 0)
        {
            string input = UiLib.Input("", header);
            int selectedSearch = UiLib.SelectionMenu(MoviesLogic.GetByPartOfTitle(input), header);
            return selectedSearch;
        }
        return selected - 1;
    }
}