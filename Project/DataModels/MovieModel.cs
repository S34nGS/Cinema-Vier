public class MovieModel
{
    public Int64 Id {get; set;}
    public string Name {get; set;}
    public string Summary {get; set;}
    public string Director {get; set;}
    public List<string> Actors {get; set;}
    public Int64 AgeRating {get; set;}

    public MovieModel(
        Int64 id,
        string name,
        string summary,
        string director,
        List<string> actors,
        Int64 ageRating
    )
    {
        Id = id;
        Name = name;
        Summary = summary;
        Director = director;
        Actors = actors;
        AgeRating = ageRating;
    }
}