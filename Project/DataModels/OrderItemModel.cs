// [Obsolete("this doesn't get saved to the database afaik")]
public class OrderItemModel
{
    public Int64 MenuItemId { get; set; }
    public string Name { get; set; }
    public double PricePerItem { get; set; }
    public Int64 Quantity { get; set; }
    public double SubTotal { get; set; }

    public OrderItemModel(Int64 menuItemId, string name, double pricePerItem, Int64 quantity)
    {
        MenuItemId = menuItemId;
        Name = name;
        PricePerItem = pricePerItem;
        Quantity = quantity;
        SubTotal = pricePerItem * quantity;
    }
}
