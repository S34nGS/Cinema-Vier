public static class Timetables
{
    public static void Start()
    {
        List<string> menu = ["Create Timetable", "Edit Timetable", "Delete Timetable"];

        int selected = UiHelper.SelectionMenu(menu);

        if (selected == menu.IndexOf("Create Timetable"))
        {
            Dictionary<string, string> CreateTimetableInput = UiHelper.InputForm(
                [
                    "Movie Title",
                    "Room Number",
                    "Date (dd-mm-yyyy)",
                    "Start Time (hh:mm)",
                ],
                "Add Timetable"
            );

            int createdTimetable = TimetablesLogic.CreateTimetableAsAdmin(
                CreateTimetableInput["Movie Title"],
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
                UiHelper.HoldUser("The given title doesn't belong to an available movie");
            }
            else if (createdTimetable == 2)
            {
                UiHelper.HoldUser("The room number doesn't exist");
            }
            else if (createdTimetable == 3)
            {
                UiHelper.HoldUser("The given date is invalid");
            }
            else if (createdTimetable == 4)
            {
                UiHelper.HoldUser("The given start time is invalid.");
            }
            else if (createdTimetable == 5)
            {
                UiHelper.HoldUser("There's already a movie playing at this time and room");
            }
        }

        // TODO rework menu
        // Show "Manage timetables"
        // Show list of movies
        // Click on one
        // Get asked to add, edit or delete
        if (selected == menu.IndexOf("Edit Timetable"))
        {
            TimetableModel timetable = TimetablesLogic.ChooseTimeTableToEditAsAdmin("All timetables from today onward");

            Dictionary<string, string> EditTimetableInput = UiHelper.InputForm(
                [
                    "Movie Title",
                    "Room Number",
                    "Date (dd-mm-yyyy)",
                    "Start Time (hh:mm)",
                ],
                "Edit Timetable",
                filledFields: TimetablesLogic.GetDetailsAsList(timetable)
            );

            int editedTimetable;

            while (true)
            { 
                editedTimetable = TimetablesLogic.EditTimeTableAsAdmin(
                    timetable.Id,
                    EditTimetableInput["Movie Title"],
                    EditTimetableInput["Room Number"],
                    EditTimetableInput["Date (dd-mm-yyyy)"],
                    EditTimetableInput["Start Time (hh:mm)"]
                );
                if (editedTimetable == 0) break;
                else continue;
            }

            if (editedTimetable == 0)
            {
                UiHelper.HoldUser("Timetable edited successfully.");
            }
            else if (editedTimetable == 1)
            {
                UiHelper.HoldUser("The given title doesn't belong to an available movie");
            }
            else if (editedTimetable == 2)
            {
                UiHelper.HoldUser("The room number doesn't exist");
            }
            else if (editedTimetable == 3)
            {
                UiHelper.HoldUser("The given date is invalid");
            }
            else if (editedTimetable == 4)
            {
                UiHelper.HoldUser("The given start time is invalid.");
            }
        }

    }
}