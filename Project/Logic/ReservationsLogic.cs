public static class ReservationsLogic
{
    public static bool CreateReservation(int userId, string reservationDate, double totalPrice, int timeTableId)
    {
        if (userId <= 0)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(reservationDate))
        {
            return false;
        }

        if (timeTableId <= 0)
        {
            return false;
        }

        ReservationModel reservation = new ReservationModel
        {
            UserId = userId,
            ReservationDate = reservationDate,
            TotalPrice = totalPrice,
            TimeTableId = timeTableId
        };

        ReservationsAccess access = new ReservationsAccess();
        access.Write(reservation);

        return true;
    }
}
