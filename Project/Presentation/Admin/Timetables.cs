public static class Timetables
{
    public static void Start()
    {
        List<string> menu = ["Create Timetable", "Edit Timetable", "Delete Timetable"];

        string movieMenuHeader = "Pick a movie to manage the timetables for";

        (int selected, MovieModel movie) = TimetablesLogic.ManageTimetables(movieMenuHeader, menu);

        if (selected == menu.IndexOf("Create Timetable"))
        {
            Dictionary<string, string> CreateTimetableInput = UiHelper.InputFormMenu.WriteMenu(
                [
                    "Room Number",
                    "Date (dd-mm-yyyy)",
                    "Start Time (hh:mm)",
                ],
                $"Add Timetable for {movie.Title}"
            );

            int createdTimetable = TimetablesLogic.CreateTimetableAsAdmin(
                movie,
                CreateTimetableInput["Room Number"],
                CreateTimetableInput["Date (dd-mm-yyyy)"],
                CreateTimetableInput["Start Time (hh:mm)"]
            );

            if (createdTimetable == 0)
            {
                UiHelper.HoldUser("Timetable added successfully.");
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

        if (selected == menu.IndexOf("Edit Timetable"))
        {
            TimetableModel timetable = TimetablesLogic.ChooseTimeTableAsAdmin("All timetables from today onward", movie);

            Dictionary<string, string> EditTimetableInput = UiHelper.InputFormMenu.WriteMenu(
                new Dictionary<string, string>{
                    {"Room Number", timetable.RoomId.ToString()},
                    {"Date (dd-mm-yyyy)",  timetable.StartTime.ConvertUnixTimeToDateTime().ConvertDateString("dd-MM-yyyy")},
                    {"Start Time (hh:mm)", timetable.StartTime.ConvertUnixTimeToDateTime().ConvertDateString("hh:mm")},
                },
                "Edit Timetable"
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
                UiHelper.HoldUser("Timetable edited successfully.");
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

        if (selected == menu.IndexOf("Delete Timetable"))
        {
            TimetableModel timetable = TimetablesLogic.ChooseTimeTableAsAdmin("All timetables from today onward", movie);

            List<string> deletionMenuOptions = ["Yes", "No"];
            string deletionMenuHeader = "Are you sure you want to delete this timetable?";

            int deletedTimetable = TimetablesLogic.DeleteTimetableAsAdmin(timetable, deletionMenuOptions, deletionMenuHeader);

            if (deletedTimetable == 0)
            {
                UiHelper.HoldUser($"The timetable with ID: {timetable.Id} has been deleted.");
            }
            else if (deletedTimetable == 1)
            {
                UiHelper.HoldUser("The timetable hasn't been deleted.");
            }
        }
    }
}
