using Dapper;


public class SeatReservationAccess : DefaultAccess
{
    protected override string Table { get; } = "SeatReservation";

    public override void CreateTable()
    {
        string sql = $@"CREATE TABLE IF NOT EXISTS {Table} 
            (
                id INTEGER PRIMARY KEY AUTOINCREMENT, 
                seatId INTEGER NOT NULL,
                reservationId INTEGER NOT NULL,
                
                FOREIGN KEY (reservationId) REFERENCES Reservation(id),
                FOREIGN KEY (seatId) REFERENCES Seat(id)
            );";
        connection.Execute(sql);
    }

    public void Write(Int64 seatId, Int64 reservationId)
    {
        string sql = $"INSERT INTO {Table} (seatId, reservationId) VALUES (@SeatId, @ReservationId)";
        connection.Execute(sql, new { SeatId = seatId, ReservationId = reservationId });
    }
}