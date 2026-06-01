using Microsoft.Data.Sqlite;

public abstract class DefaultAccess
{
    protected SqliteConnection connection = new("Data Source=./Project/DataSources/project.db");
    public abstract void CreateTable();
}
