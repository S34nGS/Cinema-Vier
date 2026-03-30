using Microsoft.Data.Sqlite;

using Dapper;

public class MovieAccess : DefaultAccess
{
    protected override string Table { get; } = "Movies";
    protected override void CreateTable()
    {
        string sql = $@"CREATE TABLE IF NOT EXISTS {Table} 
            (id INTEGER AUTOINCREMENT, name TEXT UNIQUE NOT NULL, summary TEXT NOT NULL, director TEXT NOT NULL, X, ageRating INTEGER NOT NULL)";
        connection.Execute(sql);
    }
}