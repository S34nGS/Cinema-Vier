public static class PaymentInformation
{
    public static List<string> PaymentMethods { get; } = ["Credit Card", "IBAN", "Movie pass"];
    private static List<string> CreditCardInput = [
        "Cardholder name",
        "Card number (13-19 digits)",
        "Expiration date (MM/YY)",
        "CVC/CVV code (3-4 digits)"
    ];
    private static List<string> IBANInput = [
        "Cardholder name",
        "IBAN number (for example: NL12 ABNA 1234 5678 90)"
    ];

    public static (bool, string) Start(List<string>? paymentMethods, int seatsCount = 1)
    {
        if (paymentMethods is null)
        {
            paymentMethods = DecidePaymentmethods();
        }

        string selectedPaymentMethodString = "";

        while (true)
        {
            int selectedPaymentMethod = UiHelper.SelectionMenu.WriteMenu(paymentMethods, "How do you want to pay?");
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
                    paymentInfo = UiHelper.InputFormMenu.WriteMenu(
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
                    paymentInfo = UiHelper.InputFormMenu.WriteMenu(
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
                if (!PurchaseLogic.MoviePassCheck(seatsCount))
                {
                    UiHelper.SelectionMenu.WriteMenu([$"Not enough pass points"], "");
                }
                else
                {
                    break;
                }
            }
        }

        UiHelper.SelectionMenu.WriteMenu([$"Payment successful."], "");
        return (true, selectedPaymentMethodString);
    }

    private static string InValidMessage(bool[] isValidInput, string paymentMethod)
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

    public static List<string> DecidePaymentmethods()
    {
        List<string> PaymentMethods2 = [];
        foreach(string payment in PaymentInformation.PaymentMethods)
        {
            if(payment != "Movie pass")
            {
                PaymentMethods2.Add(payment);
            }
        }

        List<string> paymentMethods = AccountsLogic.CurrentAccount.PassPoints <= 0 ? PaymentMethods2 : PaymentMethods;
        return paymentMethods;
    }
}
