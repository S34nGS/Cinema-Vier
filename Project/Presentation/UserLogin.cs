static class UserLogin
{
    private static AccountsLogic _accountsLogic = new AccountsLogic();


    public static void Start()
    {
        List<string> fields = ["Email", "Password"];
        Dictionary<string, string> inputs = UiHelper.InputForm(fields, "Please enter your login information");
        AccountModel? acc = _accountsLogic.CheckLogin(inputs["Email"], inputs["Password"]);
        string? errorMessage;

        while(acc == null)
        {
            // reset password field so it doesn't show the previous input
            inputs["Password"] = "";
            errorMessage = "No account found with that email and password";
            inputs = UiHelper.InputForm(inputs, "Please enter your login information", header: errorMessage);
            acc = _accountsLogic.CheckLogin(inputs["Email"], inputs["Password"]);
        }


        Console.WriteLine($"Welcome back {acc.FirstName} {acc.LastName}");
        UiHelper.HoldUser();
        Menu.Start();
    }
}
