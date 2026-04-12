static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        List<string> fields = ["Email", "Password"];
        Dictionary<string, string> inputs = UiLib.InputForm(fields, "Please enter your login information");
        AccountModel? acc = accountsLogic.CheckLogin(inputs["Email"], inputs["Password"]);
        string? errorMessage;

        while(acc == null)
        {
            // reset password field so it doesn't show the previous input
            inputs["Password"] = "";
            errorMessage = "No account found with that email and password";
            inputs = UiLib.InputForm(inputs, "Please enter your login information", header: errorMessage);
            acc = accountsLogic.CheckLogin(inputs["Email"], inputs["Password"]);
        }


        Console.WriteLine("Welcome back " + acc.FullName);
        UiLib.HoldUser();
        Menu.Start();
    }
}