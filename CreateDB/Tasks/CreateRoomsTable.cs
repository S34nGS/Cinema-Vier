public static class CreateRoomsTable
{
    public static Task Execute()
    {
        RoomsAccess rooms = new();
        rooms.CreateTable();

        List<RoomModel> roomsList = [
            new RoomModel(1, "Standard", "7.1 Surround Sound", 14, 12),
            new RoomModel(2, "IMAX", "IMAX Sound System", 20, 30),
            new RoomModel(3, "Dolby Cinema", "Dolby Atmos", 19, 18),
        ];

        foreach (RoomModel room in roomsList)
        {
            rooms.Write(room);
        }
        return Task.CompletedTask;
    }
}