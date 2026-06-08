// TODO: split input & output
public class InputMenu : BaseComponent
{
    public string WriteMenu(
        string? header = null,
        int defaultLength = 24,
        bool grows = false
    )
    {
        string input = "";
        bool continueSelected = true;
        int length = defaultLength;
        while (true)
        {
            Console.Clear();
            WriteHeader(header);

            string shown = input + new string(' ', length - input.Length);

            Console.WriteLine($"╔{new string('═', length + 2)}╗");
            Console.WriteLine($"║ {shown} ║");
            Console.WriteLine($"╚{new string('═', length + 2)}╝");

            continueAndBackMenu.WriteMenu(continueSelected);

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (IsLeftKey(keyInfo.Key, false) && continueSelected)
            {
                continueSelected = false;
            }
            else if (IsRightKey(keyInfo.Key, false) && !continueSelected)
            {
                continueSelected = true;
            }

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                if (!continueSelected) return "-1";
                if (input.Length > 0) return input;
            }

            if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input = input[..^1];
                if (length > defaultLength)
                {
                    length--;
                }
            }

            char character = keyInfo.KeyChar;
            if (!char.IsControl(character) && (input.Length < defaultLength || grows))
            {
                input += character;
                if (input.Length > defaultLength)
                {
                    length++;
                }
            }
        }
    }

    public int RateMenu(string header)
    {
        int rating = 0;
        char[] stars = ['\u2606','\u2606','\u2606','\u2606','\u2606'];
        int length = 10;

        while (true)
        {
            Console.Clear();
            WriteHeader(header);

            string shown = new string(stars) + new string(' ', length - stars.Length);

            Console.WriteLine($"{shown}");

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (IsLeftKey(keyInfo.Key, false))
            {
                if (rating > 0)
                {
                    rating--;
                    stars[rating] = '\u2606';
                }
            }
            else if (IsRightKey(keyInfo.Key, false))
            {
                if (rating < stars.Length)
                {
                    stars[rating] = '\u2B50';
                    rating++;
                }
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                break;
            }
        }

        return rating;
    }
}
