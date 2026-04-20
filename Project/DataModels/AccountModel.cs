public class AccountModel
{

    public Int64 Id { get; set; }
    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public AccountModel(int id, string email, string password, string firstName, string lastName)
    {
        Id = id;
        EmailAddress = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
    }
}



