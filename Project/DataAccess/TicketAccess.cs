using Dapper;


public class TicketAccess : DefaultAccess
{
    protected override string Table { get; } = "Ticket";

    public override void CreateTable()
    {
        string sql = $@"CREATE TABLE IF NOT EXISTS {Table} (
            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            reservationId INTEGER NOT NULL,
            seatId INTEGER NOT NULL,
            price INTEGER NOT NULL,
            movieId INTEGER NOT NULL,

            FOREIGN KEY (seatId) REFERENCES Seat(id),
            FOREIGN KEY (reservationId) REFERENCES Reservation(id),
            FOREIGN KEY (movieId) REFERENCES Movie(id)
        );";
        connection.Execute(sql);
    }
}