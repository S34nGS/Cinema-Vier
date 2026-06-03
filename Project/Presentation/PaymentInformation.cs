public static class PaymentInformation
{
    public static List<string> PaymentMethods { get; } = ["Credit Card", "IBAN"];
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

    public static string Start()
    {
        int selectedPaymentMethod = UiHelper.SelectionMenu.WriteMenu(PaymentMethods, "How do you want to pay?");
        if (selectedPaymentMethod == -1)
        {
            return "-1";
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
        return selectedPaymentMethodString;
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
}
