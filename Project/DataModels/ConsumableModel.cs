public class ConsumableModel
{
    public Int64 Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Int64 AgeRating { get; set; }

    public ConsumableModel(
        Int64 id,
        string name,
        decimal price,
        Int64 ageRating
    )
    {
        Id = id;
        Name = name;
        Price = price;
        AgeRating = ageRating;
    }
}
