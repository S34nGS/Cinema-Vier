using System.Runtime.InteropServices;

public static class UiLib
{
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
                    Console.WriteLine($"║>{ShowInput(inputs[title], title)} {new string(' ', longest - ShowInput(inputs[title], title).Length)}<║");
                }
                else
                {
                    Console.WriteLine($"║ {ShowInput(inputs[title], title)} {new string(' ', longest - ShowInput(inputs[title], title).Length)} ║");
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
    public static void ShowTable(Dictionary<string, string> table, string title)
    {
        int longestKey = GetLongestString(new List<string>(table.Keys));
        int longestValue = GetLongestString(new List<string>(table.Values));
        int innerWidth = longestKey + longestValue + 3;

        Console.WriteLine($"╔═ {title} {new string('═', Math.Max(innerWidth - title.Length - 1, 0))}╗");
        var entries = table.ToList();
        for (int i = 0; i < entries.Count; i++)
        {
            string displayValue = ShowInput(entries[i].Value, entries[i].Key);
            Console.WriteLine($"║ {entries[i].Key}{new string(' ', longestKey - entries[i].Key.Length)} ║ {displayValue}{new string(' ', longestValue - displayValue.Length)} ║");
            if (i < entries.Count - 1)
            {
                Console.WriteLine($"╠{new string('═', longestKey + 2)}╩{new string('═', longestValue + 2)}╣");
            }
        }
        Console.WriteLine($"╚{new string('═', longestKey + 2)}╩{new string('═', longestValue + 2)}╝");
    }
}
