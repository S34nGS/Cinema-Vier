public static class CreateAccountsTable
{
    public static Task Execute()
    {
        AccountsAccess accounts = new();
        accounts.CreateTable();

        List<AccountModel> accountsList = [
            new AccountModel(1, "john@example.com","demo_password" , "John", "Doe" , TimeLogic.ConvertDateToUnixTime(new DateTime(2000, 1, 1))),
            new AccountModel(2, "jane@example.com", "demo_password", "Jane", "Smith", TimeLogic.ConvertDateToUnixTime(new DateTime(2010, 1, 1))),
            new AccountModel(3, "admin@example.com", "demo_password", "Admin", "Admin", TimeLogic.ConvertDateToUnixTime(new DateTime(2000, 1, 1)), 1),
            new AccountModel(4, "tom@example.com", "demo_password", "tom", "Smith", TimeLogic.ConvertDateToUnixTime(new DateTime(1995, 1, 1))),
            new AccountModel(5, "bob@example.com", "demo_password", "bob", "robert", TimeLogic.ConvertDateToUnixTime(DateTime.Today.AddYears(-17).AddDays(-357)))
        ];

        foreach (AccountModel account in accountsList)
        {
            account.Password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(account.Password));
            accounts.Write(account);
        }

        return Task.CompletedTask;
    }
}