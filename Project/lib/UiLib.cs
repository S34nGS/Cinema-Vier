public static class UiLib
{
    public static void Main()
    { }

    public static int GetLongestString(List<string> strings)
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

    public static int SelectionMenu(List<string> menu, string? header = null)
    {
        int longest = GetLongestString(menu);
        int selected = 0;

        while (true)
        {
            Console.Clear();
            WriteHeader(header);

            Console.WriteLine($"╔{new string('═', longest + 6)}╗");
            for (int index = 0; index < menu.Count; index++)
            {
                if (index == selected)
                {
                    Console.WriteLine($"║ > {menu[index]} {new string(' ', longest - menu[index].Length)}< ║");
                }
                else
                {
                    Console.WriteLine($"║   {menu[index]} {new string(' ', longest - menu[index].Length)}  ║");
                }
            }
            Console.WriteLine($"╚{new string('═', longest + 6)}╝");

            ConsoleKey key = Console.ReadKey().Key;

            if (key == ConsoleKey.Enter)
            {
                break;
            }

            if ((key == ConsoleKey.DownArrow || key == ConsoleKey.J) && selected < menu.Count - 1)
            {
                selected++;
            }
            else if ((key == ConsoleKey.UpArrow || key == ConsoleKey.K) && selected > 0)
            {
                selected--;
            }
        }

        return selected;
    }

    public static string Input(string prompt, string? header = null, int maxLength = 24)
    {
        string name = "";

        while (true)
        {
            Console.Clear();
            WriteHeader(header);

            Console.WriteLine(prompt + name);
            Console.WriteLine($"{Environment.NewLine}Enter = confirm, Backspace = delete");

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Enter && name.Length > 0)
            {
                break;
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

        return name;
    }
    public static void HoldUser(string message = "Press any key to continue...")
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }

    public static Dictionary<string, string> InputForm(List<string> titles, string formTitle = "Input Form", int maxLength = 32)
    {
        int longest = Math.Max(
            Math.Max(GetLongestString(titles), maxLength),
            formTitle.Length
        );
        int selected = 0;

        Dictionary<string, string> inputs = new();

        foreach (string title in titles)
        {
            inputs[title] = "";
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔═ " + formTitle + $" {new string('═', longest - formTitle.Length)}╗");
            for (int i = 0; i < titles.Count; i++)
            {
                string title = titles[i];
                Console.WriteLine($"╠═ {title} {new string('═', longest - title.Length)}╣");
                if (i == selected)
                {
                    Console.WriteLine($"║>{inputs[title]} {new string(' ', longest - inputs[title].Length)}<║");
                }
                else
                {
                    Console.WriteLine($"║ {inputs[title]} {new string(' ', longest - inputs[title].Length)} ║");
                }
            }
            Console.WriteLine($"╚{new string('═', longest + 3)}╝");
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.Enter)
            {
                break;
            }
            else if (key.Key == ConsoleKey.DownArrow && selected < titles.Count - 1)
            {
                selected++;
            }
            else if (key.Key == ConsoleKey.UpArrow && selected > 0)
            {
                selected--;
            }
            else if (key.Key == ConsoleKey.Backspace && inputs[titles[selected]].Length > 0)
            {
                inputs[titles[selected]] = inputs[titles[selected]][..^1];
            }
            else
            {
                char character = key.KeyChar;
                if (!char.IsControl(character) && inputs[titles[selected]].Length < maxLength)
                {
                    inputs[titles[selected]] += character;
                }
            }
        }
        return inputs;
    }
}