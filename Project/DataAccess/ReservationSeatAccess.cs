using Dapper;

public class ReservationSeatAccess : DefaultAccess
{
    protected override string Table { get; } = "ReservationSeat";

    public override void CreateTable()
    {
        string sql = $@"
        CREATE TABLE IF NOT EXISTS {Table} (
            reservationId INTEGER NOT NULL,
            seatId INTEGER NOT NULL,

            FOREIGN KEY (reservationId) REFERENCES Reservation(id),
            FOREIGN KEY (seatId) REFERENCES Seat(id)
        )";

        connection.Execute(sql);
    }

    public void Write(ReservationSeatModel reservationSeat)
    {
        string sql = $@"
        INSERT INTO {Table}
        (reservationId, seatId)
        VALUES
        (@ReservationId, @SeatId)";

        connection.Execute(sql, reservationSeat);
    }

    public List<ReservationSeatModel> GetByReservationId(long reservationId)
    {
        string sql = $@"
        SELECT * FROM {Table}
        WHERE reservationId = @ReservationId";

        return connection.Query<ReservationSeatModel>(sql, new { ReservationId = reservationId }).AsList();
    }
}