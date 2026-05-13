public static class UiHelper
{
    public static int GetLongestString(IEnumerable<string> strings)
    {
        int longest = 0;
        foreach (string item in strings)
        {
            if (item.Length > longest)
            {
                longest = item.Length;
            }
        }

        return longest;
    }

    private static void WriteHeader(string? header)
    {
        if (!string.IsNullOrEmpty(header))
        {
            Console.WriteLine(header);
        }
    }

    // TODO: Rewrite using boolean instead of int
    public static void ContinueOrBackMenu(int continueOrBack)
    {
        Console.WriteLine($"╔{new string('═', 22)}╗");

        for (int index = 0; index < 1; index++)
        {
            if (index == continueOrBack)
            {
                Console.WriteLine($"║ > Back <  Continue   ║");
            }
            else
            {
                Console.WriteLine($"║   Back  > Continue < ║");
            }
        }

        Console.WriteLine($"╚{new string('═', 22)}╝");
    }

    public static int SelectionMenu(IEnumerable<string> menu, string? header = null, bool hasButtons = false)
    {
        string[] localMenu = menu.ToArray();
        int longest = GetLongestString(localMenu);
        int selected = 0;
        int continueOrBack = 1;

        while (true)
        {
            Console.Clear();
            WriteHeader(header);

            Console.WriteLine($"╔{new string('═', longest + 6)}╗");
            for (int index = 0; index < localMenu.Length; index++)
            {
                if (index == selected)
                {
                    Console.WriteLine($"║ > {localMenu[index]} {new string(' ', longest - localMenu[index].Length)}< ║");
                }
                else
                {
                    Console.WriteLine($"║   {localMenu[index]} {new string(' ', longest - localMenu[index].Length)}  ║");
                }
            }

            Console.WriteLine($"╚{new string('═', longest + 6)}╝");

            if (!hasButtons)
            {
                ContinueOrBackMenu(continueOrBack);
            }

            ConsoleKey key = Console.ReadKey().Key;

            if (!hasButtons)
            {
                if (IsLeftKey(key) && continueOrBack > 0)
                {
                    continueOrBack--;
                }
                else if (IsRightKey(key) && continueOrBack < 1)
                {
                    continueOrBack++;
                }
            }

            if (key == ConsoleKey.Enter)
            {
                if (continueOrBack == 1)
                {
                    break;
                }

                return -1;
            }

            if (IsDownKey(key) && selected < localMenu.Length - 1)
            {
                selected++;
            }
            else if (IsUpKey(key) && selected > 0)
            {
                selected--;
            }
        }

        return selected;
    }

    public static string Input(string? header = null, int maxLength = 24)
    {
        string name = "";
        int continueOrBack = 1;

        while (true)
        {
            Console.Clear();
            WriteHeader(header);

            int fieldWidth = 20;
            string shown = name.Length > fieldWidth ? name[..fieldWidth] : name;

            Console.WriteLine($"╔{new string('═', 22)}╗");
            Console.WriteLine($"║ {shown.PadRight(fieldWidth)} ║");
            Console.WriteLine($"╚{new string('═', 22)}╝");

            ContinueOrBackMenu(continueOrBack);

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (IsLeftKey(keyInfo.Key, false) && continueOrBack > 0)
            {
                continueOrBack--;
            }
            else if (IsRightKey(keyInfo.Key, false) && continueOrBack < 1)
            {
                continueOrBack++;
            }

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                if (continueOrBack == 0) return "-1";
                if (name.Length > 0) return name;
            }

            if (keyInfo.Key == ConsoleKey.Backspace && name.Length > 0)
            {
                name = name[..^1];
            }

            char character = keyInfo.KeyChar;
            if (!char.IsControl(character) && name.Length < maxLength)
            {
                name += character;
            }
        }
    }

    public static void HoldUser(string message = "Press any key to continue...")
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }

    public static string ShowInput(string input, string title)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        else if (title.ToLower().Contains("password"))
        {
            return new string('*', input.Length);
        }
        else
        {
            return input;
        }
    }

    public static Dictionary<string, string> InputForm(IEnumerable<string> titles, string formTitle = "Input Form",
        int maxLength = 32)
    {
        Dictionary<string, string> inputs = new();

        foreach (string title in titles)
        {
            inputs[title] = "";
        }

        return InputForm(inputs, formTitle, maxLength);
    }

    public static Dictionary<string, string> InputForm(Dictionary<string, string> fields,
        string formTitle = "Input Form", int maxLength = 32, string? header = null)
    {
        int longest = Math.Max(
            Math.Max(GetLongestString(fields.Keys.ToList()), maxLength),
            formTitle.Length
        );
        int selected = 0;

        Dictionary<string, string> inputs = fields;

        while (true)
        {
            Console.Clear();
            WriteHeader(header);
            Console.WriteLine("╔═ " + formTitle + $" {new string('═', longest - formTitle.Length)}╗");
            string currentField = fields.Keys.ToList()[selected];
            foreach (KeyValuePair<string, string> title in fields)
            {
                Console.WriteLine($"╠═ {title.Key} {new string('═', longest - title.Key.Length)}╣");
                if (title.Key == currentField)
                {
                    Console.WriteLine(
                        $"║>{ShowInput(inputs[title.Key], title.Key)} {new string(' ', longest - ShowInput(inputs[title.Key], title.Key).Length)}<║");
                }
                else
                {
                    Console.WriteLine(
                        $"║ {ShowInput(inputs[title.Key], title.Key)} {new string(' ', longest - ShowInput(inputs[title.Key], title.Key).Length)} ║");
                }
            }

            Console.WriteLine($"╚{new string('═', longest + 3)}╝");
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.Enter)
            {
                break;
            }
            else if (IsDownKey(key.Key, false) && selected < fields.Count - 1)
            {
                selected++;
            }
            else if (IsUpKey(key.Key, false) && selected > 0)
            {
                selected--;
            }
            else if (key.Key == ConsoleKey.Backspace && inputs[currentField].Length > 0)
            {
                inputs[currentField] = inputs[currentField][..^1];
            }
            else
            {
                char character = key.KeyChar;
                if (!char.IsControl(character) && inputs[currentField].Length < maxLength)
                {
                    inputs[currentField] += character;
                }
            }
        }

        return inputs;
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