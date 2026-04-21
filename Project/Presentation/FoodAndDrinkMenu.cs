public class FoodAndDrinkMenu
{
    public static List<OrderItemModel> ShowFoodAndDrinkMenu()
    {
        // create logic object
        MenuLogic menuLogic = new MenuLogic();
        List<OrderItemModel> orderItems = new List<OrderItemModel>();

        while (true)
        {
            // main category menu
            List<string> categoryMenu = new List<string>
            {
                "Snacks",
                "Drinks",
                "Finish order"
            };

            int categoryChoice = UiLib.SelectionMenu(categoryMenu, "Choose a category");

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

    private static void ShowCategoryItems(List<MenuItemModel> items, MenuLogic menuLogic, List<OrderItemModel> orderItems)
    {
        while (true)
        {
            Console.WriteLine($"Choose an item:"); // show items

            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i].Name} - €{items[i].Price}"); // show items
            }

            string? itemChoiceText = Console.ReadLine();
            Int64 itemChoice;

            if (Int64.TryParse(itemChoiceText, out itemChoice) == false)
            {
                Console.WriteLine($"Invalid number. Please enter a number from the list."); // wrong input
                continue;
            }

            if (itemChoice < 1 || itemChoice > items.Count)
            {
                Console.WriteLine($"Invalid choice. Please enter a number from the list."); // out of range
                continue;
            }

            MenuItemModel selectedItem = items[(int)itemChoice - 1];

            Int64 quantity;
            do
            {
                Console.WriteLine($"Enter quantity:");
                string? quantityText = Console.ReadLine();

                if (Int64.TryParse(quantityText, out quantity) == false)
                {
                    quantity = 0;
                }

                if (quantity < 1)
                {
                    Console.WriteLine($"Quantity must be at least 1."); // invalid quantity
                }

            } while (quantity < 1);

            string result = menuLogic.AddItemToOrder(orderItems, selectedItem.Id, quantity);

            if (result != "")
            {
                Console.WriteLine($"{result}");
                continue;
            }

            Console.WriteLine($"{selectedItem.Name} added to order."); // success
            ShowSummary(orderItems, menuLogic);
            ShowEditMenu(orderItems, menuLogic);
            break;
        }
    }

    private static void ShowSummary(List<OrderItemModel> orderItems, MenuLogic menuLogic)
    {
        Console.WriteLine($"");
        Console.WriteLine($"Order Summary");
        Console.WriteLine($"");

        for (int i = 0; i < orderItems.Count; i++)
        {
            OrderItemModel item = orderItems[i];
            Console.WriteLine($"{i + 1}. {item.Name}"); // item number
            Console.WriteLine($"Quantity: {item.Quantity}");
            Console.WriteLine($"Price per item: €{item.PricePerItem}");
            Console.WriteLine($"Subtotal: €{item.SubTotal}");
            Console.WriteLine($"");
        }

        Console.WriteLine($"Menu Total: €{menuLogic.CalculateMenuTotal(orderItems)}"); // total
        Console.WriteLine($"");
    }

    private static void ShowEditMenu(List<OrderItemModel> orderItems, MenuLogic menuLogic)
    {
        if (orderItems.Count == 0)
        {
            return;
        }

        while (true)
        {
            Console.WriteLine($"Do you want to edit the order?");
            Console.WriteLine($"1. Update quantity");
            Console.WriteLine($"2. Remove item");
            Console.WriteLine($"3. Continue");

            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                UpdateOrderItem(orderItems, menuLogic);
                return;
            }
            else if (choice == "2")
            {
                RemoveOrderItem(orderItems, menuLogic);
                return;
            }
            else if (choice == "3")
            {
                return;
            }
            else
            {
                Console.WriteLine($"Invalid choice. Please enter a number from the list.");
            }
        }
    }

    private static void UpdateOrderItem(List<OrderItemModel> orderItems, MenuLogic menuLogic)
    {
        Int64 index;

        while (true)
        {
            Console.WriteLine($"Choose item to update:");

            for (int i = 0; i < orderItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {orderItems[i].Name}"); // show items
            }

            string? input = Console.ReadLine();

            if (Int64.TryParse(input, out index) == false || index < 1 || index > orderItems.Count)
            {
                Console.WriteLine($"Invalid number. Please enter a number from the list.");
                continue;
            }

            break;
        }

        OrderItemModel selectedItem = orderItems[(int)index - 1];

        Console.WriteLine($"Selected item: {selectedItem.Name}"); // selected item
        Console.WriteLine($"Current quantity: {selectedItem.Quantity}");

        Int64 newQuantity;
        do
        {
            Console.WriteLine($"Enter new quantity:");
            string? qtyText = Console.ReadLine();

            if (Int64.TryParse(qtyText, out newQuantity) == false)
            {
                newQuantity = 0;
            }

            if (newQuantity < 1)
            {
                Console.WriteLine($"Quantity must be at least 1.");
            }

        } while (newQuantity < 1);

        string result = menuLogic.UpdateItemQuantity(orderItems, selectedItem.MenuItemId, newQuantity);

        if (result != "")
        {
            Console.WriteLine($"{result}");
            return;
        }

        Console.WriteLine($"Quantity updated.");
    }

    private static void RemoveOrderItem(List<OrderItemModel> orderItems, MenuLogic menuLogic)
    {
        Int64 index;

        while (true)
        {
            Console.WriteLine($"Choose item to remove:");

            for (int i = 0; i < orderItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {orderItems[i].Name}");
            }

            string? input = Console.ReadLine();

            if (Int64.TryParse(input, out index) == false || index < 1 || index > orderItems.Count)
            {
                Console.WriteLine($"Invalid number. Please enter a number from the list.");
                continue;
            }

            break;
        }

        OrderItemModel selectedItem = orderItems[(int)index - 1];

        string result = menuLogic.RemoveItemFromOrder(orderItems, selectedItem.MenuItemId);

        if (result != "")
        {
            Console.WriteLine($"{result}");
            return;
        }

        Console.WriteLine($"Item removed.");
    }
}
