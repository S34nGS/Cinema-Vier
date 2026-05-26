using Dapper;


public class SeatReservationAccess : DefaultAccess, IAccess
{
    public static string Table { get; } = "SeatReservation";

    public override void CreateTable()
    {
        string sql = $@"CREATE TABLE IF NOT EXISTS {Table} 
            (
                id INTEGER PRIMARY KEY AUTOINCREMENT, 
                seatId INTEGER NOT NULL,
                reservationId INTEGER NOT NULL,
                timetableId INTEGER NOT NULL,
                
                FOREIGN KEY (reservationId) REFERENCES Reservation(id),
                FOREIGN KEY (seatId) REFERENCES Seat(id)
                FOREIGN KEY (timetableId) REFERENCES TimeTable(id)
            );";
        connection.Execute(sql);
    }

    public void Write(Int64 seatId, Int64 reservationId, Int64 timetableId)
    {
        string sql = $"INSERT INTO {Table} (seatId, reservationId, timetableId) VALUES (@SeatId, @ReservationId, @TimetableId)";
        connection.Execute(sql, new { SeatId = seatId, ReservationId = reservationId, TimetableId = timetableId });
    }
}