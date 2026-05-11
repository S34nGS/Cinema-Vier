namespace UnitTests;

[TestClass]
[Ignore("Temporarily skipped during development. Remove Ignore before running the test as evidence.")]
public sealed class LoginTests
{
    [DataTestMethod]
    [DataRow("kevin@kevin.nl", "kevin")]
    public void LoginValidCredentials_ReturnsAccount(string email, string password)
    {
        // Arrange
        AccountsLogic logic = new();

        // Act 
        AccountModel result = logic.CheckLogin(email, password);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(email, result.EmailAddress);
        Assert.AreEqual(password, result.Password);
    }

    [DataTestMethod]
    [DataRow("kevin@kevin.nl", "wrong")]
    [DataRow("wrong1", "kevin")]
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
}        AccountsLogic l = new();

        // act 
        AccountModel result = l.CheckLogin(m, p);

        // assert
        Assert.IsNull(result);
    }
}
