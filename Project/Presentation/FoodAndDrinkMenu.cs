public class FoodAndDrinkMenu
{
    public static List<OrderItemModel> ShowFoodAndDrinkMenu()
    {
        MenuLogic menuLogic = new();
        List<OrderItemModel> orderItems = [];

        while (true)
        {
            List<string> categoryMenu = [
                "Snacks before the movie",
                "Drinks before the movie",
                "Finish order"
            ];

            int categoryChoice = UiHelper.SelectionMenu.WriteMenu(categoryMenu, "Choose snacks or drinks before the movie");

            if (categoryChoice == 0)
            {
                ShowCategoryItems(menuLogic.GetSnacks(), menuLogic, orderItems);
            }
            else if (categoryChoice == 1)
            {
                ShowCategoryItems(menuLogic.GetDrinks(), menuLogic, orderItems);
            }
            else
            {
                break;
            }
        }

        return orderItems;
    }

    public static List<OrderItemModel> ShowOnlyDrinksMenu(MenuLogic menuLogic)
    {
        // list for selected lounge drinks
        List<OrderItemModel> orderItems = [];

        // show only drinks before the movie
        ShowCategoryItems(menuLogic.GetDrinks(), menuLogic, orderItems);

        return orderItems;
    }

    private static void ShowCategoryItems(List<MenuItemModel> items, MenuLogic menuLogic, List<OrderItemModel> orderItems)
    {
        while (true)
        {
            // build item menu with prices
            List<string> itemMenu = new List<string>();

            foreach (MenuItemModel item in items)
            {
                itemMenu.Add($"{item.Name} - €{item.Price:0.00}");
            }

            int selectedItemIndex = UiHelper.SelectionMenu.WriteMenu(itemMenu, "Choose an item before the movie");

            if (selectedItemIndex == -1)
            {
                return;
            }

            MenuItemModel selectedItem = items[selectedItemIndex];

            Int64 quantity;
            do
            {
                string quantityText = UiHelper.InputMenu.WriteMenu("Enter quantity");

                if (quantityText == "-1")
                {
                    return;
                }

                if (Int64.TryParse(quantityText, out quantity) == false)
                {
                    quantity = 0;
                }

                if (quantity < 1)
                {
                    UiHelper.HoldUser("Quantity must be at least 1."); // invalid quantity
                }

            } while (quantity < 1);

            bool result = menuLogic.AddItemToOrder(orderItems, selectedItem.Id, quantity);

            if (result == false)
            {
                UiHelper.HoldUser("Could not add item.");
                continue;
            }

            UiHelper.HoldUser($"{selectedItem.Name} added to order."); // success
            ShowSummary(orderItems, menuLogic);
            ShowEditMenu(orderItems, menuLogic);
            break;
        }
    }

    private static void ShowSummary(List<OrderItemModel> orderItems, MenuLogic menuLogic)
    {
        Console.WriteLine($@"
Order Summary
");

        for (int i = 0; i < orderItems.Count; i++)
        {
            OrderItemModel item = orderItems[i];

            Console.WriteLine($@"
{i + 1}. {item.Name}
Quantity: {item.Quantity}
Price per item: €{item.PricePerItem:0.00}
Subtotal: €{item.SubTotal:0.00}
");
        }

        Console.WriteLine($@"
Menu Total: €{menuLogic.CalculateMenuTotal(orderItems):0.00}
");
    }

    private static void ShowEditMenu(List<OrderItemModel> orderItems, MenuLogic menuLogic)
    {
        if (orderItems.Count == 0)
        {
            return;
        }

        while (true)
        {
            // show edit options
            List<string> editMenu = new List<string>
            {
                "Update quantity",
                "Remove item",
                "Continue"
            };

            int choice = UiHelper.SelectionMenu.WriteMenu(editMenu, "Do you want to edit the order?");

            if (choice == 0)
            {
                UpdateOrderItem(orderItems, menuLogic);

                // show summary after update
                ShowSummary(orderItems, menuLogic);

                // go back to edit menu
                continue;
            }
            else if (choice == 1)
            {
                RemoveOrderItem(orderItems, menuLogic);

                // show summary after remove
                ShowSummary(orderItems, menuLogic);

                if (orderItems.Count == 0)
                {
                    return;
                }

                // go back to edit menu
                continue;
            }
            else
            {
                return;
            }
        }
    }

    private static void UpdateOrderItem(List<OrderItemModel> orderItems, MenuLogic menuLogic)
    {
        // build update menu
        List<string> updateMenu = new List<string>();

        foreach (OrderItemModel item in orderItems)
        {
            updateMenu.Add($"{item.Name} - current quantity: {item.Quantity}");
        }

        int selectedIndex = UiHelper.SelectionMenu.WriteMenu(updateMenu, "Choose item to update");

        if (selectedIndex == -1)
        {
            return;
        }

        OrderItemModel selectedItem = orderItems[selectedIndex];

        Int64 newQuantity;
        do
        {
            string qtyText = UiHelper.InputMenu.WriteMenu($"Enter new quantity for {selectedItem.Name}");

            if (qtyText == "-1")
            {
                return;
            }

            if (Int64.TryParse(qtyText, out newQuantity) == false)
            {
                newQuantity = 0;
            }

            if (newQuantity < 1)
            {
                UiHelper.HoldUser("Quantity must be at least 1.");
            }

        } while (newQuantity < 1);

        bool result = menuLogic.UpdateItemQuantity(orderItems, selectedItem.MenuItemId, newQuantity);

        if (result == false)
        {
            UiHelper.HoldUser("Could not update quantity.");
            return;
        }

        UiHelper.HoldUser("Quantity updated.");
    }

    private static void RemoveOrderItem(List<OrderItemModel> orderItems, MenuLogic menuLogic)
    {
        // build remove menu
        List<string> removeMenu = new List<string>();

        foreach (OrderItemModel item in orderItems)
        {
            removeMenu.Add($"{item.Name} - quantity: {item.Quantity}");
        }

        int selectedIndex = UiHelper.SelectionMenu.WriteMenu(removeMenu, "Choose item to remove");

        if (selectedIndex == -1)
        {
            return;
        }

        OrderItemModel selectedItem = orderItems[selectedIndex];

        bool result = menuLogic.RemoveItemFromOrder(orderItems, selectedItem.MenuItemId);

        if (result == false)
        {
            UiHelper.HoldUser("Could not remove item.");
            return;
        }

        UiHelper.HoldUser($"{selectedItem.Name} removed from order.");
    }
}
