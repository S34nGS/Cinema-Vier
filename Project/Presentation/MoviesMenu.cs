static class MoviesMenu
{
    public static string header = "All available movies";

    public static int Start()
    {
        while (true)
        {
            int preMovieListMenu;
            if (AccountsLogic.CurrentAccount != null)
            {
                preMovieListMenu = UiHelper.SelectionMenu.WriteMenu(
                    [
                        "Search by name",
                        "Search by date",
                        "View available movies",
                        "Recommended movies"
                    ]
                );
            }
            else
            {
                preMovieListMenu = UiHelper.SelectionMenu.WriteMenu(
                    [
                        "Search by name",
                        "Search by date",
                        "View available movies"
                    ]
                );
            }

            if (preMovieListMenu == -1)
            {
                return -1;
            }

            if (preMovieListMenu == 0)
            {
                int val = SearchByName();
                if (val == -1)
                {
                    continue;
                }
                return val;
            }

            if (preMovieListMenu == 1)
            {
                int val = SearchByDate();
                if (val == -1)
                {
                    continue;
                }

                return val;
            }

            if (preMovieListMenu == 2)
            {
                int val = ViewAllMovies();
                if (val == -1)
                {
                    continue;
                }
                return val;
            }

            if (preMovieListMenu == 3)
            {
                while (true)
                {
                    List<string> recommendedMoviesTitle = MoviesLogic.GetRecommendedMovies();

                    if (recommendedMoviesTitle.Count == 0)
                    {
                        UiHelper.SelectionMenu.WriteMenu(["No recommended movies available. Watch some movies first!"], "Recommendations", true);
                        break;
                    }

                    int selectedRecommendation = UiHelper.SelectionMenu.WriteMenu(recommendedMoviesTitle, "Recommended Movies");

                    if (selectedRecommendation == -1)
                    {
                        break;
                    }

                    string selectedMovieTitle = recommendedMoviesTitle[selectedRecommendation];
                    int movieListMenu = MoviesLogic.GetMovieTitles().IndexOf(selectedMovieTitle);

                    return movieListMenu;
                }
            }
        }
    }

    private static int SearchByName()
    {
        while (true)
        {
            string input = UiHelper.InputMenu.WriteMenu("Fill in title", grows: true);
            if (input == "-1")
            {
                break;
            }
            List<string> searchedMovieList = MoviesLogic.GetByPartOfTitle(input);
            if (searchedMovieList.Count == 0)
            {
                UiHelper.SelectionMenu.WriteMenu(
                    ["No movies found."],
                    "Results",
                    true
                );
                continue;
            }
            int movieListMenuSearch = UiHelper.SelectionMenu.WriteMenu(searchedMovieList, header);
            if (movieListMenuSearch == -1)
            {
                continue;
            }
            return movieListMenuSearch;
        }
        return -1;
    }

    private static int SearchByDate()
    {
        while (true)
        {
            List<string> dates = [];
            for (int i = 0; i < 14; i++)
            {
                dates.Add(TimeLogic.ConvertDateString(DateTime.Today.AddDays(i).AddHours(13), "dd-MM-yyyy"));
            }

            int pickedDate = UiHelper.SelectionMenu.WriteMenu(dates, header);

            if (pickedDate == -1)
            {
                return -1;
            }

            List<string> searchedDateMovieList = [];
            List<TimetableModel> searchedDateTimetableList = TimetablesLogic.GetTimetablesByDate(dates[pickedDate]);

            foreach (TimetableModel timetable in searchedDateTimetableList)
            {
                searchedDateMovieList.Add(MoviesLogic.GetById(timetable.MovieId).Title);
            }

            if (searchedDateMovieList.Count == 0)
            {
                UiHelper.SelectionMenu.WriteMenu(
                    ["No movies found."],
                    "Results",
                    true
                );
                continue;
            }
            int movieListMenuSearch = UiHelper.SelectionMenu.WriteMenu(searchedDateMovieList, header);
            if (movieListMenuSearch == -1)
            {
                return -1;
            }
            return movieListMenuSearch;
        }
    }

    private static int ViewAllMovies()
    {
        while (true)
        {
            int movieListMenu = UiHelper.SelectionMenu.WriteMenu(MoviesLogic.GetMovieTitles(), header);
            if (movieListMenu == -1)
            {
                return -1;
            }
            return movieListMenu;
        }
    }
}
