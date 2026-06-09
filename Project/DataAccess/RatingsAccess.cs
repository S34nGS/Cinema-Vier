using Dapper;

public class RatingsAccess : DefaultAccess, IAccess
{
    public static string Table { get; } = "Rating";

    public override void CreateTable()
    {
        string sql = $@"CREATE TABLE IF NOT EXISTS {Table} (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            userId INTEGER NOT NULL,
            movieId INTEGER NOT NULL,
            rating REAL NOT NULL,

            FOREIGN KEY (movieId) REFERENCES Movie(id),
            FOREIGN KEY (userId) REFERENCES Account(id)
        );";
        connection.Execute(sql);
    }

    public void Write(RatingModel rating)
    {
        string sql = $@"INSERT INTO {Table}
            (userId, movieId, rating) 
            VALUES (@UserId, @MovieId, @Rating)";
        connection.Execute(sql, rating);
    }

    public List<RatingModel> GetRatingsByMovieId(Int64 movieId)
    {
        // get all ratings for one movie
        string sql = $"SELECT * FROM {Table} WHERE movieId = @MovieId";
        return connection.Query<RatingModel>(sql, new { MovieId = movieId }).AsList();
    }
}