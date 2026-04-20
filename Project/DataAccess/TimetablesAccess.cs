using Dapper;

public class TimetablesAccess : DefaultAccess
{
    protected override string Table { get; } = "Timetable";

    public override void CreateTable()
    {
        string sql = $@"CREATE TABLE IF NOT EXISTS {Table} (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            movieId INTEGER NOT NULL,
            roomId INTEGER NOT NULL,
            startTime INTEGER NOT NULL,
            FOREIGN KEY (movieId) REFERENCES Movie(id),
            FOREIGN KEY (roomId) REFERENCES Room(id)
        );";
        connection.Execute(sql);
    }

    public void Write(TimetableModel timetable)
    {
        string sql = $@"INSERT INTO {Table}
            (movieId, roomId, startTime) 
            VALUES (@MovieId, @RoomId, @StartTime)";
        connection.Execute(sql, timetable);
    }

    public void Update(TimetableModel timetable)
    {
        string sql = $@"UPDATE {Table}
            SET movieId = @MovieId, roomId = @RoomId, startTime = @StartTime
            WHERE id = @Id";
        connection.Execute(sql, timetable);
    }

    public void Delete(TimetableModel timetable)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        connection.Execute(sql, new { Id = timetable.Id });
    }
}