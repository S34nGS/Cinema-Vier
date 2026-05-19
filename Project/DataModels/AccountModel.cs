public class AccountModel
{
    public Int64 Id { get; set; }
    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public string FullName { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Int64 DateOfBirth { get; set; }
    public Int64 IsAdmin { get; set; }

    public AccountModel(
        Int64 id,
        string email,
        string password,
        string firstName,
        string lastName,
        Int64 dateOfBirth,
        Int64 isAdmin = 0
    )
    {
        Id = id;
        EmailAddress = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        IsAdmin = isAdmin;
        FullName = $"{firstName} {lastName}".Trim();
    }

    public AccountModel(
        Int64 id,
        string email,
        string password,
        string fullName,
        string firstName,
        string lastName,
        Int64 dateOfBirth,
        Int64 isAdmin = 0
    ) : this(id, email, password, firstName, lastName, dateOfBirth, isAdmin)
    {
        FullName = fullName;
    }
}
