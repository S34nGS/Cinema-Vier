public class FoodAndDrinkMenu
{
    public static List<OrderItemModel> ShowFoodAndDrinkMenu()
    {
        MenuLogic menuLogic = new MenuLogic(); // logic layer
        List<OrderItemModel> orderItems = new List<OrderItemModel>(); // list of selected items

        bool continueOrdering = true;

        while (continueOrdering)
        {
            Console.WriteLine("Choose a category:");
            Console.WriteLine("1. Snacks");
            Console.WriteLine("2. Drinks");
            Console.WriteLine("3. Finish order");

            string categoryChoice = Console.ReadLine(); // user input

            if (categoryChoice == "1")
            {
                ShowCategoryItems(menuLogic.GetSnacks(), menuLogic, orderItems); // show snacks
            }
            else if (categoryChoice == "2")
            {
                ShowCategoryItems(menuLogic.GetDrinks(), menuLogic, orderItems); // show drinks
            }
            else if (categoryChoice == "3")
            {
                continueOrdering = false; // finish ordering
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter a number from the list."); // wrong input
            }
        }

        return orderItems; // return selected items
    }

    private static void ShowCategoryItems(List<MenuItemModel> items, MenuLogic menuLogic, List<OrderItemModel> orderItems)
    {
        while (true) // stay in same category if input wrong
        {
            Console.WriteLine("Choose an item:");

            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + items[i].Name + " - €" + items[i].Price); // show items
            }

            string itemChoiceText = Console.ReadLine();
            Int64 itemChoice;

            if (Int64.TryParse(itemChoiceText, out itemChoice) == false)
            {
                Console.WriteLine("Invalid number."); // wrong number
                continue;
            }

            if (itemChoice < 1 || itemChoice > items.Count)
            {
                Console.WriteLine("Invalid choice. Please enter a number from the list."); // out of range
                continue;
            }

            MenuItemModel selectedItem = items[(int)itemChoice - 1]; // get selected item

            Int64 quantity;

            while (true) // stay until valid quantity
            {
                Console.WriteLine("Enter quantity:");
                string quantityText = Console.ReadLine();

                if (Int64.TryParse(quantityText, out quantity) == false)
                {
                    Console.WriteLine("Invalid quantity."); // wrong input
                    continue;
                }

                if (quantity < 1)
                {
                    Console.WriteLine("Quantity must be at least 1."); // min 1
                    continue;
                }

                break;
            }

            menuLogic.AddItemToOrder(orderItems, selectedItem.Id, quantity); // add item
            Console.WriteLine(selectedItem.Name + " added to order.");

            ShowSummary(orderItems, menuLogic); // show summary
            ShowEditMenu(orderItems, menuLogic); // allow edit
            break;
        }
    }

    private static void ShowSummary(List<OrderItemModel> orderItems, MenuLogic menuLogic)
    {
        Console.WriteLine("");
        Console.WriteLine("Order Summary");

        for (int i = 0; i < orderItems.Count; i++)
        {
            OrderItemModel item = orderItems[i];
            Console.WriteLine((i + 1) + ". " + item.Name); // show index
            Console.WriteLine("Quantity: " + item.Quantity);
            Console.WriteLine("Price: €" + item.PricePerItem);
            Console.WriteLine("Subtotal: €" + item.SubTotal);
        }

        Console.WriteLine("Menu Total: €" + menuLogic.CalculateMenuTotal(orderItems)); // total
        Console.WriteLine("");
    }

    private static void ShowEditMenu(List<OrderItemModel> orderItems, MenuLogic menuLogic)
    {
        if (orderItems.Count == 0)
        {
            return; // nothing to edit
        }

        Console.WriteLine("Do you want to edit the order?");
        Console.WriteLine("1. Update quantity");
        Console.WriteLine("2. Remove item");
        Console.WriteLine("3. Continue");

        string choice = Console.ReadLine();

        if (choice == "1")
        {
            while (true)
            {
                Console.WriteLine("Choose item to update:");

                for (int i = 0; i < orderItems.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + orderItems[i].Name); // show items
                }

                string input = Console.ReadLine();
                Int64 index;

                if (Int64.TryParse(input, out index) == false || index < 1 || index > orderItems.Count)
                {
                    Console.WriteLine("Invalid number."); // wrong input
                    continue;
                }

                OrderItemModel selectedItem = orderItems[(int)index - 1];

                Console.WriteLine("Selected item: " + selectedItem.Name); // show item
                Console.WriteLine("Current quantity: " + selectedItem.Quantity); // show old qty

                Int64 newQuantity;

                while (true)
                {
                    Console.WriteLine("Enter new quantity:");
                    string qtyText = Console.ReadLine();

                    if (Int64.TryParse(qtyText, out newQuantity) == false || newQuantity < 1)
                    {
                        Console.WriteLine("Invalid quantity."); // wrong qty
                        continue;
                    }

                    break;
                }

                menuLogic.UpdateItemQuantity(orderItems, selectedItem.MenuItemId, newQuantity); // update
                Console.WriteLine("Quantity updated.");
                break;
            }
        }
        else if (choice == "2")
        {
            while (true)
            {
                Console.WriteLine("Choose item to remove:");

                for (int i = 0; i < orderItems.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + orderItems[i].Name); // show items
                }

                string input = Console.ReadLine();
                Int64 index;

                if (Int64.TryParse(input, out index) == false || index < 1 || index > orderItems.Count)
                {
                    Console.WriteLine("Invalid number."); // wrong input
                    continue;
                }

                OrderItemModel selectedItem = orderItems[(int)index - 1];

                menuLogic.RemoveItemFromOrder(orderItems, selectedItem.MenuItemId); // remove
                Console.WriteLine("Item removed.");
                break;
            }
        }
    }
}