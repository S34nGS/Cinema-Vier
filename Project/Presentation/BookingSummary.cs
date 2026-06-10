public static class BookingSummary
{
    // TODO: Re-Implement this to be a UiHelper Page :D
    // GeneratePage
    public static void Start(
        List<SeatModel> selectedSeats,
        List<OrderItemModel> orderedMenuItems,
        double menuTotal,
        List<OrderItemModel> loungePreOrderItems,
        double loungePreOrderTotal,
        double finalTotal
    )
    {
        double ticketTotal = 0.0;
        foreach (SeatModel seat in selectedSeats)
        {
            ticketTotal += PurchaseLogic.GetSeatPrice(seat);
        }

        string output = "Booking Summary\n\n";

        if (selectedSeats.Count > 0)
        {
            output += "Seats:\n";
            var seatGroups = selectedSeats
                .GroupBy(s => s.SeatPriority)
                .Select(g => new
                {
                    Priority = g.Key,
                    Count = g.Count(),
                    Price = PurchaseLogic.GetSeatPrice(g.First()),
                    Total = g.Count() * PurchaseLogic.GetSeatPrice(g.First())
                });

            foreach (var group in seatGroups)
            {
                string typeName = PurchaseLogic.GetSeatTypeName(group.Priority);
                output += $"{typeName} (€{group.Price:0.00}): {group.Count} - €{group.Total:0.00}\n";
            }
        }
        else
        {
            output += "No seats selected.\n";
        }

        output += $@"
Ticket total: €{ticketTotal:0.00}
Number of seats: {selectedSeats.Count}
";

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
