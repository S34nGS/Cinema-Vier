using Microsoft.Data.Sqlite;

public abstract class DefaultAccess
{
    protected SqliteConnection connection = new SqliteConnection($"Data Source=DataSources/project.db");
    protected abstract string Table { get; }

    protected abstract void CreateTable();
}