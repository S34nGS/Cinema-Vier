using Dapper;

public class MenuItemsAccess : DefaultAccess, IAccess
{
    public static string Table { get; } = "MenuItems";

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

    public void Write(MenuItemModel item)
    {
        string sql = $@"
            INSERT INTO {Table} (Name, Category, Price)
            VALUES (@Name, @Category, @Price)
        ";

        connection.Execute(sql, item);
    }

    public List<MenuItemModel> GetAllMenuItems()
    {
        string sql = $"SELECT * FROM {Table}";
        return connection.Query<MenuItemModel>(sql).ToList();
    }

    public List<MenuItemModel> GetMenuItemsByCategory(string category)
    {
        string sql = $@"
        SELECT *
        FROM {Table}
        WHERE Category = @Category";
        return connection.Query<MenuItemModel>(sql, new { Category = category }).ToList();
    }

    public MenuItemModel? GetMenuItemById(Int64 id)
    {
        string sql = $"SELECT * FROM {Table} WHERE Id = @Id";
        return connection.QueryFirstOrDefault<MenuItemModel>(sql, new { Id = id });
    }
}
