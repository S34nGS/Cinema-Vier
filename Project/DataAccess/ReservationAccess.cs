using Dapper;

public class ReservationsAccess : DefaultAccess
{
    protected override string Table { get; } = "Reservation";

    public override void CreateTable()
    {
    }
    public List<ReservationModel> GetReservationsByUserId(long userId)
    {
        string sql = $"SELECT * FROM {Table} WHERE userId = @UserId";
        return connection.Query<ReservationModel>(sql, new { UserId = userId }).AsList();
    }
}