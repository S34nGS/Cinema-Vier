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
        "IBAN number (for example: NL12 ABNA 1234 5678 90)",
    ];

    public static TicketModel? Start(MovieModel movie)
    {
        DateMenu.Clear();
        SetUpDateMenu(movie);
        if (DateMenu.Count == 0)
        {
            int dates = UiLib.SelectionMenu(
                ["No available dates."],
                "Pick a date",
                true
            );

            if (dates == 0) return null;
        }

        int selectedDate = UiLib.SelectionMenu(DateMenu, "Pick a date");
        if (selectedDate == -1) return null;
        string selectedDateString = DateMenu[selectedDate];

        string today = DateTime.Today.ToString("d");

        TimeMenu.Clear();
        SetUpTimeMenu(movie, selectedDateString);
        int selectedTime = UiLib.SelectionMenu(TimeMenu, "Pick a time");
        if (selectedTime == -1) return null;

        string dateTimeString = $"{selectedDateString} {TimeMenu[selectedTime].Substring(0,5)}";
        DateTime convertedDateTime = DateTime.Parse(dateTimeString);

        int selectedPaymentMethod = UiLib.SelectionMenu(PaymentMethods, "How do you want to pay?");
        if (selectedPaymentMethod == -1) return null;
        string selectedPaymentMethodString = PaymentMethods[selectedPaymentMethod];

        string invalidInputs = "";

        if(selectedPaymentMethodString == "Credit Card")
        {
            do
            {
                if(invalidInputs != "")
                {
                    Dictionary<string, string> creditCardInfo = UiLib.InputForm(CreditCardInput, $"Invalid input:{invalidInputs}please try again");
                    invalidInputs = PurchaseLogic.CreditCardCheck(creditCardInfo);
                }
                else
                {
                    Dictionary<string, string> creditCardInfo = UiLib.InputForm(CreditCardInput, "Please fill in the payment information");
                    invalidInputs = PurchaseLogic.CreditCardCheck(creditCardInfo);
                }
            } while(invalidInputs != "");
        }
        else if(selectedPaymentMethodString == "IBAN")
        {
            do
            {
                if(invalidInputs != "")
                {
                    Dictionary<string, string> iBANInfo = UiLib.InputForm(IBANInput, $"Invalid input:{invalidInputs}please try again");
                    invalidInputs = PurchaseLogic.IBANCheck(iBANInfo);
                }
                else
                {
                    Dictionary<string, string> iBANInfo = UiLib.InputForm(IBANInput, "Please fill in the payment information");
                    invalidInputs = PurchaseLogic.IBANCheck(iBANInfo);
                }
            } while(invalidInputs != "");
        }

        UiLib.SelectionMenu([$"Payment successful. Reservation number: {PurchaseLogic.GenerateReservationNumber()}"], "");
        return new TicketModel(null, null, convertedDateTime, selectedPaymentMethodString);
    }

    private static void SetUpDateMenu(MovieModel movie)
    {
        List<TimetableModel> timetables = TimetablesLogic.GetTimeTablesByMovieId(movie.Id);
        foreach (TimetableModel timetable in timetables)
        {
            if (timetable.StartTime > TimetablesLogic.ConvertDateToUnixTime(DateTime.Now))
            {
                DateMenu.Add(TimetablesLogic.GetDateString(TimetablesLogic.ConvertUnixTimeToDateTime(timetable.StartTime)));
            }
        }
    }

    private static void SetUpTimeMenu(MovieModel movie, string dateString)
    {
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
}