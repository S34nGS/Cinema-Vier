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
}
