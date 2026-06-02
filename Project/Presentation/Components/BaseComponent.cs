public abstract class BaseComponent
{
    protected readonly ContinueAndBackMenu continueAndBackMenu = new();

    protected static int GetLongestString(IEnumerable<string> strings)
    {
        return strings.Max(x => x.Length);
    }

    protected void WriteHeader(string? header)
    {
        if (!string.IsNullOrEmpty(header))
        {
            Console.WriteLine(header);
        }
    }

    protected string ShowInput(string input, string title)
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

    public bool IsLeftKey(ConsoleKey key, bool includeH = true)
    {
        return key == ConsoleKey.LeftArrow || (includeH && key == ConsoleKey.H);
    }

    public bool IsRightKey(ConsoleKey key, bool includeL = true)
    {
        return key == ConsoleKey.RightArrow || (includeL && key == ConsoleKey.L);
    }

    public bool IsUpKey(ConsoleKey key, bool includeK = true)
    {
        return key == ConsoleKey.UpArrow || (includeK && key == ConsoleKey.K);
    }

    public bool IsDownKey(ConsoleKey key, bool includeJ = true)
    {
        return key == ConsoleKey.DownArrow || (includeJ && key == ConsoleKey.J);
    }
}
