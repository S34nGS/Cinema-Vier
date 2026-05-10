public class SeatModel
{

    public Int64 Id, RoomId, Row, SeatNumber, SeatPriority;

    public SeatModel(Int64 id, Int64 roomId, Int64 row, Int64 seatNumber, Int64 seatPriority)
    {
        Id = id;
        RoomId = roomId;
        Row = row;
        SeatNumber = seatNumber;
        SeatPriority = seatPriority;
    }
}