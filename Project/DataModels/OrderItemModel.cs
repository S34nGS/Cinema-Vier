public class OrderItemModel
{
    public Int64 MenuItemId { get; set; }
    public string Name { get; set; }
    public decimal PricePerItem { get; set; }
    public Int64 Quantity { get; set; }
    public decimal SubTotal { get; set; }

    public OrderItemModel()
    {
        Name = "";
    }
}