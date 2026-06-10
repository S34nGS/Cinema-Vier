using Dapper;

public class ConsumableOrderAccess : DefaultAccess, IAccess
{
    public static string Table { get; } = "ConsumableOrder";

    public override void CreateTable()
    {
        string sql = $@"
        CREATE TABLE IF NOT EXISTS {Table} (
			id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
			reservationId INTEGER NOT NULL,
			consumableId INTEGER NOT NULL,
			amount INTEGER NOT NULL,

            FOREIGN KEY (reservationId) REFERENCES Reservation(id),
            FOREIGN KEY (consumableId) REFERENCES MenuItems(id)
        );";
        connection.Execute(sql);
    }

    public void Write(ConsumableOrderModel order)
    {
        string sql = $@"
            INSERT INTO {Table} (reservationId, consumableId, amount)
            VALUES (@ReservationId, @ConsumableId, @Amount)";

        connection.Execute(sql, order);
    }

    public List<ConsumableOrderModel> GetByReservationId(Int64 reservationId)
    {
        string sql = $"SELECT * FROM {Table} WHERE reservationId = @ReservationId";
        return connection.Query<ConsumableOrderModel>(sql, new { ReservationId = reservationId }).ToList();
    }
}
