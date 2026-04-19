public class MenuItemModel
{
    public Int64 Id { get; set; }
    public string Name { get; set; }
    public string Category {get; set; }
    public decimal Price { get; set; }

    public MenuItemModel()
    {
        Name = "";
        Category = "";
    }

}