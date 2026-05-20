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
}
