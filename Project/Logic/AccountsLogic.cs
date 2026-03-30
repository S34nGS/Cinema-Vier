

//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{

    public static AccountModel? CurrentAccount { get; private set; }
    private AccountsAccess _access = new();

    public AccountsLogic()
    {

    }

    public string HashPassword(string password)
    {
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
    }

    public AccountModel CreateAccount(string email, string password, string fullName)
    {
        AccountModel account = new AccountModel(0, email, HashPassword(password), fullName);

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

    public void Logout()
    {
        CurrentAccount = null;
    }
}
