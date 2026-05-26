public static class CreateRoomsTable
{
    public static Task Execute()
    {
        RoomsAccess rooms = new();
        rooms.CreateTable();

        List<RoomModel> roomsList = [
            new RoomModel(1, "Standard", "7.1 Surround Sound"),
            new RoomModel(2, "Dolby Cinema", "Dolby Atmos"),
            new RoomModel(3, "IMAX", "IMAX Sound System"),
        ];

        foreach (RoomModel room in roomsList)
        {
            rooms.Write(room);
        }
        return Task.CompletedTask;
    }
}