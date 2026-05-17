public static class Timetables
{
    public static void Start()
    {
        List<string> menu = ["Create Timetable", "Edit Timetable", "Delete Timetable"];

        int selected = UiHelper.SelectionMenu(menu);

        if (selected == menu.IndexOf("Create Timetable"))
        {
            Dictionary<string, string> movieInput = UiHelper.InputForm(
                [
                    "Movie Title",
                    "Room Number",
                    "Date (dd-mm-yyyy)",
                    "Start Time (hh:mm)",
                ],
                "Add Timetable"
            );

            int createdTimetable = TimetablesLogic.CreateTimeTableAsAdmin(
                movieInput["Movie Title"],
                movieInput["Room Number"],
                movieInput["Date (dd-mm-yyyy)"],
                movieInput["Start Time (hh:mm)"]
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

        if (selected == menu.IndexOf("Edit Timetable"))
        {
            
        }

    }
}