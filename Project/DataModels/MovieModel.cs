public class MovieModel
{
    public Int64 Id {get; set;}
    public string Title {get; set;}
    public Int64 Duration {get; set;}
    public string Summary {get; set;}
    public string Director {get; set;}
    public Int64 AgeRating {get; set;}
    public string Genre {get; set;}
    public Int64 ReleaseDate {get; set;}

    public MovieModel(
        Int64 id,
        string title,
        Int64 duration,
        string summary,
        string director,
        Int64 ageRating,
        string genre,
        Int64 releaseDate
    )
    {
        Id = id;
        Title = title;
        Duration = duration;
        Summary = summary;
        Director = director;
        AgeRating = ageRating;
        Genre = genre;
        ReleaseDate = releaseDate;
    }

    public override string ToString()
    {
        string dates = "";
        foreach(string date in PurchaseTicket.DateMenu)
        {
            dates += $"{date}{Environment.NewLine}";
        }

        return $"Title: {Title}{Environment.NewLine}" +
            $"Description: {Summary}{Environment.NewLine}" +
            $"Genre: {Genre}{Environment.NewLine}" +
            $"Duration: {Duration}{Environment.NewLine}" +
            $"Age Rating: {AgeRating}{Environment.NewLine}" +
            $"Available dates: {dates}";
    }
}