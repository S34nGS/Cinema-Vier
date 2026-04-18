using Microsoft.Data.Sqlite;

public abstract class DefaultAccess
{
    protected abstract string Table { get; }

    protected abstract void CreateTable();
}