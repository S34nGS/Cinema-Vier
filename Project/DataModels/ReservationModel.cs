using System.ComponentModel.DataAnnotations.Schema;

public class ReservationModel
{
    public Int64 Id { get; set; }

    [ForeignKey("User")]
    public Int64 UserId { get; set; }

    public string ReservationDate { get; set; }

    public double TotalPrice { get; set; }

    [ForeignKey("TimeTable")]
    public Int64 TimeTableId { get; set; }

    public ReservationModel()
    {
    }

    public ReservationModel(Int64 id, Int64 userId, string reservationDate, double totalPrice, Int64 timeTableId)
    {
        Id = id;
        UserId = userId;
        ReservationDate = reservationDate;
        TotalPrice = totalPrice;
        TimeTableId = timeTableId;
    }
}
