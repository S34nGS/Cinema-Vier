// This class handles the business logic for reservations
public static class ReservationsLogic
{
    public static bool CreateReservation(int userId, string reservationDate, double totalPrice, int timeTableId)
    {
        // Check if user id is valid
        if (userId <= 0)
        {
            return false;
        }

        // Check if date is empty
        if (string.IsNullOrWhiteSpace(reservationDate))
        {
            return false;
        }

        // Check if timetable id is valid
        if (timeTableId <= 0)
        {
            return false;
        }

        // Create reservation object
        ReservationModel reservation = new ReservationModel
        {
            UserId = userId,
            ReservationDate = reservationDate,
            TotalPrice = totalPrice,
            TimeTableId = timeTableId
        };

        // Save reservation to database
        ReservationsAccess access = new ReservationsAccess();
        access.Write(reservation);

        return true;
    }
}