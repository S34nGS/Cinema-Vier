public class MenuLogic
{
    private MenuItemsAccess menuItemsAccess;

    public MenuLogic()
    {
        menuItemsAccess = new MenuItemsAccess();
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
        if (quantity < 1)
        {
            return "Quantity must be at least 1.";
        }

        return "";
    }

    public void AddItemToOrder(List<OrderItemModel> orderItems, Int64 menuItemId, Int64 quantity)
    {
        string validationMessage = ValidateQuantity(quantity);

        if (validationMessage != "")
        {
            throw new Exception(validationMessage);
        }

        MenuItemModel selectedItem = menuItemsAccess.GetMenuItemById(menuItemId);

        if (selectedItem == null)
        {
            throw new Exception("Item not found.");
        }

        bool itemAlreadyExists = false;

        foreach (OrderItemModel orderItem in orderItems)
        {
            if (orderItem.MenuItemId == menuItemId)
            {
                orderItem.Quantity = orderItem.Quantity + quantity;
                orderItem.SubTotal = orderItem.Quantity * orderItem.PricePerItem;
                itemAlreadyExists = true;
            }
        }

        if (itemAlreadyExists == false)
        {
            OrderItemModel newOrderItem = new OrderItemModel();
            newOrderItem.MenuItemId = selectedItem.Id;
            newOrderItem.Name = selectedItem.Name;
            newOrderItem.PricePerItem = selectedItem.Price;
            newOrderItem.Quantity = quantity;
            newOrderItem.SubTotal = selectedItem.Price * quantity;

            orderItems.Add(newOrderItem);
        }
    }

    public void UpdateItemQuantity(List<OrderItemModel> orderItems, Int64 menuItemId, Int64 newQuantity)
    {
        string validationMessage = ValidateQuantity(newQuantity);

        if (validationMessage != "")
        {
            throw new Exception(validationMessage);
        }

        foreach (OrderItemModel orderItem in orderItems)
        {
            if (orderItem.MenuItemId == menuItemId)
            {
                orderItem.Quantity = newQuantity;
                orderItem.SubTotal = orderItem.PricePerItem * newQuantity;
                return;
            }
        }

        throw new Exception("Item not found in order.");
    }

    public void RemoveItemFromOrder(List<OrderItemModel> orderItems, Int64 menuItemId)
    {
        for (int i = 0; i < orderItems.Count; i++)
        {
            if (orderItems[i].MenuItemId == menuItemId)
            {
                orderItems.RemoveAt(i);
                return;
            }
        }

        throw new Exception("Item not found in order.");
    }

    public decimal CalculateMenuTotal(List<OrderItemModel> orderItems)
    {
        decimal total = 0;

        foreach (OrderItemModel orderItem in orderItems)
        {
            total = total + orderItem.SubTotal;
        }

        return total;
    }
}