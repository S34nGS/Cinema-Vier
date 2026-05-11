using System.Formats.Asn1;
using System.Text.RegularExpressions;

public class AccountsLogic
{
    public static AccountModel? CurrentAccount { get; private set; }
    private AccountsAccess _access = new();

    public string HashPassword(string password)
    {
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
    }

    public bool IsValidEmail(string email)
    {
        Match match = Regex.Match(email, @"([^ ])+@([A-Z])+\.([A-Z])+", RegexOptions.IgnoreCase);
        return match.Success;
    }

    // checks if password is empty or too short
    public bool IsValidPassword(string password)
    {
        if (String.IsNullOrEmpty(password))
        {
            return false;
        }

        if (password.Length < 6)
        {
            return false;
        }

        return true;
    }

    public bool IsValidDateOfBirth(DateTime dateOfBirth)
    {
        return dateOfBirth < DateTime.Today && dateOfBirth > DateTime.Today.AddYears(-120);
    }

    public static int CalculateAge(DateTime dateOfBirth)
    {
        int age = DateTime.Today.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;
        return age;
    }

    public AccountModel? CreateAccount(string email, string password, string firstName, string lastName, DateTime dateOfBirth)
    {
        // check email
        if (IsValidEmail(email) == false)
        {
            return null;
        }

        // check password
        if (IsValidPassword(password) == false)
        {
            return null;
        }
        
        // check date of birth
        if (IsValidDateOfBirth(dateOfBirth) == false)
        {
            return null;
        }

        // create account with first and last name
        AccountModel account = new AccountModel(0, email, HashPassword(password), firstName, lastName, TimetablesLogic.ConvertDateToUnixTime(dateOfBirth));

        _access.Write(account);

        account = _access.GetByEmail(email);
        return account;
    }

    public AccountModel? CheckLogin(string email, string password)
    {
        AccountModel acc = _access.GetByEmail(email);

        if (acc != null && acc.Password == HashPassword(password))
        {
            CurrentAccount = acc;
            return acc;
        }

        return null;
    }

    public void Login(AccountModel account)
    {
        CurrentAccount = account;
    }

    public static void Logout()
    {
        CurrentAccount = null;
    }
}
