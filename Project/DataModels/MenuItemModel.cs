public class MenuItemModel : IModel
{
    public Int64 Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }

    public MenuItemModel(
        Int64 id,
        string name,
        string category,
        decimal price
    )
    {
        Id = id;
        Name = name;
        Category = category;
        Price = price;
    }
}
