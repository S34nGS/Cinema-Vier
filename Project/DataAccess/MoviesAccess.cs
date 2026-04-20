using Dapper;

public class MoviesAccess : DefaultAccess
{
    protected override string Table { get; } = "Movies";

    public override void CreateTable()
    {
        string sql = $@"CREATE TABLE IF NOT EXISTS {Table} (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            title TEXT UNIQUE NOT NULL,
            duration INTEGER NOT NULL,
            summary TEXT NOT NULL,
            director TEXT NOT NULL,
            ageRating INTEGER NOT NULL,
            genre TEXT NOT NULL,
            releaseDate INTEGER NOT NULL
        );";
        connection.Execute(sql);
    }

    public void Write(MovieModel movie)
    {
        string sql = $@"INSERT INTO {Table} 
            (title, duration, summary, director, ageRating, genre, releaseDate) 
            VALUES (@Title, @Duration, @Summary, @Director, @AgeRating, @Genre, @ReleaseDate)";
        connection.Execute(sql, movie);
    }

    public List<MovieModel> GetAllMovies()
    {
        string sql = $"SELECT * FROM {Table}";
        return connection.Query<MovieModel>(sql).AsList();
    }

    public MovieModel GetByTitle(string title)
    {
        string sql = $"SELECT * FROM {Table} WHERE title = @Title";
        return connection.QueryFirstOrDefault<MovieModel>(sql, new { Title = title });
    }

    public List<MovieModel> GetByPartOfTitle(string pattern)
    {
        string sql = $"SELECT * FROM {Table} WHERE title LIKE @Pattern";
        return connection.Query<MovieModel>(sql, new { Pattern = $"%{pattern}%" }).AsList();
    }

    public void Update(MovieModel movie)
    {
        string sql = $@"UPDATE {Table} 
            SET title = @Title, duration = @Duration, summary = @Summary, director = @Director, ageRating = @AgeRating, genre = @Genre, releaseDate = @ReleaseDate
            WHERE id = @Id";
        connection.Execute(sql, movie);
    }

    public void Delete(MovieModel movie)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        connection.Execute(sql, new { Id = movie.Id });
    }
}