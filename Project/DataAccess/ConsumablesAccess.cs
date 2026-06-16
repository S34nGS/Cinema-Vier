using Dapper;

public class ConsumablesAccess : DefaultAccess, IAccess
{
    public static string Table { get; } = "Consumables";

    public override void CreateTable()
    {
        // create menu items table
        string sql = $@"
            CREATE TABLE IF NOT EXISTS {Table} (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL,
                category TEXT NOT NULL,
                price REAL NOT NULL
            );";

        connection.Execute(sql);
    }

    public void Write(ConsumableModel item)
    {
        string sql = $@"
            INSERT INTO {Table} (Name, Category, Price)
            VALUES (@Name, @Category, @Price)
        ";

        connection.Execute(sql, item);
    }

    public List<ConsumableModel> GetAllConsumables()
    {
        string sql = $"SELECT * FROM {Table}";
        return connection.Query<ConsumableModel>(sql).ToList();
    }

    public List<ConsumableModel> GetConsumablesByCategory(string category)
    {
        string sql = $@"
        SELECT *
        FROM {Table}
        WHERE Category = @Category";
        return connection.Query<ConsumableModel>(sql, new { Category = category }).ToList();
    }

    public ConsumableModel? GetConsumableById(Int64 id)
    {
        string sql = $"SELECT * FROM {Table} WHERE Id = @Id";
        return connection.QueryFirstOrDefault<ConsumableModel>(sql, new { Id = id });
    }

    public ConsumableModel? GetById(Int64 id)
    {
        return GetConsumableById(id);
    }
}
