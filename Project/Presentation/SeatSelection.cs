public static class SeatSelection
{
    private static SeatLogic _logic = new();
    public static List<SeatModel> Start(int roomId = 1)
    {
        List<SeatModel> seats = _logic.GetSeatsByRoomId(roomId);
        RoomModel room = RoomsLogic.GetRoomById(roomId);

        Dictionary<string, int> Coordinates = new();
        Coordinates["x"] = 1;
        Coordinates["y"] = 1;

        List<(Int64 Row, Int64 Seat)> selectedSeats = new();
        bool hasActiveInput = true;

        while (hasActiveInput)
        {
            Console.Clear();
            WriteScreen(seats, selectedSeats, Coordinates, room);

            ConsoleKey keyPressed = Console.ReadKey(true).Key;

            if (UiHelper.IsLeftKey(keyPressed) && Coordinates["x"] > 1)
            {
                Coordinates["x"]--;
            }
            else if (UiHelper.IsRightKey(keyPressed) && Coordinates["x"] < room.Width)
            {
                Coordinates["x"]++;
            }
            else if (UiHelper.IsDownKey(keyPressed) && Coordinates["y"] < room.Height)
            {
                Coordinates["y"]++;
            }
            else if (UiHelper.IsUpKey(keyPressed) && Coordinates["y"] > 1)
            {
                Coordinates["y"]--;
            }
            else if (keyPressed == ConsoleKey.Spacebar)
            {
                ToggleSeat(selectedSeats, Coordinates["y"], Coordinates["x"]);
            }
            else if (keyPressed == ConsoleKey.Enter)
            {
                return selectedSeats;
            }
            else if (keyPressed == ConsoleKey.S)
            {
                ShowSelectionSummary(selectedSeats);
            }
        }
    }

    public static void WriteScreen(
        List<SeatModel> seats,
        List<(Int64 Row, Int64 Seat)> selectedSeats,
        Dictionary<string, int> Coordinates,
        RoomModel room)
    {
        Console.WriteLine("Seat Selection");
        Console.WriteLine($"Room {room.Id} | {room.ScreenType} | {room.SoundType}");
        Console.WriteLine("Arrows/HJKL: move  Space/Enter: toggle  S: summary  Esc: back");
        Console.WriteLine($"Selected: {selectedSeats.Count}");
        Console.WriteLine();

        Int64 currentRow = 0;

        foreach (SeatModel seat in seats)
        {
            if (seat.Row != currentRow)
            {
                if (currentRow != 0)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                }

                currentRow = seat.Row;
                Console.Write(currentRow < 10 ? $" {currentRow}" : currentRow.ToString());
                Console.Write("  ");
            }

            bool isCursor = seat.Row == Coordinates["y"] && seat.SeatNumber == Coordinates["x"];
            bool isSelected = selectedSeats.Contains((seat.Row, seat.SeatNumber));
            WriteSeatCell(isSelected, isCursor);
            Console.Write(" ");
        }

        Console.WriteLine();
        Console.WriteLine();
    }

    private static void WriteSeatCell(bool isSelected, bool isCursor)
    {
        if (isCursor)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(isSelected ? "█" : "█");
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        if (isSelected)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("■");
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        Console.Write("░");
    }

    private static void ToggleSeat(List<(Int64 Row, Int64 Seat)> selectedSeats, Int64 row, Int64 seat)
    {
        var key = (row, seat);
        int existingIndex = selectedSeats.IndexOf(key);
        if (existingIndex >= 0)
        {
            selectedSeats.RemoveAt(existingIndex);
            return;
        }

        selectedSeats.Add(key);
    }

    private static void ShowSelectionSummary(List<(Int64 Row, Int64 Seat)> selectedSeats)
    {
        Console.Clear();
        Console.WriteLine("Selected seats");
        Console.WriteLine();

        if (selectedSeats.Count == 0)
        {
            Console.WriteLine("No seats selected.");
        }
        else
        {
            foreach ((Int64 row, Int64 seat) in selectedSeats.OrderBy(item => item.Row).ThenBy(item => item.Seat))
            {
                Console.WriteLine($"Row {row}, Seat {seat}");
            }
        }

        Console.WriteLine();
        UiHelper.HoldUser();
    }
}
