using Dapper;

public class ReservationAccess : DefaultAccess
{
	protected override string Table { get; } = "Reservation";

	public override void CreateTable()
	{
		string sql = $@"CREATE TABLE IF NOT EXISTS {Table} (
			id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
			userId INTEGER NOT NULL,
			reservationDate INTEGER NOT NULL,
			totalPrice REAL NOT NULL,
			timetableId INTEGER NOT NULL,

            FOREIGN KEY (userId) REFERENCES Account(id)
        );";
		connection.Execute(sql);
// CREATE TABLE Reservation(
// );
	}
}

