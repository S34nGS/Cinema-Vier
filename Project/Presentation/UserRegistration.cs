static class UserRegistration
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        List<string> fields = ["Full Name", "Email", "Password"];
        Dictionary<string, string> inputs = UiLib.InputForm(fields, "Please enter your registration information");
        AccountModel acc = accountsLogic.CreateAccount(inputs["Email"], inputs["Password"], inputs["Full Name"]);
        string errorMessage = null;
        while(acc == null)
        {
            errorMessage = "Account couldn't be created";

            inputs = UiLib.InputForm(inputs, "Please enter your registration information", header: errorMessage);
            acc = accountsLogic.CreateAccount(inputs["Email"], inputs["Password"], inputs["Full Name"]);

        }

        if (acc != null)
        {
            Console.WriteLine("Welcome " + acc.FullName);
            Console.WriteLine("Your email number is " + acc.EmailAddress);

            Menu.Start();
        }
        else
        {
            Console.WriteLine("Account couldn't be created");
        }
    }
}