public class ConsumableModel : IModel
{
    public Int64 Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }

    public ConsumableModel(
        Int64 id,
        string name,
        string category,
        double price
    )
    {
        Id = id;
        Name = name;
        Category = category;
        Price = price;
    }
}
