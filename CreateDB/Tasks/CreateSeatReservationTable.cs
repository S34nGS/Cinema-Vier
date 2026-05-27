public static class CreateSeatReservationTable
{
    public static Task Execute()
    {
        SeatReservationAccess seatReservation = new();
        seatReservation.CreateTable();
        return Task.CompletedTask;
    }
}