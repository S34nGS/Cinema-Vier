public abstract class BaseComponent
{
    protected readonly ContinueAndBackMenu continueAndBackMenu = new();

    protected static int GetLongestString(IEnumerable<string> strings)
    {
        return strings.Max(x => x.Length);
    }

    protected static void WriteHeader(string? header)
    {
        if (!string.IsNullOrEmpty(header))
        {
            Console.WriteLine(header);
        }
    }

    protected static string ShowInput(string input, string title)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        else if (title.Contains("password", StringComparison.OrdinalIgnoreCase))
        {
            return new string('*', input.Length);
        }
        else
        {
            return input;
        }
    }
}
