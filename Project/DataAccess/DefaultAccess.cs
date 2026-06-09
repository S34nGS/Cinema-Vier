using Microsoft.Data.Sqlite;

public abstract class DefaultAccess
{
    private static readonly string DbPath =
        Directory.Exists("./Project") ? "./Project/DataSources/project.db" :
        Directory.Exists("../Project") ? "../Project/DataSources/project.db" :
        Path.Combine(AppContext.BaseDirectory, "DataSources", "project.db");

    protected SqliteConnection connection = new($"Data Source={DbPath}");
    public abstract void CreateTable();
}
