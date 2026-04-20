using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ReservationModelTests
{
    [TestMethod]
    public void Constructor_SetsValuesCorrectly()
    {
        // Arrange
        long id = 1;
        int userId = 2;
        string date = "2026-04-20";
        double price = 10.5;
        int timeTableId = 3;

        // Act
        ReservationModel reservation = new ReservationModel(id, userId, date, price, timeTableId);

        // Assert
        Assert.AreEqual(id, reservation.Id);
        Assert.AreEqual(userId, reservation.UserId);
        Assert.AreEqual(date, reservation.ReservationDate);
        Assert.AreEqual(price, reservation.TotalPrice);
        Assert.AreEqual(timeTableId, reservation.TimeTableId);
    }
}