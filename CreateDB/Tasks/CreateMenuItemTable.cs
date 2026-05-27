public static class CreateMenuItemTable
{
    public static Task Execute()
    {
        MenuItemsAccess menuItem = new();
        menuItem.CreateTable();

        // added default menu items in CreateDB
        List<MenuItemModel> menuItemsList = [
            new MenuItemModel(0, "Popcorn", "Snack", 2.00m),
            new MenuItemModel(0, "Nachos", "Snack", 3.50m),
            new MenuItemModel(0, "Chips", "Snack", 1.50m),
            new MenuItemModel(0, "Water", "Drink", 1.00m),
            new MenuItemModel(0, "Cola", "Drink", 2.00m),
            new MenuItemModel(0, "Juice", "Drink", 2.50m)
        ];

        foreach (MenuItemModel item in menuItemsList)
        {
            menuItem.Write(item);
        }

        return Task.CompletedTask;
    }
}