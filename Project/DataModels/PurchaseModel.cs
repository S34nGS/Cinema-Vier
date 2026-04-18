public class PurchaseModel
{
    public int? UserId { get; private set; }
    public DateTime SelectedDateTime { get; private set; }
    public string PaymentMethod { get; private set; }

    // added save ordered food and drink items
    public List<OrderItemModel> OrderedMenuItems { get; private set; }

    // added save ticket total
    public decimal TicketTotal { get; private set; }

    // added save food and drink total
    public decimal MenuTotal { get; private set; }

    // added save final total
    public decimal FinalTotal { get; private set; }

    public PurchaseModel(int? userId, DateTime selectedDateTime, string paymentMethod)
    {
        UserId = userId;
        SelectedDateTime = selectedDateTime;
        PaymentMethod = paymentMethod;

        // added default values so old code keeps working
        OrderedMenuItems = new List<OrderItemModel>();
        TicketTotal = 0;
        MenuTotal = 0;
        FinalTotal = 0;
    }

    // added new constructor for purchase with food and drink items
    public PurchaseModel(
        int? userId,
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