public static class PaymentInformation
{
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

    public static void Start()
    {

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
