public class RatingModel : IModel
{
    public Int64 Id {get; set;}
    public Int64 UserId {get; set;}
    public Int64 MovieId {get; set;}
    public double Rating {get; set;}

    public RatingModel(Int64 id, Int64 userId, Int64 movieId, double rating)
    {
        Id = id;
        UserId = userId;
        MovieId = movieId;
        Rating = rating;
    }
}