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
                fullname TEXT NOT NULL, 
                firstName TEXT NOT NULL,
                lastName TEXT NOT NULL，
                dateOfBirth INTEGER NOT NULL
            )";
        connection.Execute(sql);
    }

    public void Write(AccountModel account)
    {
        string sql = $"INSERT INTO {Table} (email, password, fullname, firstName, lastName, dateOfBirth) VALUES (@EmailAddress, @Password, @FullName, @FirstName, @LastName, @DateOfBirth)";
        connection.Execute(sql, new { account.EmailAddress, account.Password, account.FullName, account.FirstName, account.LastName, DateOfBirth = TimetablesLogic.ConvertDateToUnixTime(account.DateOfBirth)});
    }

    public AccountModel GetByEmail(string email)
    {
        string sql = $"SELECT * FROM {Table} WHERE email = @Email";
        var row = connection.QueryFirstOrDefault<dynamic>(sql, new { Email = email });
        if (row == null) return null;
        AccountModel account = new AccountModel(row.id, row.email, row.password, row.fullname, TimetablesLogic.ConvertUnixTimeToDateTime((long)row.dateOfBirth));
        return account;
    }

    public void Update(AccountModel account)
    {
        string sql =
            $"UPDATE {Table} SET email = @EmailAddress, password = @Password, fullname = @FullName, firstName = @FirstName, lastName = @LastName, dateOfBirth = @DateOfBirth WHERE id = @Id";
        connection.Execute(sql, new { account.Id, account.EmailAddress, account.Password, account.FullName, DateOfBirth = TimetablesLogic.ConvertDateToUnixTime(account.DateOfBirth) });
    }

    public void Delete(AccountModel account)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        connection.Execute(sql, new { Id = account.Id });
    }
}