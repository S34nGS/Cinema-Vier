public class MenuLogic
{
    private readonly MenuItemsAccess menuItemsAccess = new();

    public List<MenuItemModel> GetSnacks()
    {
        return menuItemsAccess.GetMenuItemsByCategory("Snack");
    }

    public List<MenuItemModel> GetDrinks()
    {
        return menuItemsAccess.GetMenuItemsByCategory("Drink");
    }

    public bool AddItemToOrder(List<OrderItemModel> orderItems, Int64 menuItemId, Int64 quantity)
    {
        if (quantity <= 0)
        {
            return false;
        }

        // get item from access layer
        MenuItemModel? selectedItem = menuItemsAccess.GetMenuItemById(menuItemId);

        if (selectedItem == null)
        {
            return false;
        }

        // update quantity if item already exists
        foreach (OrderItemModel orderItem in orderItems)
        {
            if (orderItem.MenuItemId == menuItemId)
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

    public bool UpdateItemQuantity(List<OrderItemModel> orderItems, Int64 menuItemId, Int64 newQuantity)
    {
        if (newQuantity <= 0)
        {
            return false;
        }

        foreach (OrderItemModel orderItem in orderItems)
        {
            if (orderItem.MenuItemId == menuItemId)
            {
                orderItem.Quantity = newQuantity;
                orderItem.SubTotal = orderItem.PricePerItem * newQuantity;
                return true;
            }
        }

        return false;
    }

    public bool RemoveItemFromOrder(List<OrderItemModel> orderItems, Int64 menuItemId)
    {
        for (int i = 0; i < orderItems.Count; i++)
        {
            if (orderItems[i].MenuItemId == menuItemId)
            {
                orderItems.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public decimal CalculateMenuTotal(List<OrderItemModel> orderItems)
    {
        // calculate total
        decimal total = 0;

        foreach (OrderItemModel orderItem in orderItems)
        {
            total += orderItem.SubTotal;
        }

        return total;
    }
}
