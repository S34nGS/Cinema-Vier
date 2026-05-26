using System.Security.Cryptography.X509Certificates;

public class SeatSelection
{
    private SeatLogic _logic = new();
    private List<SeatModel> selectedSeats = [];
    private List<SeatModel> unavailableSeats = [];
    private Dictionary<string, Int64> Location = [];
    private SeatModel[,] Seats = new SeatModel[0,0];

    public List<SeatModel> Start(TimetableModel timetable)
    {
        Seats = _logic.GetSeatsInLayoutArray(timetable.RoomId);
        unavailableSeats = _logic.GetUnavailableSeatsByTimetableId(timetable.Id);

        SeatModel first_seat = Seats.Cast<SeatModel>().First(x => x != null);

        Location = new()
        {
            { "row", first_seat.Row },
            { "col", first_seat.SeatNumber }
        };

        ConsoleKey input = default;

        do
        {
            Console.Clear();

            Console.WriteLine(@$"
Selected seats = {selectedSeats.Count}
Select: Space
Move: Arrows
Confirm: Enter
            ");

            if (Seats.Length < 300)
            {
                WriteSmallRoom();
            }
            else if (Seats.Length < 500)
            {
                WriteMediumRoom();
            }
            else
            {
                WriteBigRoom();
            }

            input = Console.ReadKey().Key;

            // TODO: make sure the user cant get out of the map
            if (UiHelper.IsDownKey(input) && Location["row"] < Seats.GetLength(0))
            {
                Location["row"]++;
            }

            else if (UiHelper.IsUpKey(input) && Location["row"] > 1 )
            {
                Location["row"]--;
            }

            else if (UiHelper.IsRightKey(input) && Location["col"] < Seats.GetLength(1))
            {
                Location["col"]++;
            }

            else if (UiHelper.IsLeftKey(input) && Location["col"] > 1)
            {
                Location["col"]--;
            }

            else if (input == ConsoleKey.Spacebar && unavailableSeats.FirstOrDefault(x => x.Row == Location["row"] && x.SeatNumber == Location["col"]) == null)
            {
                ToggleSeat(Location["row"], Location["col"]);   
            }
        } while(input != ConsoleKey.Enter);

        return selectedSeats;
    }

    public void WriteSmallRoom()
    {
        for (int row = 0; row < Seats.GetLength(0); row++)
        {
            Console.Write($"{(Seats.GetLength(0) - row).ToString("D2")}  ");
            for (int col = 0; col < Seats.GetLength(1); col++)
            {
                SeatModel? seat = Seats[row, col];
                if (seat == null)
                {
                    Console.Write(" ");
                }
                else
                {
                    PrintSeat(
                        seat,
                        unavailableSeats.FirstOrDefault(x => x.Id == seat.Id) == null,
                        selectedSeats.FirstOrDefault(x => x.Id == seat.Id) != null,
                        seat.Row == Location["row"] && seat.SeatNumber == Location["col"]
                    );
                }
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }

    public void WriteMediumRoom()
    {
        for (int row = 0; row < Seats.GetLength(0); row++)
        {
            Console.Write($"{(Seats.GetLength(0) - row).ToString("D2")}  ");
            for (int col = 0; col < Seats.GetLength(1); col++)
            {
                SeatModel? seat = Seats[row, col];
                if (seat == null)
                {
                    Console.Write(" ");
                }
                else
                {
                    PrintSeat(
                        seat,
                        unavailableSeats.FirstOrDefault(x => x.Id == seat.Id) == null,
                        selectedSeats.FirstOrDefault(x => x.Id == seat.Id) != null,
                        seat.Row == Location["row"] && seat.SeatNumber == Location["col"]
                    );
                    if(seat.SeatNumber == 6 || seat.SeatNumber == 12)
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }

    public void WriteBigRoom()
    {
        for (int row = 0; row < Seats.GetLength(0); row++)
        {
            if(row == 6 || row == 11)
            {
                Console.WriteLine();
            }
            Console.Write($"{(Seats.GetLength(0) - row).ToString("D2")}  ");
            for (int col = 0; col < Seats.GetLength(1); col++)
            {
                SeatModel? seat = Seats[row, col];
                if (seat == null)
                {
                    Console.Write(" ");
                }
                else
                {
                    PrintSeat(
                        seat,
                        unavailableSeats.FirstOrDefault(x => x.Id == seat.Id) == null,
                        selectedSeats.FirstOrDefault(x => x.Id == seat.Id) != null,
                        seat.Row == Location["row"] && seat.SeatNumber == Location["col"]
                    );
                    if(seat.SeatNumber == 11 || seat.SeatNumber == 19)
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }

    public static void PrintSeat(SeatModel seat, bool available = true, bool selected = false, bool hovering = false)
    {

        if (seat.SeatPriority == 3)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if (seat.SeatPriority == 2)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        else if (seat.SeatPriority == 1)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        if(hovering)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
        }

        if(available)
        {
            Console.Write("☐");
        }
        else if (selected)
        {
            Console.Write("■");
        }
        else
        {
            Console.Write("☒");
        }

        Console.ResetColor();
    }

    // TODO: fix this
    public void ToggleSeat(Int64 row, Int64 col)
    {
        if(selectedSeats.FirstOrDefault(x => x.Row == row && x.SeatNumber == col) == null)
        {
            selectedSeats.Add(Seats.Cast<SeatModel>().First(x => x.Row == row && x.SeatNumber == col));
        }
        else
        {
            selectedSeats = selectedSeats.Where(x => x.Row != row && x.SeatNumber != col).ToList();
        }
    }
}