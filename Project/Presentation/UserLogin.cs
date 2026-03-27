static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        List<string> fields = ["Email", "Password"];
        Dictionary<string, string> inputs = UiLib.InputForm(fields, "Please enter your login information");
        AccountModel acc = accountsLogic.CheckLogin(inputs["Email"], inputs["Password"]);
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