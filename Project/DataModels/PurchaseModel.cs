using System.ComponentModel.DataAnnotations.Schema;

public class PurchaseModel
{
    public Int64? UserId { get; private set; }
    public DateTime SelectedDateTime { get; private set; }
    public string PaymentMethod { get; private set; }
    // public int TimetableId { get; private set; }
    // public int SeatId { get; private set;}
    // [ForeignKey("Movie")]
    // public int MovieId { get; set; }

    public PurchaseModel (Int64? userId, DateTime selectedDateTime, string paymentMethod)
    {
        UserId = userId;
        SelectedDateTime = selectedDateTime;
        PaymentMethod = paymentMethod;
        // MovieId = movieId;
    }
}