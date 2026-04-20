using Dapper;

public class ConsumeableAccess : DefaultAccess
{
	protected override string Table { get; } = "Consumable";

	public override void CreateTable()
	{
		string sql = $@"CREATE TABLE IF NOT EXISTS {Table} (
			id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
			name TEXT NOT NULL,
			price REAL NOT NULL,
			ageRating INTEGER NOT NULL
        );";
		connection.Execute(sql);
	}
}