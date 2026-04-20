using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ReservationModelTests
{
    [TestMethod]
    public void Constructor_SetsValuesCorrectly()
    {
        long id = 1;
        int userId = 2;
        string date = "2026-04-20";
        double price = 10.5;
        int timeTableId = 3;

        ReservationModel reservation = new ReservationModel(id, userId, date, price, timeTableId);

        Assert.AreEqual(1, reservation.Id);
        Assert.AreEqual(2, reservation.UserId);
        Assert.AreEqual("2026-04-20", reservation.ReservationDate);
        Assert.AreEqual(10.5, reservation.TotalPrice);
        Assert.AreEqual(3, reservation.TimeTableId);
    }
}