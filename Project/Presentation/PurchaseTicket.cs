static class PurchaseTicket
{
    public static List<string> DateMenu { get; } = [];
    public static List<string> TimeMenu { get; } = [];
    public static List<string> PaymentMethods { get; } = ["Credit Card", "IBAN"];
    private static List<TimetableModel> CurrentTimetables = [];

    public static List<string> CreditCardInput =
    [
        "Cardholder name",
        "Card number (13-19 digits, for example:4111 1111 1111 1111)",
        "Expiration date (MM/YY)",
        "CVC/CVV code (3-4 digits)"
    ];

    public static List<string> IBANInput =
    [
        "Cardholder name",
        "IBAN number (for example: NL12 ABNA 1234 5678 90)"
    ];

    public static TicketModel? Start(MovieModel movie)
    {
        // reset date menu
        DateMenu.Clear();
        SetUpDateMenu(movie);

        if (DateMenu.Count == 0)
        {
            int dates = UiHelper.SelectionMenu(
                ["No available dates."],
                "Pick a date",
                true
            );

            if (dates == 0)
            {
                return null;
            }
        }

        int selectedDate = UiHelper.SelectionMenu(DateMenu, "Pick a date");
        if (selectedDate == -1)
        {
            return null;
        }

        string selectedDateString = DateMenu[selectedDate];

        // reset time menu
        TimeMenu.Clear();
        SetUpTimeMenu(movie, selectedDateString);

        int selectedTime = UiHelper.SelectionMenu(TimeMenu, "Pick a time");

        if (selectedTime == -1)
        {
            return null;
        }
        
        TimetableModel selectedTimetable = CurrentTimetables[selectedTime];

        string dateTimeString = $"{selectedDateString} {TimeMenu[selectedTime].Substring(0, 5)}";
        DateTime convertedDateTime = DateTime.Parse(dateTimeString);

        // ticket price for summary
        decimal ticketTotal = 12.00m;

        // selected menu items
        List<OrderItemModel> orderedMenuItems = new List<OrderItemModel>();

        // ask if user wants food or drinks
        List<string> orderMenuChoices =
        [
            "Continue without food and drinks",
            "Add food and drinks"
        ];

        int selectedOrderChoice = UiHelper.SelectionMenu(orderMenuChoices, "Do you want to add snacks or drinks?");
        if (selectedOrderChoice == 1)
        {
            orderedMenuItems = FoodAndDrinkMenu.ShowFoodAndDrinkMenu();
        }

        // selected lounge pre-order drinks
        List<OrderItemModel> loungePreOrderItems = new List<OrderItemModel>();

        // ask if user wants lounge pre-order drinks
        List<string> loungePreOrderChoices =
        [
            "Continue without lounge drink pre-order",
            "Add lounge drink pre-order"
        ];

        int selectedLoungePreOrderChoice = UiHelper.SelectionMenu(
            loungePreOrderChoices,
            "Do you want to pre-order drinks from the lounge/bar?"
        );

        if (selectedLoungePreOrderChoice == 1)
        {
            // show only drinks for lounge pre-order
            MenuLogic loungeMenuLogic = new MenuLogic();
            loungePreOrderItems = FoodAndDrinkMenu.ShowOnlyDrinksMenu(loungeMenuLogic);
        }

        // calculate totals
        MenuLogic menuLogic = new MenuLogic();
        decimal menuTotal = menuLogic.CalculateMenuTotal(orderedMenuItems);

        // calculate lounge pre-order total
        decimal loungePreOrderTotal = menuLogic.CalculateMenuTotal(loungePreOrderItems);

        // calculate final total with lounge pre-order
        decimal finalTotal = PurchaseLogic.CalculateFullTotal(ticketTotal, menuTotal, loungePreOrderTotal);

        // show summary before payment
        ShowBookingSummary(
            ticketTotal,
            orderedMenuItems,
            menuTotal,
            loungePreOrderItems,
            loungePreOrderTotal,
            finalTotal
        );

        int selectedPaymentMethod = UiHelper.SelectionMenu(PaymentMethods, "How do you want to pay?");
        if (selectedPaymentMethod == -1)
        {
            return null;
        }

        string selectedPaymentMethodString = PaymentMethods[selectedPaymentMethod];
        string invalidInputs = "";

        if (selectedPaymentMethodString == "Credit Card")
        {
            do
            {
                if (invalidInputs != "")
                {
                    Dictionary<string, string> creditCardInfo = UiHelper.InputForm(
                        CreditCardInput,
                        $"Invalid input: {invalidInputs} please try again"
                    );

                    invalidInputs = PurchaseLogic.CreditCardCheck(creditCardInfo);
                }
                else
                {
                    Dictionary<string, string> creditCardInfo = UiHelper.InputForm(
                        CreditCardInput,
                        "Please fill in the payment information"
                    );

                    invalidInputs = PurchaseLogic.CreditCardCheck(creditCardInfo);
                }

            } while (invalidInputs != "");
        }
        else if (selectedPaymentMethodString == "IBAN")
        {
            do
            {
                if (invalidInputs != "")
                {
                    Dictionary<string, string> iBANInfo = UiHelper.InputForm(
                        IBANInput,
                        $"Invalid input: {invalidInputs} please try again"
                    );

                    invalidInputs = PurchaseLogic.IBANCheck(iBANInfo);
                }
                else
                {
                    Dictionary<string, string> iBANInfo = UiHelper.InputForm(
                        IBANInput,
                        "Please fill in the payment information"
                    );

                    invalidInputs = PurchaseLogic.IBANCheck(iBANInfo);
                }

            } while (invalidInputs != "");
        }

        int reservationNumber = PurchaseLogic.GenerateReservationNumber();

        UiHelper.SelectionMenu([$"Payment successful. Reservation number: {reservationNumber}"], "");
        ReservationsLogic.CreateReservation(new ReservationModel(reservationNumber,AccountsLogic.CurrentAccount!.Id,TimetablesLogic.ConvertDateToUnixTime(convertedDateTime),(double)finalTotal,selectedTimetable.Id));
        return new TicketModel(null, null, convertedDateTime, selectedPaymentMethodString);
    }

    public static void SetUpDateMenu(MovieModel movie)
    {
        // get all timetables for movie
        List<TimetableModel> timetables = TimetablesLogic.GetTimeTablesByMovieId(movie.Id);

        foreach (TimetableModel timetable in timetables)
        {
            if (timetable.StartTime > TimetablesLogic.ConvertDateToUnixTime(DateTime.Now))
            {
                string date = TimetablesLogic.GetDateString(
                    TimetablesLogic.ConvertUnixTimeToDateTime(timetable.StartTime)
                );

                if (DateMenu.Contains(date) == false)
                {
                    DateMenu.Add(date);
                }
            }
        }
    }

    private static void SetUpTimeMenu(MovieModel movie, string dateString)
    {
        // get all times for selected date
        List<TimetableModel> timetables = TimetablesLogic.GetTimeTablesByMovieId(movie.Id);

        CurrentTimetables.Clear();

        foreach (TimetableModel timetable in timetables)
        {
            if (dateString == TimetablesLogic.GetDateString(TimetablesLogic.ConvertUnixTimeToDateTime(timetable.StartTime)))
            {
                DateTime now = DateTime.Now;

                if (TimetablesLogic.ConvertUnixTimeToDateTime(timetable.StartTime) > now)
                {
                    CurrentTimetables.Add(timetable);
                    TimeMenu.Add(
                        $"{TimetablesLogic.GetTimeString(TimetablesLogic.ConvertUnixTimeToDateTime(timetable.StartTime))} {RoomsLogic.GetRoomById(Convert.ToInt32(timetable.RoomId)).ScreenType}"
                    );
                }
            }
        }
    }

    // show booking summary before payment
    static void ShowBookingSummary(
        decimal ticketTotal,
        List<OrderItemModel> orderedMenuItems,
        decimal menuTotal,
        List<OrderItemModel> loungePreOrderItems,
        decimal loungePreOrderTotal,
        decimal finalTotal)
    {
        Console.Clear();

        Console.WriteLine($@"
Booking Summary

Ticket total: €{ticketTotal:0.00}
");

        if (orderedMenuItems.Count > 0)
        {
            Console.WriteLine($@"
Food and drink items:
");

            foreach (OrderItemModel item in orderedMenuItems)
            {
                Console.WriteLine($@"
Item name: {item.Name}
Quantity: {item.Quantity}
Price per item: €{item.PricePerItem:0.00}
Subtotal: €{item.SubTotal:0.00}
");
            }
        }
        else
        {
            Console.WriteLine($@"
No food or drinks selected.
");
        }

        Console.WriteLine($@"
Food and drink total: €{menuTotal:0.00}
");

        if (loungePreOrderItems.Count > 0)
        {
            Console.WriteLine($@"
Lounge pre-order drinks:
");

            foreach (OrderItemModel item in loungePreOrderItems)
            {
                Console.WriteLine($@"
Item name: {item.Name}
Quantity: {item.Quantity}
Price per item: €{item.PricePerItem:0.00}
Subtotal: €{item.SubTotal:0.00}
");
            }
        }
        else
        {
            Console.WriteLine($@"
No lounge drinks selected.
");
        }

        Console.WriteLine($@"
Lounge drink pre-order total: €{loungePreOrderTotal:0.00}
Final total: €{finalTotal:0.00}
");

        UiHelper.HoldUser();
    }
}
