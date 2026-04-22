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
            );
        ";

        connection.Execute(sql);

        // add default items if table is empty
        string countSql = $"SELECT COUNT(*) FROM {Table}";
        int count = connection.ExecuteScalar<int>(countSql);

        if (count == 0)
        {
            string insertSql = $@"
                INSERT INTO {Table} (Name, Category, Price) VALUES
                ('Popcorn', 'Snack', 2.00),
                ('Nachos', 'Snack', 3.50),
                ('Chips', 'Snack', 1.50),
                ('Water', 'Drink', 1.00),
                ('Cola', 'Drink', 2.00),
                ('Juice', 'Drink', 2.50);
            ";

            connection.Execute(insertSql);
        }
    }

    public List<MenuItemModel> GetAllMenuItems()
    {
        // get all menu items
        string sql = $"SELECT Id, Name, Category, Price FROM {Table}";
        return connection.Query<MenuItemModel>(sql).ToList();
    }

    public List<MenuItemModel> GetMenuItemsByCategory(string category)
    {
        // get items by category
        string sql = $"SELECT Id, Name, Category, Price FROM {Table} WHERE Category = @Category";
        return connection.Query<MenuItemModel>(sql, new { Category = category }).ToList();
    }

    public MenuItemModel? GetMenuItemById(Int64 id)
    {
        // get one item by id
        string sql = $"SELECT Id, Name, Category, Price FROM {Table} WHERE Id = @Id";
        return connection.QueryFirstOrDefault<MenuItemModel>(sql, new { Id = id });
    }
}