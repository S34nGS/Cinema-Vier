namespace UnitTests;

[TestClass]
// [Ignore("Temporarily skipped during development. Remove Ignore before running the test as evidence.")]
public sealed class LoginTests
{
    [DataTestMethod]
    [DataRow("john@example.com", "demo_password")]
    public void LoginValidCredentials_ReturnsAccount(string email, string password)
    {
        // Arrange
        AccountsLogic logic = new();

        // Act 
        AccountModel result = logic.CheckLogin(email, password);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(email, result.EmailAddress);
        Assert.AreEqual(logic.HashPassword(password), result.Password);
    }

    [DataTestMethod]
    [DataRow("john@example.com", "wrong")]
    [DataRow("wrong1", "demo_password")]
    [DataRow("wrong2", "wrong")]
    [DataRow("", "")]
    [DataRow(null, null)]
    public void LoginInvalidCredentials_ReturnsNull(string email, string password)
    {
        // Arrange
        AccountsLogic logic = new();

        // Act 
        AccountModel result = logic.CheckLogin(email, password);

        // Assert
        Assert.IsNull(result); 
    }
}
