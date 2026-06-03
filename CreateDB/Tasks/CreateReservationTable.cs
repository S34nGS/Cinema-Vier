public static class CreateReservationTable
{
    public static Task Execute()
    {
        ReservationAccess reservation = new();
        reservation.CreateTable();

        List<ReservationModel> reservationList = [
            new ReservationModel(0, 1, TimeLogic.ConvertDateToUnixTime(new DateTime(2026, 4, 29)), 10.5, 1, []),
            new ReservationModel(0, 1, TimeLogic.ConvertDateToUnixTime(new DateTime(2026, 4, 10)), 15.0, 2, []),
            new ReservationModel(0, 2, TimeLogic.ConvertDateToUnixTime(new DateTime(2026, 4, 30)), 20.0, 3, []),
            new ReservationModel(0, 2, TimeLogic.ConvertDateToUnixTime(new DateTime(2026, 4, 11)), 12.5, 1, []),
        ];

        foreach (ReservationModel item in reservationList)
        {
            reservation.Write(item);
        }

        return Task.CompletedTask;
    }
}