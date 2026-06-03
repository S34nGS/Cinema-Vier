using System.Text.RegularExpressions;

public static class ValidDataLogic
{

    public static bool IsValidEmail(string email)
    {
        Match match = Regex.Match(email, @"([^ ])+@([A-Z])+\.([A-Z])+", RegexOptions.IgnoreCase);
        return match.Success;
    }


    public static bool IsValidPassword(string password)
    {
        if (String.IsNullOrEmpty(password))
        {
            return false;
        }

        if (password.Length < 6)
        {
            return false;
        }

        return true;
    }


    public static bool IsValidDateOfBirth(DateTime dateOfBirth)
    {
        return dateOfBirth < DateTime.Today && dateOfBirth > DateTime.Today.AddYears(-120);
    }

    public static bool IsValidFirstName(string name)
    {
        return !string.IsNullOrEmpty(name) && name[0] == char.ToUpper(name[0]);
    }
}
