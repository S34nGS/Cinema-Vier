using Dapper;

public class ConsumableAccess : DefaultAccess, IAccess
{
    public static string Table { get; } = "Consumable";

    public override void CreateTable()
    {
        string sql = $@"
		CREATE TABLE IF NOT EXISTS {Table} (
			id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
			name TEXT NOT NULL,
			price REAL NOT NULL,
			ageRating INTEGER NOT NULL
        );";
        connection.Execute(sql);
    }

    public void Write(ConsumableModel consumable)
    {
        string sql = $@"
            INSERT INTO {Table} (name, price, ageRating)
            VALUES (@Name, @Price, @AgeRating)";

        connection.Execute(sql, consumable);
    }

    public ConsumableModel GetById(Int64 id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return connection.QueryFirstOrDefault<ConsumableModel>(sql, new { Id = id });
    }
}
