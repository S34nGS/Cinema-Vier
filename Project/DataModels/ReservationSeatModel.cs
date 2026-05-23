public class ReservationSeatModel
{
    public Int64 ReservationId { get; set; }
    public Int64 SeatId { get; set; }

    public ReservationSeatModel(Int64 reservationId, Int64 seatId)
    {
        ReservationId = reservationId;
        SeatId = seatId;
    }
}