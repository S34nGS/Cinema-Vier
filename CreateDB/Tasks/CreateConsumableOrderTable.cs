public static class CreateConsumableOrderTable
{
    public static Task Execute()
    {
        ConsumableOrderAccess consumable = new();
        consumable.CreateTable();

        return Task.CompletedTask;
    }
}