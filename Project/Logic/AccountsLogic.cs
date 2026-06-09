
public class AccountsLogic
{
    public static AccountModel? CurrentAccount { get; private set; }
    private readonly AccountsAccess _access = new();

    public static string HashPassword(string password)
    {
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
    }

    public static int CalculateAge(DateTime dateOfBirth)
    {
        int age = DateTime.Today.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;
        return age;
    }

    public AccountModel? CreateAccount(string email, string password, string firstName, string lastName, DateTime dateOfBirth)
    {
        if (!ValidDataLogic.IsValidEmail(email))
        {
            return null;
        }
        else if (!ValidDataLogic.IsValidPassword(password))
        {
            return null;
        }
        else if (!ValidDataLogic.IsValidDateOfBirth(dateOfBirth))
        {
            return null;
        }
        if (_access.GetByEmail(email) is not null)
        {
            return null;
        }

        AccountModel? account = new(0, email, HashPassword(password), firstName, lastName, TimeLogic.ConvertDateToUnixTime(dateOfBirth));

        _access.Write(account);

        account = _access.GetByEmail(email);
        return account;
    }

    public AccountModel? CheckLogin(string email, string password)
    {
        AccountModel? acc = _access.GetByEmail(email);

        if (acc != null && acc.Password == HashPassword(password))
        {
            CurrentAccount = acc;
            return acc;
        }

        return null;
    }

    public List<AccountModel> GetAllCustomerAccounts()
    {
        return _access.GetAllCustomerAccounts();
    }

    public AccountModel GetCustomerAsAdmin(string header)
    {
        List<AccountModel> rawAccounts = GetAllCustomerAccounts();
        List<string> accounts = [];
        foreach (AccountModel account in rawAccounts)
        {
            accounts.Add(account.FullName);
        }

        // TODO: make this it's own presentation layer file
        int selected = UiHelper.SelectionMenu.WriteMenu(accounts, header);
        return rawAccounts[selected];
    }

    public void Login(AccountModel account)
    {
        CurrentAccount = account;
    }

    public static void Logout()
    {
        CurrentAccount = null;
        MoviesLogic.ClearRecommendations();
    }

    public static bool IsBirthday(AccountModel account, DateTime movieDate)
    {
        // convert saved date of birth number back to a date
        DateTime dateOfBirth = TimeLogic.ConvertUnixTimeToDateTimeValue(account.DateOfBirth);

        // check if selected movie date is on user's birthday
        return dateOfBirth.Day == movieDate.Day &&
            dateOfBirth.Month == movieDate.Month;
    }

    public static bool CanUseFreePopcornGift(AccountModel account, DateTime movieDate)
    {
        // check if movie date is birthday and gift is not used in that year
        return IsBirthday(account, movieDate) &&
            account.FreePopcornGiftUsedYear != movieDate.Year;
    }

    public void UseFreePopcornGift(AccountModel account, DateTime movieDate)
    {
        account.FreePopcornGiftUsedYear = movieDate.Year;

        // update only the gift usage year
        _access.UpdateFreePopcornGiftUsedYear(account);
    }
}
