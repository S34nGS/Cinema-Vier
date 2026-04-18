// CREATE TABLE IF NOT EXISTS seat (
// 	id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
// 	roomId INTEGER NOT NULL,
// 	"row" INTEGER NOT NULL,
// 	seatNumber INTEGER NOT NULL,
// 	seatPriority INTEGER NOT NULL
// );

using Dapper;

public class SeatAccess : DefaultAccess
{
    protected override string Table { get; } = "Seat";

    public override void CreateTable()
    {
        string sql = $@"CREATE TABLE IF NOT EXISTS {Table} 
            (id INTEGER PRIMARY KEY AUTOINCREMENT,
            roomId INTEGER NOT NULL,
            row INTEGER NOT NULL,
            seatNumber INTEGER NOT NULL,
            seatPriority INTEGER NOT NULL)";
        connection.Execute(sql);
    }

    public void Write(SeatModel seat)
    {
        string sql = $@"INSERT INTO {Table} 
            (roomId, row, seatNumber, seatPriority) 
            VALUES (@RoomId, @Row, @SeatNumber, @SeatPriority)";
        connection.Execute(sql, seat);
    }

    public List<SeatModel> GetAllSeats()
    {
        string sql = $"SELECT * FROM {Table}";
        return connection.Query<SeatModel>(sql).AsList();
    }

    public SeatModel GetBySeatId(int seatId)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return connection.QueryFirstOrDefault<SeatModel>(sql, new { Id = seatId });
    }

    public void Update(SeatModel seat)
    {
        string sql = $@"UPDATE {Table} 
            SET roomId = @RoomId, row = @Row, seatNumber = @SeatNumber, seatPriority = @SeatPriority
            WHERE id = @Id";
        connection.Execute(sql, seat);
    }

    public void Delete(SeatModel seat)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        connection.Execute(sql, new { Id = seat.Id });
    }
}