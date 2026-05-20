static class MoviesMenu
{
    public static string header = "All available movies";

    public static int Start()
    {
        while (true)
        {
            int preMovieListMenu = UiHelper.SelectionMenu.WriteMenu(["Search by name", "Search by date", "View available movies"]);

            if (preMovieListMenu == -1)
            {
                return -1;
            }

            if (preMovieListMenu == 0)
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
                continue;
            }

            if (preMovieListMenu == 1)
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
                    int movieListMenu = UiHelper.SelectionMenu.WriteMenu(MoviesLogic.GetMovieTitles(), header);
                    if (movieListMenu == -1)
                    {
                        return -1;
                    }
                    return movieListMenu;
                }
            }
        }
    }
}
