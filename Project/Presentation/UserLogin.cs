static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        List<string> fields = ["Email", "Password"];
        Dictionary<string, string> inputs = UiLib.InputForm(fields, "Please enter your login information");
        AccountModel? acc = accountsLogic.CheckLogin(inputs["Email"], inputs["Password"]);
        string? errorMessage = null;

        while(acc == null)
        {
            errorMessage = "No account found with that email and password";
            inputs = UiLib.InputForm(inputs, "Please enter your login information", header: errorMessage);
            acc = accountsLogic.CheckLogin(inputs["Email"], inputs["Password"]);

             if (acc == null)
            {
                errorMessage = "No account found with that email and password";
            }
        }


        Console.WriteLine("Welcome back " + acc.FullName);
        UiLib.HoldUser();

        if (acc != null)
        {
            Console.WriteLine("Welcome back " + acc.FullName);
            Console.WriteLine("Your email number is " + acc.EmailAddress);

            Menu.Start();
        }
        else
        {
            Console.WriteLine("No account found with that email and password");
        }
    }
}