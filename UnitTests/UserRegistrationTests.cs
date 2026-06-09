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
        AccountModel result = l.CreateAccount(email, password, firstName, lastName, dateOfBirth);

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(email, result.EmailAddress);
        Assert.AreEqual(AccountsLogic.HashPassword(password), result.Password);
        Assert.AreEqual(firstName, result.FirstName);
        Assert.AreEqual(lastName, result.LastName);
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
        AccountModel result = l.CreateAccount(email, password, firstName, lastName, dateOfBirth);

        // assert
        Assert.IsNull(result);
    }
}
