namespace UnitTests;

[TestClass]
public sealed class CancelShowingTests
{
    [DataTestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    public void GetReservationsByTimetableId_BookedShowing_ReturnsReservations(long timetableId)
    {
        // act
        List<ReservationModel> result = ReservationsLogic.GetReservationsByTimetableId(timetableId);

        // assert
        Assert.IsNotNull(result);
    }

    [DataTestMethod]
    [DataRow(10)]
    [DataRow(11)]
    [DataRow(12)]
    public void GetReservationsByTimetableId_UnbookedShowing_ReturnsEmptyList(long timetableId)
    {
        // act
        List<ReservationModel> result = ReservationsLogic.GetReservationsByTimetableId(timetableId);

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }

    [DataTestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    public void ShowingWithReservations_CannotBeDeleted(long timetableId)
    {
        // act
        List<ReservationModel> reservations = ReservationsLogic.GetReservationsByTimetableId(timetableId);
        bool canDelete = reservations.Count == 0;

        // assert
        Assert.IsFalse(canDelete);
    }

    [DataTestMethod]
    [DataRow(10)]
    [DataRow(11)]
    [DataRow(12)]
    public void ShowingWithoutReservations_CanBeDeleted(long timetableId)
    {
        // act
        List<ReservationModel> reservations = ReservationsLogic.GetReservationsByTimetableId(timetableId);
        bool canDelete = reservations.Count == 0;

        // assert
        Assert.IsTrue(canDelete);
    }
}