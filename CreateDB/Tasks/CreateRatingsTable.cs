public static class CreateRatingsTable
{
    public static Task Execute()
    {
        RatingsAccess ratings = new();
        ratings.CreateTable();

        return Task.CompletedTask;
    }
}