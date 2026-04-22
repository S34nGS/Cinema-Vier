public static class RoomsLogic
{
    private static RoomsAccess _access = new();

    public static RoomModel GetRoomById(int id)
    {
        return _access.GetById(id);
    }
}