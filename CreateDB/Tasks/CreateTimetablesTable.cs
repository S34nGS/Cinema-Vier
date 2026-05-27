public static class CreateTimetablesTable
{
    public static Task Execute()
    {
        TimetablesAccess timetables = new();
        timetables.CreateTable();

        DateTime baseDate = DateTime.Today;

        // Movie 1
        DateTime date1 = baseDate.AddDays(1).AddHours(13);
        DateTime date2 = baseDate.AddDays(2).AddHours(14);
        DateTime date3 = baseDate.AddDays(3).AddHours(15);
        DateTime date4 = baseDate.AddDays(7).AddHours(15);
        DateTime date5 = baseDate.AddDays(9).AddHours(15);
        DateTime date6 = baseDate.AddDays(14).AddHours(15);

        // Movie 2
        DateTime date7 = baseDate.AddDays(1).AddHours(15);
        DateTime date8 = baseDate.AddDays(2).AddHours(15);
        DateTime date9 = baseDate.AddDays(4).AddHours(15);
        DateTime date10 = baseDate.AddDays(5).AddHours(15);
        DateTime date11 = baseDate.AddDays(6).AddHours(15);
        DateTime date12 = baseDate.AddDays(8).AddHours(15);

        // Movie 3
        DateTime date13 = baseDate.AddDays(9).AddHours(15);
        DateTime date14 = baseDate.AddDays(10).AddHours(15);
        DateTime date15 = baseDate.AddDays(11).AddHours(15);
        DateTime date16 = baseDate.AddDays(12).AddHours(15);
        DateTime date17 = baseDate.AddDays(13).AddHours(15);
        DateTime date18 = baseDate.AddDays(14).AddHours(15);

        List<TimetableModel> timetablesList = [
            // Movie 1
            new TimetableModel(1, 1, 1, TimetablesLogic.ConvertDateToUnixTime(date1)),
            new TimetableModel(2, 1, 1, TimetablesLogic.ConvertDateToUnixTime(date2)),
            new TimetableModel(3, 1, 1, TimetablesLogic.ConvertDateToUnixTime(date3)),
            new TimetableModel(4, 1, 1, TimetablesLogic.ConvertDateToUnixTime(date4)),
            new TimetableModel(5, 1, 1, TimetablesLogic.ConvertDateToUnixTime(date5)),
            new TimetableModel(6, 1, 1, TimetablesLogic.ConvertDateToUnixTime(date6)),

            // Movie 2
            new TimetableModel(7, 2, 2, TimetablesLogic.ConvertDateToUnixTime(date7)),
            new TimetableModel(8, 2, 2, TimetablesLogic.ConvertDateToUnixTime(date8)),
            new TimetableModel(9, 2, 2, TimetablesLogic.ConvertDateToUnixTime(date9)),
            new TimetableModel(10, 2, 2, TimetablesLogic.ConvertDateToUnixTime(date10)),
            new TimetableModel(11, 2, 2, TimetablesLogic.ConvertDateToUnixTime(date11)),
            new TimetableModel(12, 2, 2, TimetablesLogic.ConvertDateToUnixTime(date12)),

            // Movie 3
            new TimetableModel(13, 3, 3, TimetablesLogic.ConvertDateToUnixTime(date13)),
            new TimetableModel(14, 3, 3, TimetablesLogic.ConvertDateToUnixTime(date14)),
            new TimetableModel(15, 3, 3, TimetablesLogic.ConvertDateToUnixTime(date15)),
            new TimetableModel(16, 3, 3, TimetablesLogic.ConvertDateToUnixTime(date16)),
            new TimetableModel(17, 3, 3, TimetablesLogic.ConvertDateToUnixTime(date17)),
            new TimetableModel(18, 3, 3, TimetablesLogic.ConvertDateToUnixTime(date18)),
        ];

        foreach (TimetableModel timetable in timetablesList)
        {
            timetables.Write(timetable);
        }
        return Task.CompletedTask;
    }
}