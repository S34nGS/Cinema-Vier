namespace UnitTests;

[TestClass]
public sealed class PurchaseTicketTests
{


    [DataTestMethod]
    [DataRow(12.0, 0.0, 0.0, 12.0)]
    [DataRow(24.0, 5.5, 0.0, 29.5)]
    [DataRow(12.0, 5.5, 3.0, 20.5)]
    [DataRow(0.0, 0.0, 0.0, 0.0)]
    [DataRow(36.0, 10.0, 5.0, 51.0)]
    public void CalculateFullTotal_ReturnsCorrectSum(double ticketTotal, double menuTotal, double loungeTotal, double expected)
    {
        // arrange

        // act
        double result = PurchaseLogic.CalculateFullTotal(ticketTotal, menuTotal, loungeTotal);

        // assert
        Assert.AreEqual(expected, result);
    }

    [DataTestMethod]
    [DataRow(1, 1, 12.0)]
    [DataRow(1, 2, 15.0)]
    [DataRow(1, 3, 18.0)]
    public void CalculateTicketPrice_SingleSeatPriority_ReturnsCorrectPrice(long row, long priority, double expected)
    {
        // arrange
        SeatModel seat = new(1, 1, row, 1, priority);
        List<SeatModel> seats = new() { seat };
        double ticketTotal = 0.0;

        // act
        seats.ForEach((seat) =>
        {
            ticketTotal += 12;
            if (seat.SeatPriority == 2)
            {
                ticketTotal += 3;
            }
            if (seat.SeatPriority == 3)
            {
                ticketTotal += 6;
            }
        });

        // assert
        Assert.AreEqual(expected, ticketTotal);
    }

    [TestMethod]
    public void CalculateTicketPrice_MultipleSeats_ReturnsCorrectTotal()
    {
        // arrange
        List<SeatModel> seats = new()
        {
            new(1, 1, 1, 1, 1),
            new(2, 1, 1, 2, 2),
            new(3, 1, 1, 3, 3)
        };
        double ticketTotal = 0.0;

        // act
        seats.ForEach((seat) =>
        {
            ticketTotal += 12;
            if (seat.SeatPriority == 2)
            {
                ticketTotal += 3;
            }
            if (seat.SeatPriority == 3)
            {
                ticketTotal += 6;
            }
        });

        // assert
        Assert.AreEqual(45.0, ticketTotal);
    }

    [TestMethod]
    public void CreditCardCheck_ValidCreditCard_ReturnsAllTrue()
    {
        // arrange
        Dictionary<string, string> creditCardInfo = new()
        {
            { "Cardholder name", "John Doe" },
            { "Card number (13-19 digits)", "4532015112830366" },
            { "Expiration date (MM/YY)", "12/30" },
            { "CVC/CVV code (3-4 digits)", "123" }
        };

        // act
        bool[] result = PurchaseLogic.CreditCardCheck(creditCardInfo);

        // assert
        Assert.AreEqual(4, result.Length);
        Assert.IsTrue(result[0]);
        Assert.IsTrue(result[1]);
        Assert.IsTrue(result[2]);
        Assert.IsTrue(result[3]);
    }

    [TestMethod]
    public void CreditCardCheck_InvalidCardNumber_ReturnsFalseForCardNumber()
    {
        // arrange
        Dictionary<string, string> creditCardInfo = new()
        {
            { "Cardholder name", "John Doe" },
            { "Card number (13-19 digits)", "1234567890123" },
            { "Expiration date (MM/YY)", "12/30" },
            { "CVC/CVV code (3-4 digits)", "123" }
        };

        // act
        bool[] result = PurchaseLogic.CreditCardCheck(creditCardInfo);

        // assert
        Assert.IsTrue(result[0]);
        Assert.IsFalse(result[1]);
        Assert.IsTrue(result[2]);
        Assert.IsTrue(result[3]);
    }

    [TestMethod]
    public void CreditCardCheck_InvalidName_ReturnsFalseForName()
    {
        // arrange
        Dictionary<string, string> creditCardInfo = new()
        {
            { "Cardholder name", "John" },
            { "Card number (13-19 digits)", "4532015112830366" },
            { "Expiration date (MM/YY)", "12/30" },
            { "CVC/CVV code (3-4 digits)", "123" }
        };

        // act
        bool[] result = PurchaseLogic.CreditCardCheck(creditCardInfo);

        // assert
        Assert.IsFalse(result[0]);
        Assert.IsTrue(result[1]);
        Assert.IsTrue(result[2]);
        Assert.IsTrue(result[3]);
    }

