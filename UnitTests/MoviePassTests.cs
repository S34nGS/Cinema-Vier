namespace UnitTests;

[TestClass]
public sealed class MoviePassTests
{
    [DataTestMethod]
    [DataRow("3", true, 3)]
    [DataRow("10", true, 10)]
    [DataRow("1", true, 1)]
    public void AmountOfPassPointsIsValid_ValidInput_ReturnsTrue(string input, bool expectedValid, int expectedAmount)
    {
        // act
        (bool isValid, int amount) = PurchaseLogic.amountOfPassPointsIsValid(input);

        // assert
        Assert.AreEqual(expectedValid, isValid);
        Assert.AreEqual(expectedAmount, amount);
    }

    [DataTestMethod]
    [DataRow("hello")]
    [DataRow("abc")]
    [DataRow("0")]
    public void AmountOfPassPointsIsValid_InvalidInput_ReturnsFalse(string input)
    {
        // act
        (bool isValid, int amount) = PurchaseLogic.amountOfPassPointsIsValid(input);

        // assert
        Assert.IsFalse(isValid);
    }

    [DataTestMethod]
    [DataRow(3, 5, 8)]
    [DataRow(10, 0, 10)]
    [DataRow(1, 99, 100)]
    public void TopUpMoviePass_AddsPointsToAccount(int topUpAmount, int startingPoints, int expectedPoints)
    {
        // arrange
        AccountModel account = new(0, "test@test.com", "password", "Test", "User", 0);
        account.PassPoints = startingPoints;
        AccountsLogic logic = new();
        logic.Login(account);

        // act
        PurchaseLogic.TopUpMoviePass(topUpAmount);

        // assert
        Assert.AreEqual(expectedPoints, AccountsLogic.CurrentAccount!.PassPoints);
    }

    [DataTestMethod]
    [DataRow(5, 1, true)]
    [DataRow(3, 3, true)]
    [DataRow(1, 1, true)]
    public void MoviePassCheck_SufficientPoints_ReturnsTrue(int startingPoints, int seatsCount, bool expected)
    {
        // arrange
        AccountModel account = new(0, "test@test.com", "password", "Test", "User", 0);
        account.PassPoints = startingPoints;
        AccountsLogic logic = new();
        logic.Login(account);

        // act
        bool result = PurchaseLogic.MoviePassCheck(seatsCount);

        // assert
        Assert.AreEqual(expected, result);
    }

    [DataTestMethod]
    [DataRow(0, 1)]
    [DataRow(2, 3)]
    [DataRow(0, 5)]
    public void MoviePassCheck_InsufficientPoints_ReturnsFalse(int startingPoints, int seatsCount)
    {
        // arrange
        AccountModel account = new(0, "test@test.com", "password", "Test", "User", 0);
        account.PassPoints = startingPoints;
        AccountsLogic logic = new();
        logic.Login(account);

        // act
        bool result = PurchaseLogic.MoviePassCheck(seatsCount);

        // assert
        Assert.IsFalse(result);
    }

    [DataTestMethod]
    [DataRow(5, 1, 4)]
    [DataRow(3, 3, 0)]
    [DataRow(10, 2, 8)]
    public void MoviePassCheck_DeductsPointsAfterPayment(int startingPoints, int seatsCount, int expectedPoints)
    {
        // arrange
        AccountModel account = new(0, "test@test.com", "password", "Test", "User", 0);
        account.PassPoints = startingPoints;
        AccountsLogic logic = new();
        logic.Login(account);

        // act
        PurchaseLogic.MoviePassCheck(seatsCount);

        // assert
        Assert.AreEqual(expectedPoints, AccountsLogic.CurrentAccount!.PassPoints);
    }
}