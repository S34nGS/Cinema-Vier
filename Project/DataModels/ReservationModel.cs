public class ReservationModel
{
    public Int64 Id { get; set; }
    public int UserId { get; set; }
    public string ReservationDate { get; set; }
    public double TotalPrice { get; set; }
    public int TimeTableId { get; set; }
    public ReservationModel()
    {
    }
    public ReservationModel(Int64 id, int userId, string reservationDate, double totalPrice, int timeTableId)
    {
        Id = id;
        UserId = userId;
        ReservationDate = reservationDate;
        TotalPrice = totalPrice;
        TimeTableId = timeTableId;
    }
}