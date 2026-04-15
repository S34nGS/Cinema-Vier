static class PurchaseTicket
{
    static List<string> TimeMenu { get; } = new() {"9:30", "11:30", "13:30", "15:30", "17:30", "19:30", "21:30", "23:30"};
    static List<string> DateMenu { get; } = [];
    static List<string> PaymentMethods { get; } = new() {"Credit Card"};
    static List<string> CreditCardInput = new()
    {
        "Cardholder name",
        "Card number (13-19 digits, for example:4111 1111 1111 1111)",
        "Expiration date (MM/YY)",
        "CVC/CVV code (3-4 digits)"
    };
    static List<string> IBANInput = new()
    {
        "Cardholder name",
        "IBAN number (for example: NL12 ABNA 1234 5678 90)",
    };

    public static PurchaseModel Start()
    {
        SetUp_dateMenu();
        string selectedDate = DateMenu[UiLib.SelectionMenu(DateMenu, "Pick a date")];
        string today = DateTime.Today.AddDays(0).ToString("dd-MM-yyyy");

        string selectedTime;
        if(selectedDate == today)
        {
            List<string> customTimeMenu = CustomizeTimeMenu();
            selectedTime = TimeMenu[UiLib.SelectionMenu(customTimeMenu, "Pick a time")];
        }
        else
        {
            selectedTime = TimeMenu[UiLib.SelectionMenu(TimeMenu, "Pick a time")];
        }

        DateTime selectedDateTime = DateTime.ParseExact($"{selectedDate} {selectedTime}", "dd-MM-yyyy H:mm", null);
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

        UiLib.SelectionMenu(["Payment successful"], "");
        return new PurchaseModel(null, selectedDateTime, selectedPaymentMethod);
    }

    static void SetUp_dateMenu()
    {
        for (int i = 0; i < 14; i ++)
        {
            DateMenu.Add(DateTime.Today.AddDays(i).ToString("dd-MM-yyyy"));
        }
    }

    static List<string> CustomizeTimeMenu()
    {
        List<string> newTimeMenu = [];
        TimeSpan now = DateTime.Now.TimeOfDay;
        TimeSpan nowPlus20 = now.Add(TimeSpan.FromMinutes(20));

        foreach(string time in TimeMenu)
        {
            TimeSpan timeToCompare = TimeSpan.Parse(time);
            if(nowPlus20 <= timeToCompare)
            {
                newTimeMenu.Add(timeToCompare.ToString(@"h\:mm"));
            }
        }
        return newTimeMenu;
    }
}