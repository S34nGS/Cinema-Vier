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
                reservationDate TEXT NOT NULL,
                totalPrice REAL NOT NULL,
                timeTableId INTEGER NOT NULL,
                FOREIGN KEY (userId) REFERENCES Account(id),
                FOREIGN KEY (timeTableId) REFERENCES TimeTable(id)
            )";
        connection.Execute(sql);
    }
    public void Write(ReservationModel reservation)
    {
        string sql = $"INSERT INTO {Table} (userId, reservationDate, totalPrice, timeTableId) VALUES (@UserId, @ReservationDate, @TotalPrice, @TimeTableId)";
        connection.Execute(sql, reservation);
    }
    public List<ReservationModel> GetReservationsByUserId(long userId)
    {
        string sql = $"SELECT * FROM {Table} WHERE userId = @UserId";
        return connection.Query<ReservationModel>(sql, new { UserId = userId }).AsList();
    }
}
