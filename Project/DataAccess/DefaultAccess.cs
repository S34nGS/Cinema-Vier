using Microsoft.Data.Sqlite;

public abstract class DefaultAccess
{
    protected SqliteConnection connection;
    protected abstract string Table { get; }

    public DefaultAccess()
    {
        connection = new SqliteConnection("Data Source=DataSources/project.db");
        connection.Open();

        CreateTable();
    }

    protected abstract void CreateTable();
}