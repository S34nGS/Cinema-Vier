public class MenuLogic
{
    private readonly ConsumablesAccess consumablesAccess = new();

    public List<ConsumableModel> GetSnacks()
    {
        return consumablesAccess.GetConsumablesByCategory("Snack");
    }

    public List<ConsumableModel> GetDrinks()
    {
        return consumablesAccess.GetConsumablesByCategory("Drink");
    }

    public bool AddItemToOrder(List<OrderItemModel> orderItems, Int64 consumableId, Int64 quantity)
    {
        if (quantity <= 0)
        {
            return false;
        }

        // get item from access layer
        ConsumableModel? selectedItem = consumablesAccess.GetConsumableById(consumableId);

        if (selectedItem == null)
        {
            return false;
        }

        // update quantity if item already exists
        foreach (OrderItemModel orderItem in orderItems)
        {
            if (orderItem.ConsumableId == consumableId)
            {
                orderItem.Quantity += quantity;
                orderItem.SubTotal = orderItem.Quantity * orderItem.PricePerItem;
                return true;
            }
        }

        OrderItemModel newOrderItem = new(
            selectedItem.Id,
            selectedItem.Name,
            selectedItem.Price,
            quantity
        );
        orderItems.Add(newOrderItem);

        return true;
    }

    public bool UpdateItemQuantity(List<OrderItemModel> orderItems, Int64 consumableId, Int64 newQuantity)
    {
        if (newQuantity <= 0)
        {
            return false;
        }

        foreach (OrderItemModel orderItem in orderItems)
        {
            if (orderItem.ConsumableId == consumableId)
            {
                orderItem.Quantity = newQuantity;
                orderItem.SubTotal = orderItem.PricePerItem * newQuantity;
                return true;
            }
        }

        return false;
    }

    public bool RemoveItemFromOrder(List<OrderItemModel> orderItems, Int64 consumableId)
    {
        for (int i = 0; i < orderItems.Count; i++)
        {
            if (orderItems[i].ConsumableId == consumableId)
            {
                orderItems.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public double CalculateMenuTotal(List<OrderItemModel> orderItems)
    {
        // calculate total
        double total = 0;

        foreach (OrderItemModel orderItem in orderItems)
        {
            total += orderItem.SubTotal;
        }

        return total;
    }
}
