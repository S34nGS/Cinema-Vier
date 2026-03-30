static class UserRegistration
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        List<string> fields = ["Full Name", "Email", "Password"];
        Dictionary<string, string> inputs = UiLib.InputForm(fields, "Please enter your registration information");
        AccountModel acc = accountsLogic.CreateAccount(inputs["Email"], inputs["Password"], inputs["Full Name"]);
        if (acc != null)
        {
            Console.WriteLine("Welcome back " + acc.FullName);
            Console.WriteLine("Your email number is " + acc.EmailAddress);

            //Write some code to go back to the menu
            //Menu.Start();
        }
        else
        {
            Console.WriteLine("No account found with that email and password");
        }
    }
}