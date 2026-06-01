// TODO: rewrite this entire file
static class PurchaseTicket
{
    public static List<string> DateMenu { get; } = [];
    public static List<string> TimeMenu { get; } = [];
    public static List<string> PaymentMethods { get; } = ["Credit Card", "IBAN"];
    private static List<TimetableModel> CurrentTimetables = [];

    private static SeatSelection seatSelection = new();
    public static List<string> CreditCardInput = [
        "Cardholder name",
        "Card number (13-19 digits)",
        "Expiration date (MM/YY)",
        "CVC/CVV code (3-4 digits)"
    ];

    public static List<string> IBANInput = [
        "Cardholder name",
        "IBAN number (for example: NL12 ABNA 1234 5678 90)"
    ];

    public static TicketModel? Start(MovieModel movie, AccountModel? customer = null)
    {
        // reset date menu
        DateMenu.Clear();
        SetUpDateMenu(movie);

        if (DateMenu.Count == 0)
        {
            int dates = UiHelper.SelectionMenu.WriteMenu(
                ["No available dates."],
                "Pick a date",
                true
            );

            if (dates == 0)
            {
                return null;
            }
        }

        int selectedDate = UiHelper.SelectionMenu.WriteMenu(DateMenu, "Pick a date");
        if (selectedDate == -1)
        {
            return null;
        }

        string selectedDateString = DateMenu[selectedDate];

        // reset time menu
        TimeMenu.Clear();
        SetUpTimeMenu(movie, selectedDateString);

        int selectedTime = UiHelper.SelectionMenu.WriteMenu(TimeMenu, "Pick a time");

        if (selectedTime == -1)
        {
            return null;
        }

        SeatSelection seatSelection = new();

        TimetableModel selectedTimetable = CurrentTimetables[selectedTime];
        List<SeatModel> selectedSeats = seatSelection.Start(selectedTimetable);

        string dateTimeString = $"{selectedDateString} {TimeMenu[selectedTime].Substring(0, 5)}";
        DateTime convertedDateTime = DateTime.ParseExact(dateTimeString, "dd-MM-yyyy HH:mm", null);

        // ticket price for summary
        double ticketTotal = 12.00;

        // selected menu items
        List<OrderItemModel> orderedMenuItems = [];

        // ask if user wants food or drinks before the movie
        List<string> orderMenuChoices =
        [
            "Continue without food and drinks before the movie",
            "Add food and drinks before the movie"
        ];

        int selectedOrderChoice = UiHelper.SelectionMenu.WriteMenu(orderMenuChoices, "Do you want to add snacks or drinks before the movie?");
        if (selectedOrderChoice == 1)
        {
            orderedMenuItems = FoodAndDrinkMenu.ShowFoodAndDrinkMenu();
        }

        // add free birthday popcorn gift if available
        if (AccountsLogic.CurrentAccount != null && AccountsLogic.CanUseFreePopcornGift(AccountsLogic.CurrentAccount))
        {
            // add free popcorn as birthday gift
            OrderItemModel freePopcornGift = new(
                0,
                "🎁 Birthday gift: Free popcorn",
                0.00,
                1
            );

            orderedMenuItems.Add(freePopcornGift);

            AccountsLogic accountsLogic = new();
            accountsLogic.UseFreePopcornGift(AccountsLogic.CurrentAccount);

            UiHelper.HoldUser("Happy birthday! A free popcorn gift has been added to your order.");
        }

        // selected lounge pre-order drinks
        List<OrderItemModel> loungePreOrderItems = [];

        // ask if user wants lounge pre-order drinks before the movie
        List<string> loungePreOrderChoices =
        [
            "Continue without lounge drink pre-order before the movie",
            "Add lounge drink pre-order before the movie"
        ];

        int selectedLoungePreOrderChoice = UiHelper.SelectionMenu.WriteMenu(
            loungePreOrderChoices,
            "Do you want to pre-order drinks from the lounge/bar before the movie?"
        );

        if (selectedLoungePreOrderChoice == 1)
        {
            // show only drinks for lounge pre-order
            MenuLogic loungeMenuLogic = new();
            loungePreOrderItems = FoodAndDrinkMenu.ShowOnlyDrinksMenu(loungeMenuLogic);
        }

        // calculate totals
        MenuLogic menuLogic = new();
        double menuTotal = menuLogic.CalculateMenuTotal(orderedMenuItems);

        // calculate lounge pre-order total
        double loungePreOrderTotal = menuLogic.CalculateMenuTotal(loungePreOrderItems);

        // calculate final total with lounge pre-order
        double finalTotal = PurchaseLogic.CalculateFullTotal(ticketTotal, menuTotal, loungePreOrderTotal);

        // show summary before payment
        BookingSummary.Start(
            ticketTotal,
            orderedMenuItems,
            menuTotal,
            loungePreOrderItems,
            loungePreOrderTotal,
            finalTotal
        );

        if (AccountsLogic.CurrentAccount == null && customer == null)
        {
            UserLogin.Start();
        }

        // user must accept terms before payment
        bool termsAccepted = AcceptTermsAndConditions();

        if (termsAccepted == false)
        {
            UiHelper.HoldUser("Purchase cancelled. You must accept the terms and conditions before payment.");
            return null;
        }

        int selectedPaymentMethod = UiHelper.SelectionMenu.WriteMenu(PaymentMethods, "How do you want to pay?");
        if (selectedPaymentMethod == -1)
        {
            return null;
        }

        string selectedPaymentMethodString = PaymentMethods[selectedPaymentMethod];
        string invalidInputs = "";
        Dictionary<string, string> paymentInfo = [];

        if (selectedPaymentMethodString == "Credit Card")
        {
            foreach (string field in CreditCardInput)
            {
                paymentInfo[field] = "";
            }

            do
            {
                paymentInfo = UiHelper.InputFormMenu.WriteMenu(
                    paymentInfo,
                    invalidInputs != "" ? $"Invalid input: {invalidInputs} please try again" : "Please fill in the payment information"
                );

                bool[] isValidInput = PurchaseLogic.CreditCardCheck(paymentInfo);
                invalidInputs = InValidMessage(isValidInput, "credit card");
            } while (invalidInputs != "");
        }
        else if (selectedPaymentMethodString == "IBAN")
        {
            foreach (string field in IBANInput)
            {
                paymentInfo[field] = "";
            }

            do
            {
                paymentInfo = UiHelper.InputFormMenu.WriteMenu(
                    paymentInfo,
                    invalidInputs != "" ? $"Invalid input: {invalidInputs} please try again" : "Please fill in the payment information"
                );

                bool[] isValidInput = PurchaseLogic.IBANCheck(paymentInfo);
                invalidInputs = InValidMessage(isValidInput, "iban");


            } while (invalidInputs != "");
        }

        UiHelper.SelectionMenu.WriteMenu([$"Payment successful."], "");

        ReservationsLogic.CreateReservation(new ReservationModel(-1, AccountsLogic.CurrentAccount!.Id, TimeLogic.ConvertDateToUnixTime(convertedDateTime), (double)finalTotal, selectedTimetable.Id, selectedSeats));

        return new TicketModel(-1, -1, convertedDateTime, selectedPaymentMethodString);
    }

