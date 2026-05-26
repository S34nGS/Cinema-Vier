// public static class SeatSelection
// {
//     private static SeatLogic _logic = new();
//     public static List<SeatModel> Start(TimetableModel timetable)
//     {
//         List<SeatModel> seats = _logic.GetSeatsByRoomId(timetable.RoomId);
//         // RoomModel room = RoomsLogic.GetRoomById(roomId);


//         Dictionary<string, int> Coordinates = new();
//         Coordinates["x"] = 1;
//         Coordinates["y"] = 1;

//         List<SeatModel> selectedSeats = new();
//         bool hasActiveInput = true;

//         while (hasActiveInput)
//         {
//             Console.Clear();
//             WriteScreen(seats, selectedSeats, unavailableSeatKeys, Coordinates, room);

//             ConsoleKey keyPressed = Console.ReadKey(true).Key;

//             if (UiHelper.IsLeftKey(keyPressed) && Coordinates["x"] > 1)
//             {
//                 Coordinates["x"]--;
//             }
//             else if (UiHelper.IsRightKey(keyPressed) && Coordinates["x"] < room.Width)
//             {
//                 Coordinates["x"]++;
//             }
//             else if (UiHelper.IsDownKey(keyPressed) && Coordinates["y"] < room.Height)
//             {
//                 Coordinates["y"]++;
//             }
//             else if (UiHelper.IsUpKey(keyPressed) && Coordinates["y"] > 1)
//             {
//                 Coordinates["y"]--;
//             }
//             else if (keyPressed == ConsoleKey.Spacebar)
//             {
//                 ToggleSeat(seats, selectedSeats, unavailableSeatKeys, Coordinates["y"], Coordinates["x"]);
//             }
//             else if (keyPressed == ConsoleKey.Enter)
//             {
//                 return selectedSeats;
//             }
//         }

//         return new List<SeatModel>();
//     }

//     public static void WriteScreen(
//         List<SeatModel> seats,
//         List<SeatModel> selectedSeats,
//         List<(Int64 Row, Int64 Seat)> unavailableSeatKeys,
//         Dictionary<string, int> Coordinates,
//         RoomModel room)
//     {
//         Console.WriteLine("Seat Selection");
//         Console.WriteLine($"Room {room.Id} | {room.ScreenType} | {room.SoundType}");
//         Console.WriteLine("Arrows/HJKL: move  Space: toggle Enter: confirm");
//         Console.WriteLine($"Selected: {selectedSeats.Count}");
//         Console.WriteLine();

//         Int64 currentRow = 0;

//         foreach (SeatModel seat in seats)
//         {
//             if (seat.Row != currentRow)
//             {
//                 if (currentRow != 0)
//                 {
//                     Console.WriteLine();
//                     Console.WriteLine();
//                 }

//                 currentRow = seat.Row;
//                 Console.Write(currentRow < 10 ? $" {currentRow}" : currentRow.ToString());
//                 Console.Write("  ");
//             }

//             bool isCursor = seat.Row == Coordinates["y"] && seat.SeatNumber == Coordinates["x"];
//             bool isSelected = IsSeatSelected(selectedSeats, seat.Row, seat.SeatNumber);
//             bool isUnavailable = unavailableSeatKeys.Contains((seat.Row, seat.SeatNumber));
//             WriteSeatCell(isSelected, isCursor, isUnavailable);
//             Console.Write(" ");
//         }

//         Console.WriteLine();
//         Console.WriteLine();
//     }

//     private static void WriteSeatCell(bool isSelected, bool isCursor, bool isUnavailable)
//     {
//         if (isUnavailable)
//         {
//             Console.ForegroundColor = ConsoleColor.Red;
//             Console.Write("■");
//             Console.ForegroundColor = ConsoleColor.White;
//             return;
//         }

//         if (isCursor)
//         {
//             Console.ForegroundColor = ConsoleColor.Green;
//             Console.Write(isSelected ? "█" : "█");
//             Console.ForegroundColor = ConsoleColor.White;
//             return;
//         }

