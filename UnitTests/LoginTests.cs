namespace UnitTests;



[TestClass]
// [Ignore]
public sealed class LoginTests
{
    [DataTestMethod]
    [DataRow("john@example.com", "demo_password")]
    public void LoginValidCredentials(string m, string p)
    {
        // arrange
        AccountsLogic l = new();
        AccountsAccess access = new();

        // act
        AccountModel result = l.CheckLogin(m, p);

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(m, result.EmailAddress);
        Assert.AreEqual(AccountsLogic.HashPassword(p), result.Password);
    }

    [DataTestMethod]
    [DataRow("kevin@kevin.nl", "wrong")] // wrong password
    [DataRow("wrong1", "kevin")] // wrong email
    [DataRow("wrong2", "wrong")] // everything wrong
    [DataRow("", "")]
    [DataRow(null, null)]
    public void LoginInvalidCredentials(string m, string p)
    {
        // arrange
        AccountsLogic l = new();

        // act
        AccountModel result = l.CheckLogin(m, p);

        // assert
        Assert.IsNull(result);
    }
}
