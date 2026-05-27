public class SeatLogic
{
    private SeatAccess _access = new();

    public List<SeatModel> GetSeatsByRoomId(Int64 room)
    {
        return _access.GetAllSeatsByRoomId(room);
    }

    public static SeatModel GetById(long seatId)
    {
        SeatAccess access = new SeatAccess();
        return access.GetById(seatId);
    }
}