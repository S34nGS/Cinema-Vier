static class UserLogin
{
    private static AccountsLogic _accountsLogic = new AccountsLogic();
    public static int LoginAttempts { get; private set; } = 0;

    public static async Task Start()
    {
        Dictionary<string, string> inputs = new Dictionary<string, string>() {
            { "Email", "" },
            { "Password", "" }
        };
        AccountModel? acc = null;
        string? errorMessage = null;

        while (acc == null && LoginAttempts < 3)
        {
            inputs["Password"] = "";
            inputs = UiHelper.InputFormMenu.WriteMenu(inputs, "Please enter your login information", header: errorMessage);
            acc = _accountsLogic.CheckLogin(inputs["Email"], inputs["Password"]);

            errorMessage = "No account found with that email and password";
            LoginAttempts++;
        }

        if (acc == null)
        {
            Console.WriteLine("Too many failed login attempts. Please try again later.");
            UiHelper.HoldUser();

            Console.WriteLine("DEBUG: Starting reset timer");
            _ = Task.Run(async () => await ResetLoginAttempts());

            Menu.Start();
            return;
        }

        Console.WriteLine($"Welcome back {acc.FirstName} {acc.LastName}");
        UiHelper.HoldUser();
        Menu.Start();
    }

    public async static Task ResetLoginAttempts()
    {
        await Task.Delay(30000);
        LoginAttempts = 0;
    }
}
