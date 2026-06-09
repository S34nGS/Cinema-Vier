namespace UnitTests;

[TestClass]
public sealed class SeatSelectionTests
{
    [DataTestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    public void GetSeatsInLayoutArray_ReturnsValidSeatMap(long roomId)
    {
        // arrange
        SeatLogic l = new();

        // act
        SeatModel[,] result = l.GetSeatsInLayoutArray(roomId);

        // assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Length > 0);
    }

    [DataTestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    public void GetSeatsByRoomId_ReturnsSeatsWithValidData(long roomId)
    {
        // arrange
        SeatLogic l = new();

        // act
        List<SeatModel> result = l.GetSeatsByRoomId(roomId);

        // assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
        foreach (SeatModel seat in result)
        {
            Assert.IsTrue(seat.Id > 0);
            Assert.IsTrue(seat.Row > 0);
            Assert.IsTrue(seat.SeatNumber > 0);
        }
    }

    [DataTestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    public void GetUnavailableSeatsByTimetableId_ReturnsValidList(long timetableId)
    {
        // arrange
        SeatLogic l = new();

        // act
        List<SeatModel> result = l.GetUnavailableSeatsByTimetableId(timetableId);

        // assert
        Assert.IsNotNull(result);
        // Empty list means all seats are available, which is valid
    }

    [TestMethod]
    public void UserCanSelectMultipleSeats()
    {
        // arrange
        SeatModel seat1 = new SeatModel(1, 1, 1, 1, 1);
        SeatModel seat2 = new SeatModel(2, 1, 1, 2, 1);
        List<SeatModel> selectedSeats = new();

        // act
        selectedSeats.Add(seat1);
        selectedSeats.Add(seat2);

        // assert
        Assert.AreEqual(2, selectedSeats.Count);
    }

    [DataTestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    public void GetById_ReturnsSeatWithValidData(long seatId)
    {
        // arrange
        SeatLogic l = new();

        // act
        SeatModel? result = SeatLogic.GetById(seatId);

        // assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Id > 0);
        Assert.IsTrue(result.Row > 0);
        Assert.IsTrue(result.SeatNumber > 0);
    }

    [DataTestMethod]
    [DataRow(1)]
    public void UnavailableSeatsAreTracked(long timetableId)
    {
        // arrange
        SeatLogic l = new();
        List<SeatModel> unavailableSeats = l.GetUnavailableSeatsByTimetableId(timetableId);

        // assert
        Assert.IsNotNull(unavailableSeats);
        // If there are unavailable seats, they should be valid seats
        foreach (SeatModel seat in unavailableSeats)
        {
            Assert.IsTrue(seat.Id > 0);
        }
    }

    [TestMethod]
    public void UserCanUnselectSeat()
    {
        // arrange
        SeatModel seat = new SeatModel(1, 1, 1, 1, 1);
        List<SeatModel> selectedSeats = new();
        selectedSeats.Add(seat);

        // act
        selectedSeats = selectedSeats.Where(x => x.Id != seat.Id).ToList();

        // assert
        Assert.AreEqual(0, selectedSeats.Count);
        Assert.IsNull(selectedSeats.FirstOrDefault(x => x.Id == seat.Id));
    }
}
