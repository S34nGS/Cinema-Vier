public class OrederItemModel
{
    public Int64 MenuItemId { get; set; }
    public string Name { get; set; }
    public double PricePerItem { get; set; }
    public Int64 Quantity { get; set; }
    public double SubTotal { get; set; }

    public OrederItemModel()
    {
        Name = "";
    }
}