namespace UnitTests;

[TestClass]
public sealed class ManageTimetablesTests
{
    [DataTestMethod]
    [DataRow("12:00", true)]
    [DataRow("09:30", true)]
    [DataRow("23:59", true)]
    public void ValidateTimeString_ValidInput_ReturnsTrue(string time, bool expected)
    {
        // act
        bool result = TimetablesLogic.ValidateTimeString(time);

        // assert
        Assert.AreEqual(expected, result);
    }

    [DataTestMethod]
    [DataRow("24:00", false)]
    [DataRow("abc", false)]
    [DataRow("9:30", false)]
    public void ValidateTimeString_InvalidInput_ReturnsFalse(string time, bool expected)
    {
        // act
        bool result = TimetablesLogic.ValidateTimeString(time);

        // assert
        Assert.AreEqual(expected, result);
    }

    [DataTestMethod]
    [DataRow("12:00", 43200)]
    [DataRow("01:00", 3600)]
    [DataRow("00:30", 1800)]
    public void ConvertTimeStringToUnixTime_ValidInput_ReturnsCorrectSeconds(string time, long expected)
    {
        // act
        long result = TimetablesLogic.ConvertTimeStringToUnixTime(time);

        // assert
        Assert.AreEqual(expected, result);
    }

    [DataTestMethod]
    [DataRow("2", 1)]
    [DataRow("a", 1)]
    [DataRow("99", 1)]
    public void CreateTimetableAsAdmin_InvalidRoomNumber_ReturnsOne(string roomNumber, int expected)
    {
        // arrange
        MovieModel movie = MoviesLogic.GetById(1);

        // act
        int result = TimetablesLogic.CreateTimetableAsAdmin(movie, roomNumber, "20/06/2026", "12:00");

        // assert
        Assert.AreEqual(expected, result);
    }
}