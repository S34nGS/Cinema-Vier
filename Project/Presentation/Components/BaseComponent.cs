public abstract class BaseComponent
{
    public static int GetLongestString(IEnumerable<string> strings)
    {
        return strings.Max(x => x.Length);
    }
}
