using Dapper;

public class ReservationAccess : DefaultAccess
{
    protected override string Table { get; } = "Reservation";

    public override void CreateTable()
    {
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
