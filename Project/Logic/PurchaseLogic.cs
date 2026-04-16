using System.Security.Cryptography;

public class PurchaseLogic  
{
    public static PurchaseModel? CurrentPayment { get; private set; }
    public static Random rng = new();

    public static string CreditCardCheck(Dictionary<string, string> creditCardInfo)
    {
        string invalidMessages = "";
        foreach(KeyValuePair<string, string> info in creditCardInfo)
        {
            if(info.Key == "Cardholder name") invalidMessages += CardHolderNameCheck(info.Value);
            else if(info.Key == "Card number (13-19 digits)") invalidMessages += CreditCardNumberCheck(info.Value);
            else if(info.Key == "Expiration date (MM/YY)") invalidMessages += CreditCardExpirationDateCheck(info.Value);
            else if(info.Key == "CVC/CVV code (3-4 digits)") invalidMessages += CreditCardCVCCVVCheck(info.Value);
        }
        return invalidMessages;
    }

    public static string IBANCheck(Dictionary<string, string> iBANInfo)
    {
        string invalidMessages = "";
        foreach(KeyValuePair<string, string> info in iBANInfo)
        {
            if(info.Key == "Cardholder name") invalidMessages += CardHolderNameCheck(info.Value);
            else if(info.Key == "IBAN number (for example: NL12 ABNA 1234 5678 90)") invalidMessages += IBANNumberCheck(info.Value);
        }
        return invalidMessages;
    }

    static string CardHolderNameCheck(string fullname)
    {
        string invalidMessages = "";
        string spacesRemovedFromInput = fullname.Replace(" ", "");
        string[] splitName = fullname.Split(' ');

        if (!spacesRemovedFromInput.All(char.IsLetter)) invalidMessages += "Special characters are not allowed, ";
        else if (!fullname.Contains(' '))invalidMessages += "Enter your full name, ";
        else if (splitName[0].Length < 2 || splitName[splitName.Length - 1].Length < 2) invalidMessages += "First and last name must be at least 2 letters, ";
        else if (splitName[0].Length > 30 || splitName[splitName.Length - 1].Length > 30)invalidMessages += "First and last name cannot exceed 30 letters, ";
        else if (!char.IsUpper(splitName[0][0]) || !char.IsUpper(splitName[splitName.Length - 1][0])) invalidMessages += "Name must start with a capital letter, ";

        return invalidMessages;
    }

    static string CreditCardNumberCheck(string cardNumber)
    {
        string invalidMessages = "";
        cardNumber = cardNumber.Replace(" ", "");

        if (!cardNumber.All(char.IsDigit)) invalidMessages += "Card number must contain only digits, ";
        else if (cardNumber.Length < 13 || cardNumber.Length > 19) invalidMessages += "Card number must be 13-19 digits, ";
        else if (!HasValidPrefix(cardNumber)) invalidMessages += "Card number is not from a known provider, ";
        // Check if card number is mathematically valid: catch mistyped numbers and random fake numbers
        else if (!LuhnCheck(cardNumber)) invalidMessages += "Card number is invalid, ";

        return invalidMessages;
    }
    static bool HasValidPrefix(string cardNumber)
    {
        // Visa
        if (cardNumber.StartsWith("4")) return true;

        // Mastercard
        if (cardNumber.Length >= 2)
        {
            int firstTwo = int.Parse(cardNumber[..2]);
            if (firstTwo >= 51 && firstTwo <= 55) return true;
        }

        // Amex
        if (cardNumber.StartsWith("34") || cardNumber.StartsWith("37")) return true;

        return false;
    }

    static bool LuhnCheck(string cardNumber)
    {
        int sum = 0;
        bool alternate = false;

        for (int i = cardNumber.Length - 1; i >= 0; i--)
        {
            int digit = cardNumber[i] - '0';

            if (alternate)
            {
                digit *= 2;
                if (digit > 9) digit -= 9;
            }

            sum += digit;
            alternate = !alternate;
        }
        return sum % 10 == 0;
    }

    static string CreditCardExpirationDateCheck(string expirationDate)
    {
        string invalidMessages = "";
        int month = Convert.ToInt32(expirationDate.Split("/")[0]);
        int year = Convert.ToInt32(expirationDate.Split("/")[1]) + 2000;
        DateTime expiryDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

        if (expirationDate.Length != 5 || expirationDate.Count(c => c == '/') != 1 || !expirationDate.Replace("/", "").All(char.IsDigit))
        {
            invalidMessages += "Invalid expiration date format, ";
        }
        else if (month < 1 || month > 12) invalidMessages += "Month must be 01-12, ";
        else if (expiryDate < DateTime.Today) invalidMessages += "Card is expired, ";

        return invalidMessages;
    }

    static string CreditCardCVCCVVCheck(string number)
    {
        string invalidMessages = "";

        if (!number.All(char.IsDigit)) invalidMessages += "CVC/CVV must contain only digits, ";
        else if (number.Length < 3 || number.Length > 4) invalidMessages += "CVC/CVV must be 3-4 digits, ";
        else if (number.Distinct().Count() == 1) invalidMessages += "CVC/CVV is invalid, ";

        return invalidMessages;
    }

    static string IBANNumberCheck(string iban)
    {
        string invalidMessages = "";
        iban = iban.Replace(" ", "").ToUpper();

        if (iban.Length < 4 || !char.IsLetter(iban[0]) || !char.IsLetter(iban[1]) || !char.IsDigit(iban[2]) || !char.IsDigit(iban[3]))
        {
            invalidMessages += "IBAN must start with a country code like NL, DE, BE, ";
        }
        else if (iban.Length < 15 || iban.Length > 34) invalidMessages += "Invalid IBAN length, ";
        else if (!Mod97Check(iban))invalidMessages += "IBAN is invalid, ";

        return invalidMessages;
    }

    static bool Mod97Check(string iban)
    {
        // Move the first 4 characters to the end
        string rearranged = iban[4..] + iban[..4];

        // Replace letters with numbers: A=10, B=11 ... Z=35
        string numericIban = string.Concat(
            rearranged.Select(c => char.IsLetter(c) ? (c - 'A' + 10).ToString() : c.ToString())
        );

        // Calculate remainder — a valid IBAN always gives 1
        int remainder = 0;
        foreach (char c in numericIban)
        {
            remainder = (remainder * 10 + (c - '0')) % 97;
        }

        return remainder == 1;
    }

    public static int GenerateReservationNumber()
    {
        return rng.Next();
    }
}