public static class ReservationsLogic
{
    public static List<ReservationModel> GetFutureReservations(Int64 userId)
    {
        ReservationsAccess access = new ReservationsAccess();
        List<ReservationModel> allReservations = access.GetReservationsByUserId(userId);

        List<ReservationModel> futureReservations = new List<ReservationModel>();

        foreach (ReservationModel reservation in allReservations)
        {
            DateTime reservationDate;

            if (DateTime.TryParse(reservation.ReservationDate, out reservationDate))
            {
                if (reservationDate.Date >= DateTime.Today)
                {
                    futureReservations.Add(reservation);
                }
            }
        }

        return futureReservations;
    }
    public static List<ReservationModel> GetPastReservations(Int64 userId)
    {
        ReservationsAccess access = new ReservationsAccess();
        List<ReservationModel> allReservations = access.GetReservationsByUserId(userId);

        List<ReservationModel> pastReservations = new List<ReservationModel>();

        foreach (ReservationModel reservation in allReservations)
        {
            DateTime reservationDate;

            if (DateTime.TryParse(reservation.ReservationDate, out reservationDate))
            {
                if (reservationDate.Date < DateTime.Today)
                {
                    pastReservations.Add(reservation);
                }
            }
        }

        return pastReservations;
    }
}