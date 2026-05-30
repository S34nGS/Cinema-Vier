using System.Net.Http.Headers;

static class PurchaseTicket
{
    public static List<string> DateMenu { get; } = [];
    public static List<string> TimeMenu { get; } = [];
    public static List<string> PaymentMethods { get; } = ["Credit Card", "IBAN", "Movie pass"];
    private static List<TimetableModel> CurrentTimetables = [];

    public static List<string> CreditCardInput =
    [
        "Cardholder name",
        "Card number (13-19 digits)",
        "Expiration date (MM/YY)",
        "CVC/CVV code (3-4 digits)"
    ];

    public static List<string> IBANInput =
    [
        "Cardholder name",
        "IBAN number (for example: NL12 ABNA 1234 5678 90)"
    ];

    public static TicketModel? Start(MovieModel movie, AccountModel customer = null)
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

        SeatSelection seatSelection = new();

        TimetableModel selectedTimetable = CurrentTimetables[selectedTime];
        List<SeatModel> selectedSeats = seatSelection.Start(selectedTimetable);

        string dateTimeString = $"{selectedDateString} {TimeMenu[selectedTime].Substring(0, 5)}";
        DateTime convertedDateTime = DateTime.ParseExact(dateTimeString, "dd-MM-yyyy HH:mm", null);

        // ticket price for summary
        decimal ticketTotal = 12.00m;

        // selected menu items
        List<OrderItemModel> orderedMenuItems = new List<OrderItemModel>();

        // ask if user wants food or drinks before the movie
        List<string> orderMenuChoices =
        [
            "Continue without food and drinks before the movie",
            "Add food and drinks before the movie"
        ];

        int selectedOrderChoice = UiHelper.SelectionMenu(orderMenuChoices, "Do you want to add snacks or drinks before the movie?");
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
                0.00m,
                1
            );

            orderedMenuItems.Add(freePopcornGift);

            AccountsLogic accountsLogic = new();
            accountsLogic.UseFreePopcornGift(AccountsLogic.CurrentAccount);

            UiHelper.HoldUser("Happy birthday! A free popcorn gift has been added to your order.");
        }

        // selected lounge pre-order drinks
        List<OrderItemModel> loungePreOrderItems = new List<OrderItemModel>();

        // ask if user wants lounge pre-order drinks before the movie
        List<string> loungePreOrderChoices =
        [
            "Continue without lounge drink pre-order before the movie",
            "Add lounge drink pre-order before the movie"
        ];

        int selectedLoungePreOrderChoice = UiHelper.SelectionMenu(
            loungePreOrderChoices,
            "Do you want to pre-order drinks from the lounge/bar before the movie?"
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

        (bool completePayment, string selectedPaymentMethodString) = Payment(PaymentMethods);

        if(!completePayment) return null;

        UiHelper.SelectionMenu([$"Payment successful."], "");
        ReservationsLogic.CreateReservation(new ReservationModel(-1, (customer != null) ? customer.Id : AccountsLogic.CurrentAccount!.Id, TimetablesLogic.ConvertDateToUnixTime(convertedDateTime), (double)finalTotal, selectedTimetable.Id, selectedSeats));

        return new TicketModel(null, null, convertedDateTime, selectedPaymentMethodString);
    }

    public static void SetUpDateMenu(MovieModel movie)
    {
        // get all timetables for movie
        List<TimetableModel> timetables = TimetablesLogic.GetTimeTablesByMovieId(movie.Id);

        foreach (TimetableModel timetable in timetables)
        {
            if (
                timetable.StartTime > TimetablesLogic.ConvertDateToUnixTime(DateTime.Now) &&
                timetable.StartTime < TimetablesLogic.ConvertDateToUnixTime(DateTime.Now.AddDays(14))
                )
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
                    else if (CreditCardInput[i] == "IBAN number (for example: NL12 ABNA 1234 5678 90)")
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
    int selected = UiHelper.SelectionMenu(menu, header);
    return selected == 0;
    }

    public static (bool, string) Payment(List<string> paymentMethods)
    {
        string selectedPaymentMethodString = "";

        while (true)
        {
            int selectedPaymentMethod = UiHelper.SelectionMenu(paymentMethods, "How do you want to pay?");
            if (selectedPaymentMethod == -1)
            {
                return (false, selectedPaymentMethodString);
            }

            selectedPaymentMethodString = paymentMethods[selectedPaymentMethod];
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
                    paymentInfo = UiHelper.InputForm(
                        paymentInfo,
                        invalidInputs != "" ? $"Invalid input: {invalidInputs} please try again" : "Please fill in the payment information"
                    );

                    bool[] isValidInput = PurchaseLogic.CreditCardCheck(paymentInfo);
                    invalidInputs = InValidMessage(isValidInput, "credit card");
                } while (invalidInputs != "");
                break;
            }
            else if (selectedPaymentMethodString == "IBAN")
            {
                foreach (string field in IBANInput)
                {
                    paymentInfo[field] = "";
                }

                do
                {
                    paymentInfo = UiHelper.InputForm(
                        paymentInfo,
                        invalidInputs != "" ? $"Invalid input: {invalidInputs} please try again" : "Please fill in the payment information"
                    );

                    bool[] isValidInput = PurchaseLogic.IBANCheck(paymentInfo);
                    invalidInputs = InValidMessage(isValidInput, "iban");

                } while (invalidInputs != "");
                break;
            }
            else if (selectedPaymentMethodString == "Movie pass")
            {
                if (!PurchaseLogic.MoviePassCheck())
                {
                    UiHelper.SelectionMenu([$"Not enough Pass points. Please use another payment method."], "");
                }
                else
                {
                    break;
                }
            }
        }
        return (true, selectedPaymentMethodString);
    }
}
