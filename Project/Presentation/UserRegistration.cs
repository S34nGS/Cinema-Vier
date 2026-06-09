static class UserRegistration
{
    private static AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        string[] fields = ["First Name", "Last Name", "Email", "Password (8-32 characters)", "Date of birth (dd/mm/yyyy)"];
        Dictionary<string, string> inputs = UiHelper.InputFormMenu.WriteMenu(fields, "Please enter your registration information");

        DateTime.TryParseExact(inputs["Date of birth (dd/mm/yyyy)"], "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dateOfBirth);

        AccountModel? acc = accountsLogic.CreateAccount(inputs["Email"], inputs["Password (8-32 characters)"], inputs["First Name"], inputs["Last Name"], dateOfBirth);

        string errorMessage;

        while (acc == null)
        {
            errorMessage = "Account couldn't be created";

            inputs = UiHelper.InputFormMenu.WriteMenu(inputs, "Please enter your registration information", header: errorMessage);

            DateTime.TryParseExact(inputs["Date of birth (dd/mm/yyyy)"], "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dateofBirth);

            acc = accountsLogic.CreateAccount(inputs["Email"], inputs["Password (8-32 characters)"], inputs["First Name"], inputs["Last Name"], dateofBirth);

        }

        Console.WriteLine("Account created successfully");
        UiHelper.HoldUser();

        Menu.Start();
    }

    private static string GetErrorMessage(RegistrationResult result) => result switch
    {
        RegistrationResult.InvalidEmail       => "Invalid email address",
        RegistrationResult.InvalidPassword    => "Password must be between 8 and 32 characters",
        RegistrationResult.InvalidDateOfBirth => "Invalid date of birth",
        RegistrationResult.EmailAlreadyExists => "An account with this email already exists",
        _                                     => "Invalid input"
    };
}
