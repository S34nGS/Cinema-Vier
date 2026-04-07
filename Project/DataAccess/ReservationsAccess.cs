using Dapper;

public class ReservationsAccess : DefaultAccess
{
    protected override string Table { get; } = "Reservation";

    protected override void CreateTable()
    {
        string sql = $@"
            CREATE TABLE IF NOT EXISTS {Table} (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                userId INTEGER NOT NULL,
                reservationDate TEXT NOT NULL,
                totalPrice REAL NOT NULL,
                timeTableId INTEGER NOT NULL
            )";

        connection.Execute(sql);
    }

    public void Write(ReservationModel reservation)
    {
        string sql = $@"
            INSERT INTO {Table} (userId, reservationDate, totalPrice, timeTableId)
            VALUES (@UserId, @ReservationDate, @TotalPrice, @TimeTableId)";

        connection.Execute(sql, reservation);
    }

    public List<ReservationModel> GetReservationsByUserId(int userId)
    {
        string sql = $"SELECT * FROM {Table} WHERE userId = @UserId";
        return connection.Query<ReservationModel>(sql, new { UserId = userId }).AsList();
    }

    public void Update(ReservationModel reservation)
    {
        string sql = $@"
            UPDATE {Table}
            SET userId = @UserId,
                reservationDate = @ReservationDate,
                totalPrice = @TotalPrice,
                timeTableId = @TimeTableId
            WHERE id = @Id";

        connection.Execute(sql, reservation);
    }

    public void Delete(ReservationModel reservation)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        connection.Execute(sql, new { Id = reservation.Id });
    }
}