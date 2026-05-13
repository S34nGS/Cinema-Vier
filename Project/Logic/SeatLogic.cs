public class SeatLogic
{
    private SeatAccess _access = new();

    public List<SeatModel> GetSeatsByRoomId(Int64 room)
    {
        return _access.GetAllSeatsByRoomId(room);
    }
}