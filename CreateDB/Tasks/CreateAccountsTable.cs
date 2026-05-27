public static class CreateAccountsTable
{
    public static Task Execute()
    {
        AccountsAccess accounts = new();
        accounts.CreateTable();

        List<AccountModel> accountsList = [
            new AccountModel(1, "john@example.com","demo_password" , "John", "Doe" , TimetablesLogic.ConvertDateToUnixTime(new DateTime(2000, 1, 1))),
            new AccountModel(2, "jane@example.com", "demo_password", "Jane", "Smith", TimetablesLogic.ConvertDateToUnixTime(new DateTime(2010, 1, 1))),
            new AccountModel(3, "admin@example.com", "demo_password", "Admin", "Admin", TimetablesLogic.ConvertDateToUnixTime(new DateTime(2000, 1, 1)), 1),
            new AccountModel(3, "admin@example.com", "demo_password", "Admin", "Admin", TimetablesLogic.ConvertDateToUnixTime(new DateTime(2000, 1, 1)), 1),
            new AccountModel(4, "tom@example.com", "demo_password", "tom", "Smith", TimetablesLogic.ConvertDateToUnixTime(new DateTime(1995, 1, 1))),
        ];

        foreach (AccountModel account in accountsList)
        {
            account.Password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(account.Password));
            accounts.Write(account);
        }

        return Task.CompletedTask;
    }
}