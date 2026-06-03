public class AccountModel : IModel
{
    public Int64 Id { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Int64 DateOfBirth { get; set; }
    public Int64 IsAdmin { get; set; }
    public Int64 FreePopcornGiftUsedYear { get; set; }
    public Int64 PassPoints { get; set; }


    public AccountModel()
    {
        EmailAddress = string.Empty;
        Password = string.Empty;
        FullName = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public AccountModel(
        Int64 id,
        string email,
        string password,
        string firstName,
        string lastName,
        Int64 dateOfBirth,
        Int64 isAdmin = 0,
        Int64 freePopcornGiftUsedYear = 0,
        Int64 passPoints = 0
    )
    {
        Id = id;
        EmailAddress = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        FullName = $"{firstName} {lastName}".Trim();
        DateOfBirth = dateOfBirth;
        IsAdmin = isAdmin;
        FreePopcornGiftUsedYear = freePopcornGiftUsedYear;
        PassPoints = passPoints;
    }

    public AccountModel(
        Int64 id,
        string email,
        string password,
        string fullName,
        string firstName,
        string lastName,
        Int64 dateOfBirth,
        Int64 isAdmin = 0,
        Int64 freePopcornGiftUsedYear = 0,
        Int64 passPoints = 0
    ) : this(id, email, password, firstName, lastName, dateOfBirth, isAdmin, freePopcornGiftUsedYear, passPoints)
    {
        FullName = fullName;
    }
}
