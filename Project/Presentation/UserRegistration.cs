static class UserRegistration
{
    private static AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        List<string> fields = ["Full Name", "Email", "Password (8-32 characters)"];
        Dictionary<string, string> inputs = UiLib.InputForm(fields, "Please enter your registration information");
        AccountModel? acc = accountsLogic.CreateAccount(inputs["Email"], inputs["Password (8-32 characters)"], inputs["Full Name"]);
        string errorMessage;
        while(acc == null)
        {
            errorMessage = "Account couldn't be created";

            inputs = UiLib.InputForm(inputs, "Please enter your registration information", header: errorMessage);
            acc = accountsLogic.CreateAccount(inputs["Email"], inputs["Password (8-32 characters)"], inputs["Full Name"]);

        }

        Console.WriteLine("Account created successfully");
        UiLib.HoldUser();
        
        Menu.Start();
    }
}