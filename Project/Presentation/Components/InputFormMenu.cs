// TODO: Seperate input & output
public class InputFormMenu : BaseComponent
{
    public Dictionary<string, string> WriteMenu(
        IEnumerable<string> titles,
        string formTitle = "Input Form",
        int maxLength = 32
    )
    {
        Dictionary<string, string> inputs = [];

        foreach (string title in titles)
        {
            inputs[title] = "";
        }

        return WriteMenu(inputs, formTitle, maxLength);
    }

    public Dictionary<string, string> WriteMenu(
        Dictionary<string, string> fields,
        string formTitle = "Input Form",
        int maxLength = 32,
        string? header = null
    )
    {
        int longest = Math.Max(
            Math.Max(GetLongestString(fields.Keys.ToList()), maxLength),
            formTitle.Length
        );
        int selected = 0;
        int totalOptions = fields.Count + 1;

        Dictionary<string, string> inputs = fields;

        while (true)
        {
            Console.Clear();
            WriteHeader(header);
            Console.WriteLine("╔═ " + formTitle + $" {new string('═', longest - formTitle.Length)}╗");
            string? currentField = null;
            if (selected < fields.Count)
            {
                currentField = fields.Keys.ToList()[selected];
            }

            foreach (KeyValuePair<string, string> title in fields)
            {
                Console.WriteLine($"╠═ {title.Key} {new string('═', longest - title.Key.Length)}╣");

                string displayText = ShowInput(inputs[title.Key], title.Key);
                List<string> lines = WrapText(displayText, longest);

                bool isSelected = title.Key == currentField;


                for (int i = 0; i < lines.Count; i++)
                {
                    string line = lines[i];
                    bool isLastLine = i == lines.Count - 1;

                    if (isSelected && isLastLine)
                    {
                        Console.WriteLine($"║>{line}{new string(' ', longest - line.Length + 1)}<║");
                    }
                    else if (isSelected)
                    {
                        Console.WriteLine($"║ {line}{new string(' ', longest - line.Length + 1)} ║");
                    }
                    else
                    {
                        Console.WriteLine($"║ {line}{new string(' ', longest - line.Length + 1)} ║");
                    }
                }
            }

            Console.WriteLine($"╚{new string('═', longest + 3)}╝");
            bool backSelected = selected == fields.Count;

            Console.WriteLine("╔════════╗");
            if (backSelected)
            {
                Console.WriteLine("║> Back <║");
            }
            else
            {
                Console.WriteLine("║  Back  ║");
            }
            Console.WriteLine("╚════════╝");

            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.Enter)
            {
                if (selected == fields.Count)
                {
                    return [];
                }
                break;
            }
            else if (IsDownKey(key.Key, false) && selected < totalOptions - 1)
            {
                selected++;
            }
            else if (IsUpKey(key.Key, false) && selected > 0)
            {
                selected--;
            }
            else if (currentField != null && key.Key == ConsoleKey.Backspace && inputs[currentField].Length > 0)
            {
                inputs[currentField] = inputs[currentField][..^1];
            }
            else if (currentField != null)
            {
                char character = key.KeyChar;
                if (!char.IsControl(character))
                {
                    inputs[currentField] += character;
                }
            }
        }

        return inputs;
    }

    private static List<string> WrapText(string text, int width)
    {
        List<string> lines = [];
        if (string.IsNullOrWhiteSpace(text))
        {
            lines.Add("");
            return lines;
        }

        string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string currentLine = "";

        foreach (string word in words)
        {
            if (string.IsNullOrEmpty(currentLine))
            {
                currentLine = word;
            }
            else if (currentLine.Length + 1 + word.Length <= width)
            {
                currentLine += " " + word;
            }
            else
            {
                lines.Add(currentLine);
                currentLine = word;
            }
        }

        if (!string.IsNullOrEmpty(currentLine))
        {
            lines.Add(currentLine);
        }

        return lines;
    }
}