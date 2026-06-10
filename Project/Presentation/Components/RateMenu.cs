public class RateMenu : BaseComponent
{
    public int WriteMenu(string header, int existingRating = -1)
    {
        int rating = (existingRating >= 0) ? existingRating : 0;
        
        char[] stars = ['\u2606','\u2606','\u2606','\u2606','\u2606'];
        int length = 10;

        for(int i = 0; i < rating; i++)
        {
            stars[i] = '\u2B50';
        }

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