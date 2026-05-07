using System.Linq;

static class MoviesMenu
{
    public static string header = "All available movies:";
    public static int Start()
    {
        while (true)
        {
            int selected = UiHelper.SelectionMenu(MoviesLogic.GetMovieTitles().Prepend("Search").ToList(), header);
            
            if (selected == -1) return -1;

            if (selected == 0)
            {
                while (true)
                {
                    string input = UiHelper.Input("Search movie:");
                    if (input == "-1") break;
                    List<string> searchedMovieList = MoviesLogic.GetByPartOfTitle(input);
                    if (searchedMovieList.Count == 0)
                    {
                        UiHelper.SelectionMenu(
                            ["No movies found."],
                            "Results",
                            true
                        );
                        continue;
                    }
                    int selectedSearch = UiHelper.SelectionMenu(searchedMovieList, header);
                    if (selectedSearch == -1) continue;
                    return selectedSearch;
                }
                continue;
            }
            
            return selected - 1;
        }
    }
}