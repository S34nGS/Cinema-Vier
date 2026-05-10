public static class SeatSelection
{
    private static SeatLogic _logic = new();
    public static void Start()
    {
        Console.Clear();
        List<SeatModel> seats = _logic.GetSeatsByRoomId(1);
        Int64 current_row = 1;

        Console.WriteLine("-- Screen --");
        foreach(SeatModel seat in seats)
        {
            if(seat.Row > current_row)
            {
                Console.Write(Environment.NewLine);
                current_row = seat.Row;
            }

            Console.Write("□ ");
        }
    }
}
