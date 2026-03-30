using Microsoft.Data.Sqlite;

using Dapper;


public class AccountsAccess : DefaultAccess
{
    protected override string Table { get; } = "Accounts";
    protected override void CreateTable()
    {
        string sql = $@"CREATE TABLE IF NOT EXISTS {Table} 
            (id INTEGER AUTOINCREMENT, email TEXT UNIQUE NOT NULL, password TEXT NOT NULL, fullname TEXT NOT NULL)";
        connection.Execute(sql);
    }

    public void Write(AccountModel account)
    {
        string sql = $"INSERT INTO {Table} (email, password, fullname) VALUES (@EmailAddress, @Password, @FullName)";
        connection.Execute(sql, account);
    }

    public AccountModel GetByEmail(string email)
    {
        string sql = $"SELECT * FROM {Table} WHERE email = @Email";
        return connection.QueryFirstOrDefault<AccountModel>(sql, new { Email = email });
    }

    public void Update(AccountModel account)
    {
        string sql = $"UPDATE {Table} SET email = @EmailAddress, password = @Password, fullname = @FullName WHERE id = @Id";
        connection.Execute(sql, account);
    }

    public void Delete(AccountModel account)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        connection.Execute(sql, new { Id = account.Id });
    }
}