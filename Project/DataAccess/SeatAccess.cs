using Dapper;

public class SeatAccess : DefaultAccess, IAccess
{
	public static string Table { get; } = "Seat";

	public override void CreateTable()
	{
		string sql = $@"CREATE TABLE IF NOT EXISTS {Table} (
			id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
			roomId INTEGER NOT NULL,
			row INTEGER NOT NULL,
			seatNumber INTEGER NOT NULL,
			seatPriority INTEGER NOT NULL,

            FOREIGN KEY (roomId) REFERENCES Room(id)
		);";
		connection.Execute(sql);
	}

	public void Write(SeatModel seat)
	{
		string sql = $@"INSERT INTO {Table} 
            (roomId, row, seatNumber, seatPriority)
            VALUES (@RoomId, @Row, @SeatNumber, @SeatPriority)";
		connection.Execute(sql, seat);
	}

	public List<SeatModel> GetAllSeatsByRoomId(Int64 roomId)
	{
		string sql = $"SELECT * FROM {Table} WHERE roomId = @RoomId";
		return connection.Query<SeatModel>(sql, new { RoomId = roomId }).AsList();
	}

	public List<SeatModel> GetTakenSeatsByTimetableId(Int64 timetableId)
	{
		string sql = $@"
			SELECT s.* FROM {Table} s
				JOIN SeatReservation sr ON s.id = sr.seatId
				WHERE sr.timetableId = @TimetableId
		";

		return connection.Query<SeatModel>(sql, new { TimetableId = timetableId }).AsList();
	}
	public SeatModel GetById(long seatId)
	{
    	string sql = $"SELECT * FROM {Table} WHERE id = @SeatId";
    	return connection.QueryFirstOrDefault<SeatModel>(sql, new { SeatId = seatId });
	}
}