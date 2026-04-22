using Dapper;

public class ConsumableOrderAccess : DefaultAccess
{
	protected override string Table { get; } = "ConsumableOrder";

	public override void CreateTable()
	{
		// CREATE TABLE ConsumableOrder(
		// );

		string sql = $@"CREATE TABLE IF NOT EXISTS {Table} (
			id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
			reservationId INTEGER NOT NULL,
			consumableId INTEGER NOT NULL,
			amount INTEGER NOT NULL,

            FOREIGN KEY (reservationId) REFERENCES Reservation(id),
            FOREIGN KEY (consumableId) REFERENCES Consumable(id)
        );";
		connection.Execute(sql);
	}
}