public class SeatModel
{
    public Int64 Id { get; set; }
    public Int64 RoomId { get; set; }
    public Int64 Row { get; set; }
    public Int64 SeatNumber { get; set; }
    public Int64 SeatPriority { get; set; }

    public SeatModel(Int64 id, Int64 roomId, Int64 row, Int64 seatNumber, Int64 seatPriority)
    {
        Id = id;
        RoomId = roomId;
        Row = row;
        SeatNumber = seatNumber;
        SeatPriority = seatPriority;
    }
}