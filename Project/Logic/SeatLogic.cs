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

    public SeatModel[,] GetSeatsInLayoutArray(Int64 room)
    {
        List<SeatModel> localSeats = GetSeatsByRoomId(room);

        Int64 maxRow = localSeats.Max(x => x.Row);
        Int64 maxSeatNr = localSeats.Max(x => x.SeatNumber);

        SeatModel[,] seats = new SeatModel[maxRow, maxSeatNr];

        foreach (SeatModel seat in localSeats)
        {
            seats[seat.Row - 1, seat.SeatNumber - 1] = seat;
        }

        return seats;
    }

    public List<SeatModel> GetUnavailableSeatsByTimetableId(Int64 timetableId)
    {
        return _access.GetTakenSeatsByTimetableId(timetableId);
    }

    public static (Int64 RoomId, Int64 MaxRow, Int64 MaxSeatNumber) GetRoomSeatInfo(Int64 roomId)
    {
        SeatAccess access = new SeatAccess();
        return access.GetRoomSeatInfo(roomId);
    }
}
