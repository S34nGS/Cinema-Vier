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

    public void Write(TicketModel ticket)
    {
        string sql = $@"INSERT INTO {Table} 
            (reservationId, seatId, price, movieId)
            VALUES (@ReservationId, @SeatId, @Price, @MovieId)";
        connection.Execute(sql, ticket);
    }

    public AccountModel GetByReservationId(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE reservationId = @ReservationId";
        return connection.QueryFirstOrDefault<AccountModel>(sql, new { ReservationId = id });
    }

    public AccountModel GetBySeatId(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE seatId = @SeatId";
        return connection.QueryFirstOrDefault<AccountModel>(sql, new { SeatId = id });
    }

    public void Update(TicketModel ticket)
    {
        string sql =
            $"UPDATE {Table} SET reservationId = @ReservationId, seatId = @SeatId, price = @Price, movieId = @MovieId WHERE id = @Id";
        connection.Execute(sql, ticket);
    }

    public void Delete(TicketModel ticket)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        connection.Execute(sql, new { Id = ticket.Id });
    }
}