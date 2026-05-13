using Dapper;

public class RoomsAccess : DefaultAccess
{
    protected override string Table {get;} = "Room";

    public override void CreateTable()
    {
        string sql = $@"CREATE TABLE IF NOT EXISTS {Table} (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            screenType TEXT NOT NULL,
            soundTYPE TEXT NOT NULL,
            height INTEGER NOT NULL,
            width INTEGER NOT NULL
        );";
        connection.Execute(sql);
    }

    public void Write(RoomModel room)
    {
        string sql = $@"INSERT INTO {Table}
            (screenType, soundType, height, width)
            VALUES (@ScreenType, @SoundType, @Height, @Width)";
        connection.Execute(sql, room);
    }

    public void Update(RoomModel room)
    {
        string sql = $@"UPDATE {Table} 
            SET screenType = @ScreenType, soundType = @SoundType
            WHERE id = @Id";
        connection.Execute(sql, room);
    }

    public void Delete(RoomModel room)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        connection.Execute(sql, new { Id = room.Id });
    }

    public RoomModel GetById(Int64 id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return connection.QueryFirstOrDefault<RoomModel>(sql, new {Id = id});
    }
}