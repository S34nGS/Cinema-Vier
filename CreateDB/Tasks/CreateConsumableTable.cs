public static class CreateConsumableTable
{

    public static Task Execute()
    {
        ConsumableAccess consumable = new();
        consumable.CreateTable();

        return Task.CompletedTask;
    }
}