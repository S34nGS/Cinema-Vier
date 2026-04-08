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

    public AccountModel? CreateAccount(string email, string password, string fullName)
    {
        if (IsValidEmail(email))
        {
            AccountModel account = new AccountModel(0, email, HashPassword(password), fullName);

            _access.Write(account);

            account = _access.GetByEmail(email);
            return account;
        }
        return null;
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

    public void Logout()
    {
        CurrentAccount = null;
    }
}
