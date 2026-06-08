public static class Timetables
{
    public static void Start()
    {
        List<string> menu = ["Create a showing", "Edit a showing", "Delete a showing"];

        string movieMenuHeader = "Pick a movie to manage the showings for";

        (int selected, MovieModel movie) = TimetablesLogic.ManageTimetables(movieMenuHeader, menu);

        if (selected == menu.IndexOf("Create a showing"))
        {
            if (movie.IsActive == 0)
            {
                UiHelper.HoldUser("It's not possible to create new showings for disabled movies.");
            }
            else
            {
                Dictionary<string, string> CreateTimetableInput = UiHelper.InputFormMenu.WriteMenu(
                    [
                        "Room Number",
                        "Date (dd-mm-yyyy)",
                        "Start Time (hh:mm)",
                    ],
                    $"Add showing for {movie.Title}"
                );

                int createdTimetable = TimetablesLogic.CreateTimetableAsAdmin(
                    movie,
                    CreateTimetableInput["Room Number"],
                    CreateTimetableInput["Date (dd-mm-yyyy)"],
                    CreateTimetableInput["Start Time (hh:mm)"]
                );

                if (createdTimetable == 0)
                {
                    UiHelper.HoldUser("Showing added successfully.");
                }
                else if (createdTimetable == 1)
                {
                    UiHelper.HoldUser("The room number doesn't exist");
                }
                else if (createdTimetable == 2)
                {
                    UiHelper.HoldUser("The given date is invalid");
                }
                else if (createdTimetable == 3)
                {
                    UiHelper.HoldUser("The given start time is invalid.");
                }
                else if (createdTimetable == 4)
                {
                    UiHelper.HoldUser("There's already a movie playing at this time and room");
                }
            }
        }

        if (selected == menu.IndexOf("Edit a showing"))
        {
            TimetableModel timetable = TimetablesLogic.ChooseTimeTableAsAdmin("All showings from today onward", movie);

            Dictionary<string, string> EditTimetableInput = UiHelper.InputFormMenu.WriteMenu(
                new Dictionary<string, string>{
                    {"Room Number", timetable.RoomId.ToString()},
                    {"Date (dd-mm-yyyy)",  timetable.StartTime.ConvertUnixTimeToDateTime().ConvertDateString("dd-MM-yyyy")},
                    {"Start Time (hh:mm)", timetable.StartTime.ConvertUnixTimeToDateTime().ConvertDateString("hh:mm")},
                },
                "Edit showing"
            );

            int editedTimetable = TimetablesLogic.EditTimeTableAsAdmin(
                timetable.Id,
                movie,
                EditTimetableInput["Room Number"],
                EditTimetableInput["Date (dd-mm-yyyy)"],
                EditTimetableInput["Start Time (hh:mm)"]
            );

            if (editedTimetable == 0)
            {
                UiHelper.HoldUser("Showing edited successfully.");
            }
            else if (editedTimetable == 1)
            {
                UiHelper.HoldUser("The room number doesn't exist");
            }
            else if (editedTimetable == 2)
            {
                UiHelper.HoldUser("The given date is invalid");
            }
            else if (editedTimetable == 3)
            {
                UiHelper.HoldUser("The given start time is invalid.");
            }
        }

        if (selected == menu.IndexOf("Delete a showing"))
        {
            TimetableModel timetable = TimetablesLogic.ChooseTimeTableAsAdmin("All showings from today onward", movie);

            List<string> deletionMenuOptions = ["Yes", "No"];
            string deletionMenuHeader = "Are you sure you want to delete this showing?";

            int deletedTimetable = TimetablesLogic.DeleteTimetableAsAdmin(timetable, deletionMenuOptions, deletionMenuHeader);

            if (deletedTimetable == 0)
            {
                UiHelper.HoldUser($"The showing with ID: {timetable.Id} has been deleted.");
            }
            else if (deletedTimetable == 1)
            {
                UiHelper.HoldUser("The showing hasn't been deleted.");
            }
        }
    }
}
