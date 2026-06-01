using Dapper;

public class ReservationAccess : DefaultAccess, IAccess
{
    public static string Table { get; } = "Reservation";

    public override void CreateTable()
    {
        string sql = $@"
            CREATE TABLE IF NOT EXISTS {Table} (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                userId INTEGER NOT NULL,
                reservationDate INTEGER NOT NULL,
                totalPrice REAL NOT NULL,
                timeTableId INTEGER NOT NULL,

                FOREIGN KEY (userId) REFERENCES Account(id),
                FOREIGN KEY (timeTableId) REFERENCES TimeTable(id)
            )";
        connection.Execute(sql);
    }
    public ReservationModel Write(ReservationModel reservation)
    {
        string sql = $@"INSERT INTO {Table} 
            (userId, reservationDate, totalPrice, timeTableId) 
            VALUES (@UserId, @ReservationDate, @TotalPrice, @TimeTableId)
        ";
        connection.Execute(sql, reservation);
        reservation.Id = connection.QuerySingle<int>("SELECT last_insert_rowid()");

        foreach (SeatModel seat in reservation.Seats)
        {
            SeatReservationAccess seatReservationAccess = new();
            seatReservationAccess.Write(seat.Id, reservation.Id, reservation.TimeTableId);
        }

        return reservation;
    }
    public List<ReservationModel> GetReservationsByUserId(long userId)
    {
        string sql = $"SELECT * FROM {Table} WHERE userId = @UserId";
        return connection.Query<ReservationModel>(sql, new { UserId = userId }).AsList();
    }

    public TimetableModel? GetById(Int64 timetableId)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @TimetableId";
        return connection.QueryFirstOrDefault<TimetableModel>(sql, new { TimetableId = timetableId });
    }
}
