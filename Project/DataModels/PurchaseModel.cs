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

    // added save ordered food and drink items
    public List<OrderItemModel> OrderedMenuItems { get; private set; }

    // added save ticket total
    public decimal TicketTotal { get; private set; }

    // added save food and drink total
    public decimal MenuTotal { get; private set; }

    // added save final total
    public decimal FinalTotal { get; private set; }

    public PurchaseModel(Int64? userId, DateTime selectedDateTime, string paymentMethod)
    {
        UserId = userId;
        SelectedDateTime = selectedDateTime;
        PaymentMethod = paymentMethod;
        // MovieId = movieId;

        // added default values so old code keeps working
        OrderedMenuItems = new List<OrderItemModel>();
        TicketTotal = 0;
        MenuTotal = 0;
        FinalTotal = 0;
    }

    // added new constructor for purchase with food and drink items
    public PurchaseModel(
        Int64? userId,
        DateTime selectedDateTime,
        string paymentMethod,
        List<OrderItemModel> orderedMenuItems,
        decimal ticketTotal,
        decimal menuTotal,
        decimal finalTotal)
    {
        UserId = userId;
        SelectedDateTime = selectedDateTime;
        PaymentMethod = paymentMethod;
        OrderedMenuItems = orderedMenuItems;
        TicketTotal = ticketTotal;
        MenuTotal = menuTotal;
        FinalTotal = finalTotal;
    }
}