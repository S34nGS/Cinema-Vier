public class PurchaseModel
{
    public int? UserId { get; private set; }
    public int TimetableId { get; private set; }
    public DateTime SelectedDateTime { get; private set; }
    public string PaymentMethod { get; private set; }

    public PurchaseModel (int? userId, int timetableId, DateTime selectedDateTime, string paymentMethod)
    {
        UserId = userId;
        TimetableId = timetableId;
        SelectedDateTime = selectedDateTime;
        PaymentMethod = paymentMethod;
    }
}