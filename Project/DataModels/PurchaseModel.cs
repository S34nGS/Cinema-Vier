public class PurchaseModel
{
    public int? UserId { get; set; }
    public int TimetableId { get; set; }
    public DateTime SelectedDateTime { get; set; }
    public string PaymentMethod { get; set; }

    public PurchaseModel (int? userId, int timetableId, DateTime selectedDateTime, string paymentMethod)
    {
        UserId = userId;
        TimetableId = timetableId;
        SelectedDateTime = selectedDateTime;
        PaymentMethod = paymentMethod;
    }
}