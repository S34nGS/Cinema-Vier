public static class CreateConsumableTable
{
    public static Task Execute()
    {
        ConsumablesAccess consumable = new();
        consumable.CreateTable();

        // added default menu items in CreateDB
        List<ConsumableModel> consumablesList = [
            new ConsumableModel(0, "Popcorn", "Snack", 2.00),
            new ConsumableModel(0, "Nachos", "Snack", 3.50),
            new ConsumableModel(0, "Chips", "Snack", 1.50),
            new ConsumableModel(0, "Water", "Drink", 1.00),
            new ConsumableModel(0, "Cola", "Drink", 2.00),
            new ConsumableModel(0, "Juice", "Drink", 2.50)
        ];

        foreach (ConsumableModel item in consumablesList)
        {
            consumable.Write(item);
        }

        return Task.CompletedTask;
    }
}