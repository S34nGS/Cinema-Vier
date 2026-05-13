using Dapper;


public class AccountsAccess : DefaultAccess
{
    protected override string Table { get; } = "Account";

    public override void CreateTable()
    {
        string sql = $@"CREATE TABLE IF NOT EXISTS {Table} 
            (
                id INTEGER PRIMARY KEY AUTOINCREMENT, 
                email TEXT UNIQUE NOT NULL, 
                password TEXT NOT NULL, 
                fullName TEXT NOT NULL, 
                firstName TEXT NOT NULL,
                lastName TEXT NOT NULL,
                dateOfBirth INTEGER NOT NULL
            );";
        connection.Execute(sql);
    }

    public void Write(AccountModel account)
    {
        string sql = $"INSERT INTO {Table} (email, password, fullName, firstName, lastName, dateOfBirth) VALUES (@EmailAddress, @Password, @FullName, @FirstName, @LastName, @DateOfBirth)";
        connection.Execute(sql, account);
    }

    public AccountModel GetByEmail(string email)
    {
        string sql = $"SELECT * FROM {Table} WHERE email = @Email";
        return connection.QueryFirstOrDefault<AccountModel>(sql, new { Email = email });
    }

    public void Update(AccountModel account)
    {
        string sql =
            $"UPDATE {Table} SET email = @EmailAddress, password = @Password, fullName = @FullName, firstName = @FirstName, lastName = @LastName, dateOfBirth = @DateOfBirth WHERE id = @Id";
        connection.Execute(sql, account);
    }

    public void Delete(AccountModel account)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        connection.Execute(sql, new { Id = account.Id });
    }
}