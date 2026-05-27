using System.Linq;

static class MoviesMenu
{
    public static string header = "All available movies";

    public static int Start()
    {
        while (true)
        {
            int preMovieListMenu;
            if(AccountsLogic.CurrentAccount != null)
            {
                preMovieListMenu = UiHelper.SelectionMenu(["Search by name", "Search by date", "View available movies", "recommended movies"]);         
            }
            else
            {
                preMovieListMenu = UiHelper.SelectionMenu(["Search by name", "Search by date", "View available movies"]);
            }

            if (preMovieListMenu == -1)
            {
                return -1;
            }

            if (preMovieListMenu == 0)
            {
                while (true)
                {
                    string input = UiHelper.Input("Fill in title");
                    if (input == "-1")
                    {
                        break;
                    }
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
                    int movieListMenuSearch = UiHelper.SelectionMenu(searchedMovieList, header);
                    if (movieListMenuSearch == -1)
                    {
                        continue;
                    }
                    return movieListMenuSearch;
                }
                continue;
            }

            if (preMovieListMenu == 1)
            {
                while (true)
                {
                    List<string> dates = [];
                    for (int i = 0; i < 14; i++)
                    {
                        dates.Add(TimetablesLogic.GetDateString(DateTime.Today.AddDays(i).AddHours(13)));
                    }

                    int pickedDate = UiHelper.SelectionMenu(dates, header);

                    if (pickedDate == -1)
                    {
                        break;
                    }

                    List<string> searchedDateMovieList = [];
                    List<TimetableModel> searchedDateTimetableList = TimetablesLogic.GetTimetablesByDate(dates[pickedDate]);

                    foreach (TimetableModel timetable in searchedDateTimetableList)
                    {
                        searchedDateMovieList.Add(MoviesLogic.GetById(timetable.MovieId).Title);
                    }

                    if (searchedDateMovieList.Count == 0)
                    {
                        UiHelper.SelectionMenu(
                            ["No movies found."],
                            "Results",
                            true
                        );
                        continue;
                    }
                    int movieListMenuSearch = UiHelper.SelectionMenu(searchedDateMovieList, header);
                    if (movieListMenuSearch == -1)
                    {
                        continue;
                    }
                    return movieListMenuSearch;
                }
                continue;
            }

            if (preMovieListMenu == 2)
            {
                while (true)
                {
                    int movieListMenu = UiHelper.SelectionMenu(MoviesLogic.GetMovieTitles(), header);
                    if (movieListMenu == -1)
                    {
                        return -1;
                    }
                    return movieListMenu;
                }
            }

            if (preMovieListMenu == 3)
            {
                while (true)
                {
                    List<string> recommendedMoviesTitle = MoviesLogic.GetRecommendedMovies();
                    
                    if (recommendedMoviesTitle.Count == 0)
                    {
                        UiHelper.SelectionMenu(["No recommended movies available. Watch some movies first!"], "Recommendations", true);
                        break;
                    }

                    int selectedRecommendation = UiHelper.SelectionMenu(recommendedMoviesTitle, "Recommended Movies");

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
}