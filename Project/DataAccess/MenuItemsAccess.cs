using Dapper;

public class MenuItemsAccess : DefaultAccess
{
    protected override string Table => "MenuItems";

    public override void CreateTable()
    {
        // create menu items table
        string sql = $@"
            CREATE TABLE IF NOT EXISTS {Table} (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Category TEXT NOT NULL,
                Price REAL NOT NULL
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
