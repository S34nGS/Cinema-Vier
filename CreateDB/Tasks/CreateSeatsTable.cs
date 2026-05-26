public static class CreateSeatsTable
{
    public static Task Execute()
    {

        SeatAccess seats = new();
        seats.CreateTable();

        InsertSmallLayoutSeats();
        InsertMediumLayoutSeats();
        InsertLargeLayoutSeats();

        return Task.CompletedTask;
    }

    public static Task InsertLargeLayoutSeats()
    {

        return Task.CompletedTask;
    }

    public static Task InsertMediumLayoutSeats()
    {
        SeatAccess seats = new();
        int[,] layout = {
            { 0, 1, 1, 1, 1, 1,   1, 1, 1, 1, 1, 1,    1, 1, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 1, 1,   1, 1, 1, 1, 1, 1,    1, 1, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 1, 2,   2, 2, 2, 2, 2, 2,    2, 1, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 1, 2,   2, 2, 2, 2, 2, 2,    2, 1, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 2, 2,   2, 2, 2, 2, 2, 2,    2, 2, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 2, 2,   2, 2, 2, 2, 2, 2,    2, 2, 1, 1, 1, 0 },
            { 1, 1, 1, 2, 2, 2,   2, 2, 3, 3, 2, 2,    2, 2, 2, 1, 1, 1 },
            { 1, 1, 1, 2, 2, 2,   2, 3, 3, 3, 3, 2,    2, 2, 2, 1, 1, 1 },
            { 1, 1, 2, 2, 2, 2,   2, 3, 3, 3, 3, 2,    2, 2, 2, 2, 1, 1 },
            { 1, 1, 2, 2, 2, 2,   3, 3, 3, 3, 3, 3,    2, 2, 2, 2, 1, 1 },
            { 1, 1, 2, 2, 2, 2,   3, 3, 3, 3, 3, 3,    2, 2, 2, 2, 1, 1 },
            { 0, 1, 1, 2, 2, 2,   2, 3, 3, 3, 3, 2,    2, 2, 2, 1, 1, 0 },
            { 0, 1, 1, 1, 2, 2,   2, 2, 3, 3, 2, 2,    2, 2, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 1, 2,   2, 2, 2, 2, 2, 2,    2, 1, 1, 1, 1, 0 },
            { 0, 0, 1, 1, 1, 1,   2, 2, 2, 2, 2, 2,    1, 1, 1, 1, 0, 0 },
            { 0, 0, 1, 1, 1, 1,   2, 2, 2, 2, 2, 2,    1, 1, 1, 1, 0, 0 },
            { 0, 0, 1, 1, 1, 1,   1, 1, 1, 1, 1, 1,    1, 1, 1, 1, 0, 0 },
            { 0, 0, 0, 1, 1, 1,   1, 1, 1, 1, 1, 1,    1, 1, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 1, 1,   1, 1, 1, 1, 1, 1,    1, 1, 1, 0, 0, 0 },
        };

        for (int y_axis = 0; y_axis < layout.GetLength(0); y_axis++)
        {
            for (int x_axis = 0; x_axis < layout.GetLength(1); x_axis++)
            {
                if (layout[y_axis, x_axis] == 0)
                {
                    continue;
                }
                SeatModel seat = new(-1, 2, y_axis + 1, x_axis + 1, layout[y_axis, x_axis]);
                seats.Write(seat);
            }
        }

        return Task.CompletedTask;
    }
    public static Task InsertSmallLayoutSeats()
    {
        SeatAccess seats = new();
        int[,] layout = {
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
            { 1, 1, 1, 1, 1, 2, 2, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 2, 2, 2, 2, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 2, 3, 3, 2, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 2, 3, 3, 2, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 2, 3, 3, 2, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 2, 3, 3, 2, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 2, 2, 2, 2, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 2, 2, 1, 1, 1, 1, 1 },
            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
        };

        for (int y_axis = 0; y_axis < layout.GetLength(0); y_axis++)
        {
            for (int x_axis = 0; x_axis < layout.GetLength(1); x_axis++)
            {
                if (layout[y_axis, x_axis] == 0)
                {
                    continue;
                }
                SeatModel seat = new(-1, 1, y_axis + 1, x_axis + 1, layout[y_axis, x_axis]);
                seats.Write(seat);
            }
        }

        return Task.CompletedTask;
    }
}