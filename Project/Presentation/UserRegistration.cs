static class UserRegistration
{
    private static AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        List<string> fields = ["First Name", "Last Name", "Email", "Password (8-32 characters)", "Date of birth (dd/mm/yyyy)"];
        Dictionary<string, string> inputs = UiLib.InputForm(fields, "Please enter your registration information");
        DateTime.TryParseExact(inputs["Date of birth (dd/mm/yyyy)"], "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dateOfBirth);
        AccountModel? acc = accountsLogic.CreateAccount(inputs["Email"], inputs["Password (8-32 characters)"], inputs["First Name"], inputs["Last Name"], dateOfBirth);
        string errorMessage;
        while(acc == null)
        {
            errorMessage = "Account couldn't be created";

            inputs = UiLib.InputForm(inputs, "Please enter your registration information", header: errorMessage);
            DateTime.TryParseExact(inputs["Date of birth (dd/mm/yyyy)"], "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dateofBirth);
            acc = accountsLogic.CreateAccount(inputs["Email"], inputs["Password (8-32 characters)"], inputs["First Name"], inputs["Last Name"], dateofBirth);

        }

        Console.WriteLine("Account created successfully");
        UiLib.HoldUser();
        
        Menu.Start();
    }
}
