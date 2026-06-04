public class ContinueAndBackMenu
{
    public void WriteMenu(bool continueSelected = true)
    {
        Console.WriteLine(GetMenu(continueSelected));
    }

    public string GetMenu(bool continueSelected)
    {
        string output = $"╔{new string('═', 22)}╗" + Environment.NewLine;
        if (continueSelected)
        {
            output += "║   Back  > Continue < ║" + Environment.NewLine;
        }
        else
        {
            output += "║ > Back <  Continue   ║" + Environment.NewLine;
        }

        return output += $"╚{new string('═', 22)}╝";
    }
}
