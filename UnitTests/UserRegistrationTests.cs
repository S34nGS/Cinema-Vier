namespace UnitTests;

[TestClass]
public sealed class UserRegistrationTests
{
    [DataTestMethod]
    [DataRow("newuser@example.com", "password123", "John", "Doe", "01/01/2000")]
    public void RegisterValidCredentials(string email, string password, string firstName, string lastName, string dateOfBirthStr)
    {
        // arrange
        AccountsLogic l = new();
        DateTime dateOfBirth = DateTime.ParseExact(dateOfBirthStr, "dd/MM/yyyy", null);

        // act
        (AccountModel? account, RegistrationResult _) = l.CreateAccount(email, password, firstName, lastName, dateOfBirth);

        // assert
        Assert.IsNotNull(account);
        Assert.AreEqual(email, account!.EmailAddress);
        Assert.AreEqual(AccountsLogic.HashPassword(password), account.Password);
        Assert.AreEqual(firstName, account.FirstName);
        Assert.AreEqual(lastName, account.LastName);
    }

    [DataTestMethod]
    [DataRow("invalid-email", "password123", "John", "Doe", "01/01/2000")]
    [DataRow("test@test.com", "short", "John", "Doe", "01/01/2000")]
    [DataRow("test@test.com", "", "John", "Doe", "01/01/2000")]
    [DataRow("test@test.com", null, "John", "Doe", "01/01/2000")]
    [DataRow("test@test.com", "password123", "John", "Doe", "01/01/2100")]
    [DataRow("test@test.com", "password123", "John", "Doe", "01/01/1800")]
    public void RegisterInvalidCredentials(string email, string password, string firstName, string lastName, string dateOfBirthStr)
    {
        // arrange
        AccountsLogic l = new();
        DateTime dateOfBirth = DateTime.ParseExact(dateOfBirthStr, "dd/MM/yyyy", null);

        // act
        (AccountModel? account, RegistrationResult _) = l.CreateAccount(email, password, firstName, lastName, dateOfBirth);

        // assert
        Assert.IsNull(account);
    }
}
