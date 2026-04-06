using Microsoft.Data.Sqlite;

using Dapper;


public class AccountsAccess : DefaultAccess
{
    protected override string Table { get; } = "Reservations";
    protected override void CreateTable()
    {
        string sql = $@"CREATE TABLE IF NOT EXISTS {Table} (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                userId INTEGER NOT NULL,
                reservationDate INTEGER NOT NULL,
                totalPrice REAL NOT NULL,
                timeTableId INTEGER NOT NULL,

                FOREIGN KEY (userId) REFERENCE Accounts(id) ON DELETE CASCADE,
                FOREIGN KEY (timeTableId) REFERENCES TimeTables(id) ON DELETE RESTRICT
            )";
        connection.Execute(sql);
    }

    public void Write(ReservationModel reservation)
    {
        string sql = $"INSERT INTO {Table} (userId, reservationDate, timeTableId) VALUES (@UserId, @ReservationDate, @TimeTableId)";
        connection.Execute(sql, account);
    }

    public List<ReservationModel> GetReservationsByUserId(int userId)
    {
        string sql = $"SELECT * FROM {Table} WHERE userId = @UserId";
        return connection.Query<ReservationModel>(sql, new { UserId = userId }).AsList();
    }

    public void Update(ReservationModel reservation)
    {
        string sql = $"UPDATE {Table} SET userId = @UserId, reservationDate = @ReservationDate, timeTableId = @TimeTableId WHERE id = @Id";
        connection.Execute(sql, reservation);
    }

    public void Delete(ReservationModel reservation)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        connection.Execute(sql, new { Id = reservation.Id });
    }
}