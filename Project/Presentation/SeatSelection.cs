public static class SeatSelection
{
    private static SeatLogic _logic = new();
    public static List<SeatModel> Start(Int64 roomId = 1, List<SeatModel> unavailableSeats = null)
    {
        List<SeatModel> seats = _logic.GetSeatsByRoomId(roomId);
        RoomModel room = RoomsLogic.GetRoomById(roomId);

        unavailableSeats ??= seats.Where(seat => seat.Row == 14).ToList();
        List<(Int64 Row, Int64 Seat)> unavailableSeatKeys = unavailableSeats
            .Select(seat => (seat.Row, seat.SeatNumber))
            .ToList();

        Dictionary<string, int> Coordinates = new();
        Coordinates["x"] = 1;
        Coordinates["y"] = 1;

        List<SeatModel> selectedSeats = new();
        bool hasActiveInput = true;

        while (hasActiveInput)
        {
            Console.Clear();
            WriteScreen(seats, selectedSeats, unavailableSeatKeys, Coordinates, room);

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
                ToggleSeat(seats, selectedSeats, unavailableSeatKeys, Coordinates["y"], Coordinates["x"]);
            }
            else if (keyPressed == ConsoleKey.Enter)
            {
                return selectedSeats;
            }
        }

        return new List<SeatModel>();
    }

    public static void WriteScreen(
        List<SeatModel> seats,
        List<SeatModel> selectedSeats,
        List<(Int64 Row, Int64 Seat)> unavailableSeatKeys,
        Dictionary<string, int> Coordinates,
        RoomModel room)
    {
        Console.WriteLine("Seat Selection");
        Console.WriteLine($"Room {room.Id} | {room.ScreenType} | {room.SoundType}");
        Console.WriteLine("Arrows/HJKL: move  Space: toggle Enter: confirm");
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
            bool isSelected = IsSeatSelected(selectedSeats, seat.Row, seat.SeatNumber);
            bool isUnavailable = unavailableSeatKeys.Contains((seat.Row, seat.SeatNumber));
            WriteSeatCell(isSelected, isCursor, isUnavailable);
            Console.Write(" ");
        }

        Console.WriteLine();
        Console.WriteLine();
    }

    private static void WriteSeatCell(bool isSelected, bool isCursor, bool isUnavailable)
    {
        if (isUnavailable)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("■");
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

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

    private static void ToggleSeat(
        List<SeatModel> seats,
        List<SeatModel> selectedSeats,
        List<(Int64 Row, Int64 Seat)> unavailableSeatKeys,
        Int64 row,
        Int64 seat)
    {
        if (unavailableSeatKeys.Contains((row, seat)))
        {
            return;
        }

        int existingIndex = selectedSeats.FindIndex(s => s.Row == row && s.SeatNumber == seat);
        if (existingIndex >= 0)
        {
            selectedSeats.RemoveAt(existingIndex);
            return;
        }

        SeatModel? match = seats.FirstOrDefault(s => s.Row == row && s.SeatNumber == seat);
        if (match != null)
        {
            selectedSeats.Add(match);
        }
    }

    private static bool IsSeatSelected(List<SeatModel> selectedSeats, Int64 row, Int64 seat)
    {
        return selectedSeats.Any(s => s.Row == row && s.SeatNumber == seat);
    }

}
