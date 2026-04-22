public class MenuItemModel
{
    public Int64 Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }

    // empty constructor for dapper
    public MenuItemModel()
    {
        Name = "";
        Category = "";
    }

    // full constructor
    public MenuItemModel(Int64 id, string name, string category, decimal price)
    {
        Id = id;
        Name = name;
        Category = category;
        Price = price;
    }
}