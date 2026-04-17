public class FoodAndDrinkMenu
{
    public static List<OrderItemModel> ShowFoodAndDrinkMenu()
    {
        MenuLogic menuLogic = new MenuLogic();
        List<OrderItemModel> orderItems = new List<OrderItemModel>();

        bool continueOrdering = true;

        while (continueOrdering)
        {
            Console.WriteLine("Choose a category:");
            Console.WriteLine("1. Snacks");
            Console.WriteLine("2. Drinks");
            Console.WriteLine("3. Finish order");

            string categoryChoice = Console.ReadLine();

            if (categoryChoice == "1")
            {
                ShowCategoryItems(menuLogic.GetSnacks(), menuLogic, orderItems);
            }
            else if (categoryChoice == "2")
            {
                ShowCategoryItems(menuLogic.GetDrinks(), menuLogic, orderItems);
            }
            else if (categoryChoice == "3")
            {
                continueOrdering = false;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        return orderItems;
    }

    private static void ShowCategoryItems(List<MenuItemModel> items, MenuLogic menuLogic, List<OrderItemModel> orderItems)
    {
        Console.WriteLine("Choose an item:");

        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + items[i].Name + " - €" + items[i].Price);
        }

        string itemChoiceText = Console.ReadLine();
        Int64 itemChoice;

        if (Int64.TryParse(itemChoiceText, out itemChoice) == false)
        {
            Console.WriteLine("Invalid number.");
            return;
        }

        if (itemChoice < 1 || itemChoice > items.Count)
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        MenuItemModel selectedItem = items[(int)itemChoice - 1];

        Console.WriteLine("Enter quantity:");
        string quantityText = Console.ReadLine();
        Int64 quantity;

        if (Int64.TryParse(quantityText, out quantity) == false)
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        try
        {
            menuLogic.AddItemToOrder(orderItems, selectedItem.Id, quantity);
            Console.WriteLine(selectedItem.Name + " added to order.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        ShowSummary(orderItems, menuLogic);
        ShowEditMenu(orderItems, menuLogic);
    }

    private static void ShowSummary(List<OrderItemModel> orderItems, MenuLogic menuLogic)
    {
        Console.WriteLine("");
        Console.WriteLine("Order Summary");
        Console.WriteLine("-----------------------------");

        foreach (OrderItemModel item in orderItems)
        {
            Console.WriteLine("Name: " + item.Name);
            Console.WriteLine("Quantity: " + item.Quantity);
            Console.WriteLine("Price per item: €" + item.PricePerItem);
            Console.WriteLine("Subtotal: €" + item.SubTotal);
            Console.WriteLine("-----------------------------");
        }

        Console.WriteLine("Menu Total: €" + menuLogic.CalculateMenuTotal(orderItems));
        Console.WriteLine("");
    }

    private static void ShowEditMenu(List<OrderItemModel> orderItems, MenuLogic menuLogic)
    {
        if (orderItems.Count == 0)
        {
            return;
        }

        Console.WriteLine("Do you want to edit the order?");
        Console.WriteLine("1. Update quantity");
        Console.WriteLine("2. Remove item");
        Console.WriteLine("3. Continue");

        string choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.WriteLine("Enter item id to update:");
            string itemIdText = Console.ReadLine();
            Int64 itemId;

            if (Int64.TryParse(itemIdText, out itemId) == false)
            {
                Console.WriteLine("Invalid item id.");
                return;
            }

            Console.WriteLine("Enter new quantity:");
            string quantityText = Console.ReadLine();
            Int64 newQuantity;

            if (Int64.TryParse(quantityText, out newQuantity) == false)
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            try
            {
                menuLogic.UpdateItemQuantity(orderItems, itemId, newQuantity);
                Console.WriteLine("Quantity updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else if (choice == "2")
        {
            Console.WriteLine("Enter item id to remove:");
            string itemIdText = Console.ReadLine();
            Int64 itemId;

            if (Int64.TryParse(itemIdText, out itemId) == false)
            {
                Console.WriteLine("Invalid item id.");
                return;
            }

            try
            {
                menuLogic.RemoveItemFromOrder(orderItems, itemId);
                Console.WriteLine("Item removed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}