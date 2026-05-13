using Dapper;

public class ReservationAccess : DefaultAccess
{
    protected override string Table { get; } = "Reservation";

    public override void CreateTable()
    {
        string sql = $@"
            CREATE TABLE IF NOT EXISTS {Table} (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                userId INTEGER NOT NULL,
                reservationDate INTEGER NOT NULL,
                totalPrice REAL NOT NULL,
                timeTableId INTEGER NOT NULL,
                seatId INTEGER NOT NULL,

                FOREIGN KEY (userId) REFERENCES Account(id),
                FOREIGN KEY (timeTableId) REFERENCES TimeTable(id),
                FOREIGN KEY (seatId) REFERENCES Seat(id)
            )";
        connection.Execute(sql);
    }
    public void Write(ReservationModel reservation)
    {
        string sql = $"INSERT INTO {Table} (userId, reservationDate, totalPrice, timeTableId, seatId) VALUES (@UserId, @ReservationDate, @TotalPrice, @TimeTableId, @SeatId)";
        connection.Execute(sql, reservation);
    }
    public List<ReservationModel> GetReservationsByUserId(long userId)
    {
        string sql = $"SELECT * FROM {Table} WHERE userId = @UserId";
        return connection.Query<ReservationModel>(sql, new { UserId = userId }).AsList();
    }

    public TimetableModel GetById(Int64 timetableId)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @TimetableId";
        return connection.QueryFirstOrDefault<TimetableModel>(sql, new { TimetableId = timetableId });
    }
}