    [TestMethod]
    public void CreditCardCheck_InvalidExpirationDate_ReturnsFalseForDate()
    {
        // arrange
        Dictionary<string, string> creditCardInfo = new()
        {
            { "Cardholder name", "John Doe" },
            { "Card number (13-19 digits)", "4532015112830366" },
            { "Expiration date (MM/YY)", "01/20" },
            { "CVC/CVV code (3-4 digits)", "123" }
        };

        // act
        bool[] result = PurchaseLogic.CreditCardCheck(creditCardInfo);

        // assert
        Assert.IsTrue(result[0]);
        Assert.IsTrue(result[1]);
        Assert.IsFalse(result[2]);
        Assert.IsTrue(result[3]);
    }

    [TestMethod]
    public void CreditCardCheck_InvalidCvc_ReturnsFalseForCvc()
    {
        // arrange
        Dictionary<string, string> creditCardInfo = new()
        {
            { "Cardholder name", "John Doe" },
            { "Card number (13-19 digits)", "4532015112830366" },
            { "Expiration date (MM/YY)", "12/30" },
            { "CVC/CVV code (3-4 digits)", "11" }
        };

        // act
        bool[] result = PurchaseLogic.CreditCardCheck(creditCardInfo);

        // assert
        Assert.IsTrue(result[0]);
        Assert.IsTrue(result[1]);
        Assert.IsTrue(result[2]);
        Assert.IsFalse(result[3]);
    }

    // Tests for PurchaseLogic.IBANCheck
    [TestMethod]
    public void IBANCheck_ValidIBAN_ReturnsAllTrue()
    {
        // arrange
        Dictionary<string, string> ibanInfo = new()
        {
            { "Cardholder name", "John Doe" },
            { "IBAN number (for example: NL12 ABNA 1234 5678 90)", "NL91ABNA0417164300" }
        };

        // act
        bool[] result = PurchaseLogic.IBANCheck(ibanInfo);

        // assert
        Assert.AreEqual(2, result.Length);
        Assert.IsTrue(result[0]);
        Assert.IsTrue(result[1]);
    }

    [TestMethod]
    public void IBANCheck_InvalidIBAN_ReturnsFalseForIBAN()
    {
        // arrange
        Dictionary<string, string> ibanInfo = new()
        {
            { "Cardholder name", "John Doe" },
            { "IBAN number (for example: NL12 ABNA 1234 5678 90)", "INVALID123" }
        };

        // act
        bool[] result = PurchaseLogic.IBANCheck(ibanInfo);

        // assert
        Assert.IsTrue(result[0]);
        Assert.IsFalse(result[1]);
    }

    [TestMethod]
    public void IBANCheck_InvalidName_ReturnsFalseForName()
    {
        // arrange
        Dictionary<string, string> ibanInfo = new()
        {
            { "Cardholder name", "John" },
            { "IBAN number (for example: NL12 ABNA 1234 5678 90)", "NL91ABNA0417164300" }
        };

        // act
        bool[] result = PurchaseLogic.IBANCheck(ibanInfo);

        // assert
        Assert.IsFalse(result[0]);
        Assert.IsTrue(result[1]);
    }

    [TestMethod]
    public void MoviePassCheck_SufficientPoints_ReturnsTrue()
    {
        // arrange
        AccountModel account = new(0, "test@test.com", "password", "Test", "User", 0);
        account.PassPoints = 5;
        AccountsLogic logic = new();
        logic.Login(account);

        // act
        bool result = PurchaseLogic.MoviePassCheck(3);

        // assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void MoviePassCheck_InsufficientPoints_ReturnsFalse()
    {
        // arrange
        AccountModel account = new(0, "test@test.com", "password", "Test", "User", 0);
        account.PassPoints = 2;
        AccountsLogic logic = new();
        logic.Login(account);

        // act
        bool result = PurchaseLogic.MoviePassCheck(3);

        // assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GenerateReservationNumber_ReturnsPositiveInteger()
    {
        // arrange

        // act
        int result = PurchaseLogic.GenerateReservationNumber();

        // assert
        Assert.IsTrue(result > 0);
    }
}
