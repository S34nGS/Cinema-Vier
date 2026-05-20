public abstract class BaseComponent
{
    public readonly ContinueAndBackMenu continueAndBackMenu = new();

    public static int GetLongestString(IEnumerable<string> strings)
    {
        return strings.Max(x => x.Length);
    }

    private static void WriteHeader(string? header)
    {
        if (!string.IsNullOrEmpty(header))
        {
            Console.WriteLine(header);
        }
    }

    public static string ShowInput(string input, string title)
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
