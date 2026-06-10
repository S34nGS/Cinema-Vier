public class ConsumableOrderModel
{
    public Int64 Id { get; set; }
    public Int64 ReservationId { get; set; }
    public Int64 ConsumableId { get; set; }
    public Int64 Amount { get; set; }

    public ConsumableOrderModel(Int64 id, Int64 reservationId, Int64 consumableId, Int64 amount)
    {
        Id = id;
        ReservationId = reservationId;
        ConsumableId = consumableId;
        Amount = amount;
    }
}