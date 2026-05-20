public static class UiHelper
{
    public static SelectionMenu SelectionMenu { get; private set; }
    public static InputMenu InputMenu { get; private set; }
    public static InputFormMenu InputFormMenu { get; private set; }

    static UiHelper()
    {
        SelectionMenu = new();
        InputMenu = new();
        InputFormMenu = new();
    }

    public static Action GeneratePage(string content)
    {
        return () =>
        {
            Console.Clear();

            Console.WriteLine(content);

            HoldUser();
        };
    }

    public static void HoldUser(string message = "Press any key to continue...")
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }

    public static bool IsLeftKey(ConsoleKey key, bool includeH = true)
    {
        return key == ConsoleKey.LeftArrow || (includeH && key == ConsoleKey.H);
    }

    public static bool IsRightKey(ConsoleKey key, bool includeL = true)
    {
        return key == ConsoleKey.RightArrow || (includeL && key == ConsoleKey.L);
    }

    public static bool IsUpKey(ConsoleKey key, bool includeK = true)
    {
        return key == ConsoleKey.UpArrow || (includeK && key == ConsoleKey.K);
    }

    public static bool IsDownKey(ConsoleKey key, bool includeJ = true)
    {
        return key == ConsoleKey.DownArrow || (includeJ && key == ConsoleKey.J);
    }
}