//         if (isSelected)
//         {
//             Console.ForegroundColor = ConsoleColor.Yellow;
//             Console.Write("■");
//             Console.ForegroundColor = ConsoleColor.White;
//             return;
//         }

//         Console.Write("░");
//     }

//     private static void ToggleSeat(
//         List<SeatModel> seats,
//         List<SeatModel> selectedSeats,
//         List<(Int64 Row, Int64 Seat)> unavailableSeatKeys,
//         Int64 row,
//         Int64 seat)
//     {
//         if (unavailableSeatKeys.Contains((row, seat)))
//         {
//             return;
//         }

//         int existingIndex = selectedSeats.FindIndex(s => s.Row == row && s.SeatNumber == seat);
//         if (existingIndex >= 0)
//         {
//             selectedSeats.RemoveAt(existingIndex);
//             return;
//         }

//         SeatModel? match = seats.FirstOrDefault(s => s.Row == row && s.SeatNumber == seat);
//         if (match != null)
//         {
//             selectedSeats.Add(match);
//         }
//     }

//     private static bool IsSeatSelected(List<SeatModel> selectedSeats, Int64 row, Int64 seat)
//     {
//         return selectedSeats.Any(s => s.Row == row && s.SeatNumber == seat);
//     }

// }

using System.Security.Cryptography.X509Certificates;

public static class SeatSelection
{
    private static SeatLogic _logic = new();
    public static List<SeatModel> Start(TimetableModel timetable)
    {
        List<SeatModel> selectedSeats = [];
        SeatModel[,] seats = _logic.GetSeatsInLayoutArray(timetable.RoomId);
        if (seats.Length < 300)
        {
            WriteSmallRoom(seats);
        }
        else if (seats.Length < 500)
        {
            WriteMediumRoom(seats);
        }
        else
        {
            WriteBigRoom(seats);
        }

        UiHelper.HoldUser();
        return selectedSeats;
    }

    public static void WriteSmallRoom(SeatModel[,] seats)
    {
        string output = "";
        for (int row = 0; row < seats.GetLength(0); row++)
        {
            for (int col = 0; col < seats.GetLength(1); col++)
            {
                if (seats[row, col] == null)
                {
                    output += " ";
                }
                else
                {
                    output += "░";
                }
                output += " ";
            }
            output += Environment.NewLine + Environment.NewLine;
        }

        Console.WriteLine(output);
    }

    public static void WriteMediumRoom(SeatModel[,] seats)
    {
        string output = "";
        for (int row = 0; row < seats.GetLength(0); row++)
        {
            for (int col = 0; col < seats.GetLength(1); col++)
            {
                if (seats[row, col] == null)
                {
                    output += " ";
                }
                else
                {
                    output += "░";
                }
                output += " ";
            }
            output += Environment.NewLine + Environment.NewLine;
        }

        Console.WriteLine(output);
    }

    public static void WriteBigRoom(SeatModel[,] seats)
    {
        string output = "";
        for (int row = 0; row < seats.GetLength(0); row++)
        {
            for (int col = 0; col < seats.GetLength(1); col++)
            {
                SeatModel? seat = seats[row, col];
                if (seat == null)
                {
                    output += " ";
                }
                else
                {
                    if (seat.SeatPriority == 3)
                    {
                        output += "░".Colorize("red");
                    }
                    else if (seat.SeatPriority == 2)
                    {
                        output += "░".Colorize("yellow");
                    }
                    else if (seat.SeatPriority == 1)
                    {
                        output += "░".Colorize("blue");
                    }
                    // output += "░";
                    Console.ResetColor();
                }
                output += " ";
            }
            output += Environment.NewLine + Environment.NewLine;
        }

        Console.WriteLine(output);
    }

    public static string Colorize(this string str, string color)
    {
        return color.ToLower() switch
        {
            "red" => $@"\e[0;31m${str}",
            "orange" => $@"\e[0;33m{str}",
            "yellow" => $@"\e[0;33m{str}",
            "blue" => $@"\e[0;94m{str}",
            _ => str
        };
    }
}