public class MenuLogic
{
    private MenuItemsAccess menuItemsAccess;

    public MenuLogic()
    {
        menuItemsAccess = new MenuItemsAccess();

        // make sure table exists
        menuItemsAccess.CreateTable();
    }

    public List<MenuItemModel> GetSnacks()
    {
        return menuItemsAccess.GetMenuItemsByCategory("Snack");
    }

    public List<MenuItemModel> GetDrinks()
    {
        return menuItemsAccess.GetMenuItemsByCategory("Drink");
    }

    public string ValidateQuantity(Int64 quantity)
    {
        // check quantity
        if (quantity < 1)
        {
            return "Quantity must be at least 1.";
        }

        return "";
    }

    public string AddItemToOrder(List<OrderItemModel> orderItems, Int64 menuItemId, Int64 quantity)
    {
        // validate quantity
        string validationMessage = ValidateQuantity(quantity);

        if (validationMessage != "")
        {
            return validationMessage;
        }

        // get item from access layer
        MenuItemModel? selectedItem = menuItemsAccess.GetMenuItemById(menuItemId);

        if (selectedItem == null)
        {
            return "Item not found.";
        }

        // update quantity if item already exists
        foreach (OrderItemModel orderItem in orderItems)
        {
            if (orderItem.MenuItemId == menuItemId)
            {
                orderItem.Quantity = orderItem.Quantity + quantity;
                orderItem.SubTotal = orderItem.Quantity * orderItem.PricePerItem;
                return "";
            }
        }

        // add new item
        OrderItemModel newOrderItem = new OrderItemModel(
            selectedItem.Id,
            selectedItem.Name,
            selectedItem.Price,
            quantity
        );

        orderItems.Add(newOrderItem);
        return "";
    }

    public string UpdateItemQuantity(List<OrderItemModel> orderItems, Int64 menuItemId, Int64 newQuantity)
    {
        // validate new quantity
        string validationMessage = ValidateQuantity(newQuantity);

        if (validationMessage != "")
        {
            return validationMessage;
        }

        foreach (OrderItemModel orderItem in orderItems)
        {
            if (orderItem.MenuItemId == menuItemId)
            {
                orderItem.Quantity = newQuantity;
                orderItem.SubTotal = orderItem.PricePerItem * newQuantity;
                return "";
            }
        }

        return "Item not found in order.";
    }

    public string RemoveItemFromOrder(List<OrderItemModel> orderItems, Int64 menuItemId)
    {
        for (int i = 0; i < orderItems.Count; i++)
        {
            if (orderItems[i].MenuItemId == menuItemId)
            {
                orderItems.RemoveAt(i);
                return "";
            }
        }

        return "Item not found in order.";
    }

    public decimal CalculateMenuTotal(List<OrderItemModel> orderItems)
    {
        // calculate total
        decimal total = 0;

        foreach (OrderItemModel orderItem in orderItems)
        {
            total = total + orderItem.SubTotal;
        }

        return total;
    }
}