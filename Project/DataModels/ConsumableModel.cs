public class ConsumableModel : IModel
{
    public Int64 Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Int64 AgeRating { get; set; }

    public ConsumableModel(
        Int64 id,
        string name,
        double price,
        Int64 ageRating
    )
    {
        Id = id;
        Name = name;
        Price = price;
        AgeRating = ageRating;
    }
}
