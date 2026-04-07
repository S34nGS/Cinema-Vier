using Dapper;

// This class handles database operations for reservations
public class ReservationsAccess : DefaultAccess
{
    // Table name in database
    protected override string Table { get; } = "Reservation";

    // Create table if it does not exist
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

    // Insert a new reservation into the database
    public void Write(ReservationModel reservation)
    {
        string sql = $@"
            INSERT INTO {Table} (userId, reservationDate, totalPrice, timeTableId)
            VALUES (@UserId, @ReservationDate, @TotalPrice, @TimeTableId)";

        connection.Execute(sql, reservation);
    }

    // Get all reservations by user id
    public List<ReservationModel> GetReservationsByUserId(int userId)
    {
        string sql = $"SELECT * FROM {Table} WHERE userId = @UserId";
        return connection.Query<ReservationModel>(sql, new { UserId = userId }).AsList();
    }
}