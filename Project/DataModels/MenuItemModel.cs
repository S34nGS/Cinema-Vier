public class MenuItemModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }

    public MenuItemModel(string name, string category, decimal price)
    {
        Name = name;
        Category = category;
        Price = price;
    }
}
