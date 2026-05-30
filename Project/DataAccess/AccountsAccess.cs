using Dapper;


public class AccountsAccess : DefaultAccess, IAccess
{
    public static string Table { get; } = "Account";

    public override void CreateTable()
    {
        string sql = $@"CREATE TABLE IF NOT EXISTS {Table} 
            (
                id INTEGER PRIMARY KEY AUTOINCREMENT, 
                email TEXT UNIQUE NOT NULL, 
                password TEXT NOT NULL, 
                fullname TEXT NOT NULL, 
                firstName TEXT NOT NULL,
                lastName TEXT NOT NULL,
                dateOfBirth INTEGER NOT NULL,
                isAdmin INTEGER NOT NULL,
                freePopcornGiftUsedYear INTEGER NOT NULL,
                passPoints INTEGER NOT NULL
            );";
        connection.Execute(sql);
    }

    public void Write(AccountModel account)
    {
        string sql = $"INSERT INTO {Table} (email, password, fullname, firstName, lastName, dateOfBirth, isAdmin, freePopcornGiftUsedYear, passPoints) VALUES (@EmailAddress, @Password, @FullName, @FirstName, @LastName, @DateOfBirth, @IsAdmin, @FreePopcornGiftUsedYear, @PassPoints)";
        connection.Execute(sql, account);
    }

    public AccountModel GetByEmail(string email)
    {
        string sql = $"SELECT * FROM {Table} WHERE email = @Email";
        return connection.QueryFirstOrDefault<AccountModel>(sql, new { Email = email });
    }

    public List<AccountModel> GetAllCustomerAccounts()
    {
        string sql = $"SELECT * FROM {Table} WHERE IsAdmin = 0";
        return connection.Query<AccountModel>(sql).AsList();
    }

    public void Update(AccountModel account)
    {
        string sql =
            $"UPDATE {Table} SET email = @EmailAddress, password = @Password, fullname = @FullName, firstName = @FirstName, lastName = @LastName, dateOfBirth = @DateOfBirth, isAdmin = @IsAdmin, freePopcornGiftUsedYear = @FreePopcornGiftUsedYear, passPoints = @PassPoints WHERE id = @Id";
        connection.Execute(sql, account);
    }

    public void UpdateFreePopcornGiftUsedYear(AccountModel account)
    {
        // update only birthday popcorn gift usage year
        string sql = $"UPDATE {Table} SET freePopcornGiftUsedYear = @FreePopcornGiftUsedYear WHERE id = @Id";
        connection.Execute(sql, account);
    }

    public void Delete(AccountModel account)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        connection.Execute(sql, new { Id = account.Id });
    }
}