    public static void SetUpDateMenu(MovieModel movie)
    {
        // get all timetables for movie
        List<TimetableModel> timetables = TimetablesLogic.GetTimeTablesByMovieId(movie.Id);

        foreach (TimetableModel timetable in timetables)
        {
            if (
                timetable.StartTime > TimeLogic.ConvertDateToUnixTime(DateTime.Now) &&
                timetable.StartTime < TimeLogic.ConvertDateToUnixTime(DateTime.Now.AddDays(14))
            )
            {
                string date = TimeLogic.ConvertDateString(
                    TimeLogic.ConvertUnixTimeToDateTime(timetable.StartTime),
                    "dd-MM-yyyy"
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
            if (dateString == TimeLogic.ConvertDateString(TimeLogic.ConvertUnixTimeToDateTime(timetable.StartTime), "dd-MM-yyyy"))
            {
                DateTime now = DateTime.Now;

                if (TimeLogic.ConvertUnixTimeToDateTime(timetable.StartTime) > now)
                {
                    CurrentTimetables.Add(timetable);
                    TimeMenu.Add(
                        $"{TimeLogic.ConvertDateString(TimeLogic.ConvertUnixTimeToDateTime(timetable.StartTime), "HH:mm")} {RoomsLogic.GetRoomById(Convert.ToInt32(timetable.RoomId)).ScreenType}"
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
Lounge pre-order drinks before the movie:
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

    public static string InValidMessage(bool[] isValidInput, string paymentMethod)
    {
        string message = "";
        if (paymentMethod == "credit card")
        {
            for (int i = 0; i < isValidInput.Length; i++)
            {
                if (isValidInput[i] == false)
                {
                    if (CreditCardInput[i] == "Cardholder name")
                    {
                        message += "Invalid name, ";
                    }
                    else if (CreditCardInput[i] == "Card number (13-19 digits)")
                    {
                        message += "Invalid card number, ";
                    }
                    else if (CreditCardInput[i] == "Expiration date (MM/YY)")
                    {
                        message += "Invalid date, ";
                    }
                    else if (CreditCardInput[i] == "CVC/CVV code (3-4 digits)")
                    {
                        message += "Invalid CVC/CVV code, ";
                    }
                }
            }
        }
        else if (paymentMethod == "iban")
        {
            for (int i = 0; i < isValidInput.Length; i++)
            {
                if (isValidInput[i] == false)
                {
                    if (CreditCardInput[i] == "Cardholder name")
                    {
                        message += "Invalid name, ";
                    }
                    else if (CreditCardInput[i].Contains("IBAN number"))
                    {
                        message += "Invalid IBAN number, ";
                    }
                }
            }
        }

        return message;
    }

    static bool AcceptTermsAndConditions()
    {
        // show terms before payment
    List<string> menu =
    [
        "Accept terms and continue",
        "Cancel purchase"
    ];

    string header = @"
=== Terms and Conditions ===

By continuing, you agree to the cinema rules and payment conditions.

Tickets are only valid for the selected movie, date and time.
The user is responsible for entering correct information.
Food and drinks cannot be refunded after purchase.
";
    int selected = UiHelper.SelectionMenu.WriteMenu(menu, header);
    return selected == 0;
    }
}
