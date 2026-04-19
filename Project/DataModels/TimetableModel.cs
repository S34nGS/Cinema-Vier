using System.ComponentModel.DataAnnotations.Schema;

public class TimetableModel
{
    public Int64 Id {get; set;}
    [ForeignKey("Movies")]
    public Int64 MovieId {get; set;}
    [ForeignKey("Rooms")]
    public Int64 RoomId {get; set;}
    public Int64 StartTime {get; set;}

    public TimetableModel(Int64 id, Int64 movieId, Int64 roomId, Int64 startTime)
    {
        Id = id;
        MovieId = movieId;
        RoomId = roomId;
        StartTime = startTime;
    }
}