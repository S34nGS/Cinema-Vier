static class PurchaseTicket
{
    public static List<string> DateMenu { get; } = [];
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

    public static PurchaseModel? Start(MovieModel movie)
    {
        DateMenu.Clear();
        SetUp_dateMenu(movie);
        if (DateMenu.Count == 0)
        {
            int dates = UiLib.SelectionMenu(
                ["No available dates."],
                "Pick a date",
                true
            );

            if (dates == 0)
            {
                return null;
            }
        }

        int selectedDate = UiLib.SelectionMenu(DateMenu, "Pick a date");
        if (selectedDate == -1) return null;

        string selectedDateString = DateMenu[selectedDate];

        string today = DateTime.Today.AddDays(0).ToString("dd-MM-yyyy");

        List<string> customTimeMenu = CustomizeTimeMenu(movie);

        int selectedTime = UiLib.SelectionMenu(customTimeMenu, "Pick a time");
        if (selectedTime == -1) return null;

        string dateTimeString = $"{selectedDateString} {customTimeMenu[selectedTime]}";
        DateTime convertedDateTime = DateTime.Parse(dateTimeString);

        string selectedPaymentMethod = PaymentMethods[UiLib.SelectionMenu(PaymentMethods, "How do you want to pay?")];
        string invalidInputs = "";

        if(selectedPaymentMethod == "Credit Card")
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
        else if(selectedPaymentMethod == "IBAN")
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
        return new PurchaseModel(null, convertedDateTime, selectedPaymentMethod);
    }

    private static void SetUp_dateMenu(MovieModel movie)
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

    private static List<string> CustomizeTimeMenu(MovieModel movie)
    {
        List<TimetableModel> timetables = TimetablesLogic.GetTimeTablesByMovieId(movie.Id);
        List<string> times = [];
        foreach (TimetableModel timetable in timetables)
        {
            DateTime now = DateTime.Now;
            if (TimetablesLogic.ConvertUnixTimeToDateTime(timetable.StartTime) > now)
            {
                times.Add(TimetablesLogic.GetTimeString(TimetablesLogic.ConvertUnixTimeToDateTime(timetable.StartTime)));
            }
        }
        return times;

    }
}