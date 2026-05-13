public static class RoomsLogic
{
    private static RoomsAccess _access = new();

    public static RoomModel GetRoomById(Int64 id)
    {
        return _access.GetById(id);
    }
}