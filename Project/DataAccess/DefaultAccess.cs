using Microsoft.Data.Sqlite;

public abstract class DefaultAccess
{
    protected SqliteConnection connection = new SqliteConnection("Data Source=./Project/DataSources/project.db");
    protected abstract string Table { get; }

    public abstract void CreateTable();
}