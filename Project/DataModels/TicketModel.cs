public class TicketModel : IModel
{
    public Int64 Id { get; set; }
    public Int64 UserId { get; private set; }
    public DateTime SelectedDateTime { get; private set; }
    public string PaymentMethod { get; private set; }

    public TicketModel(Int64 id, Int64 userId, DateTime selectedDateTime, string paymentMethod)
    {
        Id = id;
        UserId = userId;
        SelectedDateTime = selectedDateTime;
        PaymentMethod = paymentMethod;
    }
}
