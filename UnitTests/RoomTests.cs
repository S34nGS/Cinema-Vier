namespace UnitTests;

[TestClass]
public sealed class RoomTests
{
    [DataTestMethod]
    [DataRow(1, "Standard", "7.1 Surround Sound")]
    [DataRow(2, "Dolby Cinema", "Dolby Atmos")]
    [DataRow(3, "IMAX", "IMAX Sound System")]
    public void GetRoomById_ValidId_ReturnsRoomWithData(long id, string screenType, string soundType)
    {
        // act
        RoomModel? result = RoomsLogic.GetRoomById(id);

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(id, result.Id);
        Assert.AreEqual(screenType, result.ScreenType);
        Assert.AreEqual(soundType, result.SoundType);
    }

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(999)]
    [DataRow(-1)]
    public void GetRoomById_InvalidId_ReturnsNull(long id)
    {
        // act
        RoomModel? result = RoomsLogic.GetRoomById(id);

        // assert
        Assert.IsNull(result);
    }

    [DataTestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    public void GetSeatsByRoomId_RoomHasAssignedSeats(long roomId)
    {
        // arrange
        SeatLogic l = new();

        // act
        List<SeatModel> result = l.GetSeatsByRoomId(roomId);

        // assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
    }

    [DataTestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    public void GetRoomSeatInfo_ReturnsValidData(long roomId)
    {
        // act
        (long RoomId, long MaxRow, long MaxSeatNumber) result = SeatLogic.GetRoomSeatInfo(roomId);

        // assert
        Assert.AreEqual(roomId, result.RoomId);
        Assert.IsTrue(result.MaxRow > 0);
        Assert.IsTrue(result.MaxSeatNumber > 0);
    }
}
