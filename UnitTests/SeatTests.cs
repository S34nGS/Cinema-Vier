// namespace UnitTests;



// [TestClass]

// [Ignore("Temporarily skipped during development. Remove Ignore before running the test as evidence.")]
// public sealed class SeatTest
// {
//     public void UserChoosesAvailableSeat()
//     {
//         // Arrange
//         SeatsAccess _access = new();
//         Seat seat = new("E", "4", Availability: true);

//         // Act
//         bool selection = SeatsLogic.PickSeat(seat);

//         // Assert
//         Assert.AreEqual(true, selection);
//     }

//     public void UserChoosesUnavailableSeat()
//     {
//         // Arrange
//         SeatsAccess _access = new();
//         Seat seat = new("E", "4", Availability: false);

//         // Act
//         bool selection = SeatsLogic.PickSeat(seat);

//         // Assert
//         Assert.AreEqual(false, selection);
//     }
// }