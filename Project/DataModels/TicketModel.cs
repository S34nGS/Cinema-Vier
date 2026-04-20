using System.ComponentModel.DataAnnotations.Schema;

public class TicketModel
{
    public Int64 Id { get; set; }
    public Int64? UserId { get; private set; }
    public DateTime SelectedDateTime { get; private set; }
    public string PaymentMethod { get; private set; }
    // public int TimetableId { get; private set; }
    // public int SeatId { get; private set;}

    public TicketModel (Int64? Id, Int64? userId, DateTime selectedDateTime, string paymentMethod)
    {
        Id = Id;
        UserId = userId;
        SelectedDateTime = selectedDateTime;
        PaymentMethod = paymentMethod;
    }
}