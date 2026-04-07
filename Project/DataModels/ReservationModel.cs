using System.ComponentModel.DataAnnotations.Schema;

// This class represents a reservation object
public class ReservationModel
{
    public Int64 Id { get; set; }

    // Foreign key to User
    [ForeignKey("User")]
    public Int64 UserId { get; set; }

    // Reservation date
    public string ReservationDate { get; set; }

    // Total price of reservation
    public double TotalPrice { get; set; }

    // Foreign key to TimeTable
    [ForeignKey("TimeTable")]
    public Int64 TimeTableId { get; set; }

    // Empty constructor
    public ReservationModel()
    {
    }

    // Constructor with parameters
    public ReservationModel(Int64 id, Int64 userId, string reservationDate, double totalPrice, Int64 timeTableId)
    {
        Id = id;
        UserId = userId;
        ReservationDate = reservationDate;
        TotalPrice = totalPrice;
        TimeTableId = timeTableId;
    }
}