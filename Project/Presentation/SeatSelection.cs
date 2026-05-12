public static class SeatSelection
{
    private static SeatLogic _logic = new();
    public static void Start(int roomId = 1)
    {

        List<SeatModel> seats = _logic.GetSeatsByRoomId(roomId);
        Dictionary<string, int> Coordinates = new();
        Coordinates["x"] = 1;
        Coordinates["y"] = 1;
        bool has_active_input = true;
        do
        {
            Console.Clear();

            WriteScreen(seats, Coordinates);
            ConsoleKey keyPressed = Console.ReadKey().Key;

            if(keyPressed == ConsoleKey.H && Coordinates["x"] > 1)
            {
                Coordinates["x"]--;
            }
            else if(keyPressed == ConsoleKey.L)
            {
                Coordinates["x"]++;
            }
            else if(keyPressed == ConsoleKey.J)
            {
                Coordinates["y"]++;
            }
            else if(keyPressed == ConsoleKey.K)
            {
                Coordinates["y"]--;
            }
        } while(has_active_input);

    }

    public static void WriteScreen(List<SeatModel> seats, Dictionary<string, int> Coordinates)
    {
        Int64 current_row = 1;

        foreach(SeatModel seat in seats)
        {
            if(seat.Row > current_row)
            {
                Console.WriteLine();
                Console.WriteLine();
                current_row = seat.Row;
            }

            if(seat.SeatNumber == 1)
            {
                Console.Write(seat.Row < 10 ? $" {seat.Row}" : seat.Row); 
                Console.Write("  ");
            }
            if(Coordinates["x"] == seat.SeatNumber && Coordinates["y"] == seat.Row)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("█");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Write("░");
            }
            Console.Write(" ");
        }
        Console.WriteLine();
    }
}
