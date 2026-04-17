static class UserRegistration
{
    private static AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        List<string> fields = ["Full Name", "Email", "Password"];
        Dictionary<string, string> inputs = UiLib.InputForm(fields, "Please enter your registration information (Password must be at least 8 characters)");
        AccountModel? acc = accountsLogic.CreateAccount(inputs["Email"], inputs["Password"], inputs["Full Name"]);
        string errorMessage;
        while(acc == null)
        {
            errorMessage = "Account couldn't be created";

            inputs = UiLib.InputForm(inputs, "Please enter your registration information (Password must be at least 8 characters)", header: errorMessage);
            acc = accountsLogic.CreateAccount(inputs["Email"], inputs["Password"], inputs["Full Name"]);

        }

        Console.WriteLine("Account created successfully");
        UiLib.HoldUser();
        
        Menu.Start();
    }
}