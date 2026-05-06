public static class ReservationsLogic
{
    private static ReservationAccess _access = new();
    public static List<ReservationModel> GetFutureReservations(Int64 userId)
    {
        ReservationAccess access = new ReservationAccess();
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
        ReservationAccess access = new ReservationAccess();
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

    public static void CreateReservation(ReservationModel reservation)
    {
        _access.Write(reservation);
    }
}