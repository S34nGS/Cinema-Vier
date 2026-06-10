public class ReservationModel : IModel, IHasUser, IHasTimetable
{
    public Int64 Id { get; set; }
    public Int64 UserId { get; set; }
    public Int64 ReservationDate { get; set; }
    public double TotalPrice { get; set; }
    public Int64 TimeTableId { get; set; }
    public List<SeatModel> Seats { get; set; } = [];


    public ReservationModel(
        Int64 id,
        Int64 userId,
        Int64 reservationDate,
        double totalPrice,
        Int64 timeTableId,
        List<SeatModel> seats
    ) : this(id, userId, reservationDate, totalPrice, timeTableId)
    {
        Seats = seats;
    }

    public ReservationModel(
        Int64 id,
        Int64 userId,
        Int64 reservationDate,
        double totalPrice,
        Int64 timeTableId
    )
    {
        Id = id;
        UserId = userId;
        ReservationDate = reservationDate;
        TotalPrice = totalPrice;
        TimeTableId = timeTableId;
    }
}
