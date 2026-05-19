public class OrderItemModel
{
    public Int64 MenuItemId { get; set; }
    public string Name { get; set; }
    public decimal PricePerItem { get; set; }
    public Int64 Quantity { get; set; }
    public decimal SubTotal { get; set; }

    public OrderItemModel(Int64 menuItemId, string name, decimal pricePerItem, Int64 quantity)
    {
        MenuItemId = menuItemId;
        Name = name;
        PricePerItem = pricePerItem;
        Quantity = quantity;
        SubTotal = pricePerItem * quantity;
    }
}
