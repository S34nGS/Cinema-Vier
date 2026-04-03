public class PurchaseModel
{
    public int? UserId { get; private set; }
    public DateTime SelectedDateTime { get; private set; }
    public string PaymentMethod { get; private set; }
    // public int TimetableId { get; private set; }
    // public int SeatId { get; private set;}

    public PurchaseModel (int? userId, DateTime selectedDateTime, string paymentMethod)
    {
        UserId = userId;
        SelectedDateTime = selectedDateTime;
        PaymentMethod = paymentMethod;
    }
}