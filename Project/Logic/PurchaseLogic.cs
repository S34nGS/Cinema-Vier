public class PurchaseLogic  
{
    public static PurchaseModel? CurrentPayment { get; private set; }

    public static string CreditCardCheck(Dictionary<string, string> creditCardInfo)
    {
        string invalidMessages = "";
        foreach(var info in creditCardInfo)
        {
            if(info.Key == "Cardholder name")
            {
                invalidMessages += CardHolderNameCheck(info.Value);
            }
            else if(info.Key == "Card number (13-19 digits)")
            {
                invalidMessages += CreditCardNumberCheck(info.Value);
            }
            else if(info.Key == "Expiration date (MM/YY)")
            {
                invalidMessages += CreditCardExpirationDateCheck(info.Value);
            }
            else if(info.Key == "CVC/CVV code (3-4 digits)")
            {
                invalidMessages += CreditCardCVCCVVCheck(info.Value);
            }
        }
        return invalidMessages;
    }

    public static string IBANCheck(Dictionary<string, string> iBANInfo)
    {
        string invalidMessages = "";
        foreach(var info in iBANInfo)
        {
            if(info.Key == "Cardholder name")
            {
                invalidMessages += CardHolderNameCheck(info.Value);
            }
            else if(info.Key == "IBAN number (for example: NL12 ABNA 1234 5678 90)")
            {
                invalidMessages += IBANNumberCheck(info.Value);
            }
        }
        return invalidMessages;
    }

    static string CardHolderNameCheck(string fullname)
    {
        string invalidMessages = "";
        if(!fullname.Contains(' '))
        {
            invalidMessages += "Enter your full name, ";
            return invalidMessages;
        }
        
        string[] splitName = fullname.Split(' ');
        if (!char.IsUpper(splitName[0][0]) || !char.IsUpper(splitName[splitName.Count() - 1][0]))
        {
            invalidMessages += "Name must start with a capital letter, ";
            return invalidMessages;
        }

        string spacesRemovedFromInput = fullname.Replace(" ", "");
        if (!spacesRemovedFromInput.All(char.IsLetter))
        {
            invalidMessages += "special char are not allowed, ";
            return invalidMessages;
        }
        return invalidMessages;
    }

    static string CreditCardNumberCheck(string cardNumber)
    {
        string invalidMessages = "";
        if (!cardNumber.All(char.IsDigit))
        {
            invalidMessages += "Card number must contain only digits, ";
            return invalidMessages;
        }

        if(cardNumber.Length < 13 || cardNumber.Length > 19)
        {
            invalidMessages += "Card number must be 13-19 digits, ";
            return invalidMessages;
        }
        return invalidMessages;
    }

    static string CreditCardExpirationDateCheck(string expirationDate)
    {
        string invalidMessages = "";
        if(expirationDate.Length != 5 || expirationDate.Count(c => c == '/') != 1 || !expirationDate.Replace("/", "").All(char.IsDigit))
        {
            invalidMessages += "invalid expiration date format, ";
            return invalidMessages;
        }

        int month = Convert.ToInt32(expirationDate.Split("/")[0]);
        if(month < 1 || month > 12)
        {
            invalidMessages += "Month must be 01-12, ";
            return invalidMessages;
        }
        return invalidMessages;
    }

    static string CreditCardCVCCVVCheck(string number)
    {
        string invalidMessages = "";
        if(number.Length < 3 || number.Length > 4)
        {
            invalidMessages += "CVC/CVV must be 3-4 digits, ";
            return invalidMessages;
        }

        if(!number.All(char.IsDigit))
        {
            invalidMessages += "CVC/CVV must contain only digits, ";
            return invalidMessages;
        }
        return invalidMessages;
    }

    static string IBANNumberCheck(string cardNumber)
    {
        string invalidMessages = "";
        cardNumber = cardNumber.Replace(" ", "");
        if(cardNumber.Length != 18)
        {
            invalidMessages += "invalid IBAN length, ";
            return invalidMessages;
        }
        if (!cardNumber.StartsWith("NL"))
        {
            invalidMessages += "IBAN must start with NL, ";
            return invalidMessages;
        }

        if (!char.IsLetter(cardNumber[4]) || !char.IsLetter(cardNumber[5]) || !char.IsLetter(cardNumber[6]) || !char.IsLetter(cardNumber[7]))
        {
            invalidMessages += "Bank code invalid, ";
            return invalidMessages;
        }

        for (int i = 8; i < 18; i++)
        {
            if (!char.IsDigit(cardNumber[i]))
            {
                invalidMessages += "Account number invalid, ";
                return invalidMessages;
            }
        }
        return invalidMessages;
    }
}
