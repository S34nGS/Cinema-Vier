using Dapper;

public class ReservationsAccess : DefaultAccess
{
    protected override string Table { get; } = "Reservations";
    public ReservationsAccess()
    {
        connection.Open();
        CreateTable();
    }
    protected override void CreateTable()
    {
        string sql = $@"CREATE TABLE IF NOT EXISTS {Table} (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        userId INTEGER NOT NULL,
                        reservationDate TEXT NOT NULL,
                        totalPrice REAL NOT NULL,
                        timeTableId INTEGER NOT NULL
                    )";
        connection.Execute(sql);
    }
    public List<ReservationModel> GetReservationsByUserId(int userId)
    {
        string sql = $"SELECT * FROM {Table} WHERE userId = @UserId";
        return connection.Query<ReservationModel>(sql, new { UserId = userId }).AsList();
    }
}