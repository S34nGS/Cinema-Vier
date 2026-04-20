using Dapper;

public class SeatAccess : DefaultAccess
{
	protected override string Table { get; } = "Seat";

	public override void CreateTable()
	{
		string sql = $@"CREATE TABLE IF NOT EXISTS {Table} (
			id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
			roomId INTEGER NOT NULL,
			row INTEGER NOT NULL,
			seatNumber INTEGER NOT NULL,
			seatPriority INTEGER NOT NULL
		);";
		connection.Execute(sql);
	}
}