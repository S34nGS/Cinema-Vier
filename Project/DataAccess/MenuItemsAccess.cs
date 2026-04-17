public class MenuItemsAccess
{
    public List<MenuItemModel> GetAllMenuItems()
    {
        List<MenuItemModel> items = new List<MenuItemModel>();

        items.Add(new MenuItemModel { Id = 1, Name = "Popcorn", Category = "Snack", Price = 2.00m });
        items.Add(new MenuItemModel { Id = 2, Name = "Nachos", Category = "Snack", Price = 3.50m });
        items.Add(new MenuItemModel { Id = 3, Name = "Chips", Category = "Snack", Price = 1.50m });

        items.Add(new MenuItemModel { Id = 4, Name = "Water", Category = "Drink", Price = 1.00m });
        items.Add(new MenuItemModel { Id = 5, Name = "Cola", Category = "Drink", Price = 2.00m });
        items.Add(new MenuItemModel { Id = 6, Name = "Juice", Category = "Drink", Price = 2.50m });

        return items;
    }

    public List<MenuItemModel> GetMenuItemsByCategory(string category)
    {
        List<MenuItemModel> allItems = GetAllMenuItems();
        List<MenuItemModel> result = new List<MenuItemModel>();

        foreach (MenuItemModel item in allItems)
        {
            if (item.Category == category)
            {
                result.Add(item);
            }
        }

        return result;
    }

    public MenuItemModel GetMenuItemById(Int64 id)
    {
        List<MenuItemModel> allItems = GetAllMenuItems();

        foreach (MenuItemModel item in allItems)
        {
            if (item.Id == id)
            {
                return item;
            }
        }

        return null;
    }
}