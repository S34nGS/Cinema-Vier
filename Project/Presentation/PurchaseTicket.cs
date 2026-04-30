static class PurchaseTicket
{
    public static List<string> DateMenu { get; } = [];
    public static List<string> TimeMenu { get; } = [];
    public static List<string> PaymentMethods { get; } = ["Credit Card", "IBAN"];

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

        // calculate totals
        MenuLogic menuLogic = new MenuLogic();
        decimal menuTotal = menuLogic.CalculateMenuTotal(orderedMenuItems);
        decimal finalTotal = ticketTotal + menuTotal;

        // show summary before payment
        ShowBookingSummary(ticketTotal, orderedMenuItems, menuTotal, finalTotal);

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
        ReservationsLogic.CreateReservation(new(reservationNumber, AccountsLogic.CurrentAccount.Id, selectedDateString, 10, 1));
        return new TicketModel(null, null, convertedDateTime, selectedPaymentMethodString);
    }

    private static void SetUpDateMenu(MovieModel movie)
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

        foreach (TimetableModel timetable in timetables)
        {
            if (dateString == TimetablesLogic.GetDateString(TimetablesLogic.ConvertUnixTimeToDateTime(timetable.StartTime)))
            {
                DateTime now = DateTime.Now;

                if (TimetablesLogic.ConvertUnixTimeToDateTime(timetable.StartTime) > now)
                {
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
        decimal finalTotal)
    {
        Console.Clear();

        Console.WriteLine($"Booking Summary");
        Console.WriteLine($"");

        Console.WriteLine($"Ticket total: €{ticketTotal:0.00}");
        Console.WriteLine($"");

        if (orderedMenuItems.Count > 0)
        {
            Console.WriteLine($"Food and drink items:");
            Console.WriteLine($"");

            foreach (OrderItemModel item in orderedMenuItems)
            {
                Console.WriteLine($"Item name: {item.Name}");
                Console.WriteLine($"Quantity: {item.Quantity}");
                Console.WriteLine($"Price per item: €{item.PricePerItem:0.00}");
                Console.WriteLine($"Subtotal: €{item.SubTotal:0.00}");
                Console.WriteLine($"");
            }
        }
        else
        {
            Console.WriteLine($"No food or drinks selected.");
            Console.WriteLine($"");
        }

        Console.WriteLine($"Food and drink total: €{menuTotal:0.00}");
        Console.WriteLine($"Final total: €{finalTotal:0.00}");
        Console.WriteLine($"");

        UiHelper.HoldUser();
    }
}