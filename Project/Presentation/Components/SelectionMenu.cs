public class SelectionMenu : BaseComponent
{
    public int WriteMenu(
        IEnumerable<string> menu,
        string? header = null,
        bool hasButtons = false
    )
    {
        string[] localMenu = menu.ToArray();
        int selected = 0;

        bool continueSelected = true;

        while (true)
        {
            Console.Clear();
            WriteHeader(header);

            Console.WriteLine(GetMenu(localMenu, selected));

            if (!hasButtons)
            {
                continueAndBackMenu.WriteMenu(continueSelected);
            }

            ConsoleKey key = Console.ReadKey().Key;

            if (!hasButtons)
            {
                if (IsLeftKey(key) && continueSelected)
                {
                    continueSelected = false;
                }
                else if (IsRightKey(key) && !continueSelected)
                {
                    continueSelected = true;
                }
            }

            if (key == ConsoleKey.Enter)
            {
                if (continueSelected)
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

    public string GetMenu(string[] menu, int selected)
    {
        int longest = GetLongestString(menu);

        string output = $"╔{new string('═', longest + 6)}╗" + Environment.NewLine;

        for (int index = 0; index < menu.Length; index++)
        {
            if (index == selected)
            {
                output += $"║ > {menu[index]} {new string(' ', longest - menu[index].Length)}< ║";
            }
            else
            {
                output += $"║   {menu[index]} {new string(' ', longest - menu[index].Length)}  ║";
            }
            output += Environment.NewLine;
        }

        output += $"╚{new string('═', longest + 6)}╝";

        return output;
    }
}
