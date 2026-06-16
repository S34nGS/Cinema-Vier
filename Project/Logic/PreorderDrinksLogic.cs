public static class PreorderDrinksLogic
{
    private static ConsumableOrderAccess consumableOrderAccess = new();
    public static void Save(List<OrderItemModel> items, Int64 reservationId)
    {
        foreach (OrderItemModel item in items)
        {
            ConsumableOrderModel order = new ConsumableOrderModel(
                -1,
                reservationId,
                item.ConsumableId,
                item.Quantity
            );

            consumableOrderAccess.Write(order);
        }
    }
}
