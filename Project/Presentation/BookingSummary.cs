public static class BookingSummary
{
    // TODO: Re-Implement this to be a UiHelper Page :D
    // GeneratePage
    public static void Start(
        double ticketTotal,
        List<OrderItemModel> orderedMenuItems,
        double menuTotal,
        List<OrderItemModel> loungePreOrderItems,
        double loungePreOrderTotal,
        double finalTotal,
        List<SeatModel> selectedSeats
    )
    {
        string output = @$"Booking Summary

Ticket total: €{ticketTotal:0.00}
Number of seats: {selectedSeats.Count}

Selected seats:";

        foreach (SeatModel seat in selectedSeats)
        {
            output += $@"
Row: {seat.Row}, Seat: {seat.SeatNumber}";
        }

        output += Environment.NewLine;

        if (orderedMenuItems.Count > 0)
        {
            output += "Food and drink items:";

            foreach (OrderItemModel item in orderedMenuItems)
            {
                output += $@"
Item name: {item.Name}
Quantity: {item.Quantity}
Price per item: €{item.PricePerItem:0.00}
Subtotal: €{item.SubTotal:0.00}
";
            }
        }
        else
        {
            output += "No food or drinks selected.";
        }

        output += $@"
Food and drink total: €{menuTotal:0.00}
";

        if (loungePreOrderItems.Count > 0)
        {
            output += "Lounge pre-order drinks:";

            foreach (OrderItemModel item in loungePreOrderItems)
            {
                output += $@"
Item name: {item.Name}
Quantity: {item.Quantity}
Price per item: €{item.PricePerItem:0.00}
Subtotal: €{item.SubTotal:0.00}
";
            }
        }
        else
        {
            output += "No lounge drinks selected.";
        }

        output += $@"
Lounge drink pre-order total: €{loungePreOrderTotal:0.00}
Final total: €{finalTotal:0.00}
";

        UiHelper.GeneratePage(output)();
    }
}