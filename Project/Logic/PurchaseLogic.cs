using System.Runtime.InteropServices;
using System.Security.Cryptography;

public class PurchaseLogic  
{
    public static TicketModel? CurrentPayment { get; private set; }
    public static Random rng = new();

    public static bool[] CreditCardCheck(Dictionary<string, string> creditCardInfo)
    {
        bool[] isValidInput = new bool[creditCardInfo.Count];

        foreach(KeyValuePair<string, string> info in creditCardInfo)
        {
            if(info.Key == "Cardholder name") 
            {
                isValidInput[0] = CardHolderNameCheck(info.Value);
            }
            else if(info.Key == "Card number (13-19 digits)")
            {
                isValidInput[1] = CreditCardNumberCheck(info.Value);
            }
            else if(info.Key == "Expiration date (MM/YY)")
            {
                isValidInput[2] = CreditCardExpirationDateCheck(info.Value);
            }
            else if(info.Key == "CVC/CVV code (3-4 digits)")
            {
                isValidInput[3] = CreditCardCVCCVVCheck(info.Value);
            }
        }

        return isValidInput;
    }

    public static bool[] IBANCheck(Dictionary<string, string> iBANInfo)
    {
        bool[] isValidInput = new bool[iBANInfo.Count];

        foreach(KeyValuePair<string, string> info in iBANInfo)
        {
            if(info.Key == "Cardholder name")
            {
                isValidInput[0] = CardHolderNameCheck(info.Value);
            }
            else if(info.Key == "IBAN number (for example: NL12 ABNA 1234 5678 90)")
            {
                isValidInput[1] = IBANNumberCheck(info.Value);
            }
        }

        return isValidInput;
    }

    private static bool CardHolderNameCheck(string fullname)
    {
        string fullnameWithoutSpaces = fullname.Replace(" ", "");
        string[] splitName = fullname.Split(' ');

        return fullnameWithoutSpaces.All(char.IsLetter)
        && fullname.Contains(' ')
        && splitName[0].Length > 2 && splitName[splitName.Length - 1].Length > 2
        && splitName[0].Length < 30 && splitName[splitName.Length - 1].Length < 30
        && char.IsUpper(splitName[0][0]) && char.IsUpper(splitName[splitName.Length - 1][0]);
    }

    private static bool CreditCardNumberCheck(string cardNumber)
    {
        cardNumber = cardNumber.Replace(" ", "");

        return cardNumber.All(char.IsDigit)
        && cardNumber.Length >= 13 && cardNumber.Length <= 19
        && CheckCardtype(cardNumber)
        && LuhnCheck(cardNumber);
    }
    private static bool CheckCardtype(string cardNumber)
    {
        string[][] cardtypes =
        {
            new[] {"4"},                             // Visa
            new[] {"51", "52", "53", "54", "55"},    // Mastercard
            new[] {"34", "37"}                       // Amex
        };

        return cardtypes[0].Contains(cardNumber.Substring(0, 1))
        || cardtypes[1].Contains(cardNumber.Substring(0, 2))
        || cardtypes[2].Contains(cardNumber.Substring(0, 2));
    }

    private static bool LuhnCheck(string cardNumber)
    {
        int sum = 0;
        bool shouldDouble = false;

        for (int i = cardNumber.Length - 1; i >= 0; i--)
        {

            int digit = (int)char.GetNumericValue(cardNumber[i]);

            if (shouldDouble)
            {
                digit *= 2;
                if (digit > 9)
                {
                    digit -= 9;
                }
            }

            sum += digit;
            shouldDouble = !shouldDouble;
        }

        return sum % 10 == 0;
    }

    private static bool CreditCardExpirationDateCheck(string expirationDate)
    {
        int month = Convert.ToInt32(expirationDate.Split("/")[0]);
        int year = Convert.ToInt32(expirationDate.Split("/")[1]) + 2000;
        DateTime expiryDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

        return expirationDate.Length == 5
        && expirationDate.Count(c => c == '/') == 1
        && expirationDate.Replace("/", "").All(char.IsDigit)
        && month >= 1 && month <= 12
        && expiryDate > DateTime.Today;
    }

    private static bool CreditCardCVCCVVCheck(string number)
    {
        return number.All(char.IsDigit)
        && number.Length == 3 || number.Length == 4
        && number.Distinct().Count() != 1;
    }

    private static bool IBANNumberCheck(string iban)
    {
        iban = iban.Replace(" ", "").ToUpper();

        return iban.Length >= 15 && iban.Length <= 34
        && char.IsLetter(iban[0]) && char.IsLetter(iban[1]) && char.IsDigit(iban[2]) && char.IsDigit(iban[3])
        && Mod97Check(iban);
    }

    // Validates an IBAN using the Mod-97 checksum algorithm.
    // Mod-97 check is a rule that every valid IBAN must follow.
    // Ensures the IBAN is correctly formatted
    private static bool Mod97Check(string iban)
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