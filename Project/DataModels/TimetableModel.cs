using System.ComponentModel.DataAnnotations.Schema;

public class TimetableModel
{
    public Int64 Id {get; set;}
    public Int64 MovieId {get; set;}
    public Int64 RoomId {get; set;}
    public Int64 StartTime {get; set;}
    public bool IsActive {get; set;}

    public TimetableModel(Int64 id, Int64 movieId, Int64 roomId, Int64 startTime, Int64 isActive)
    {
        Id = id;
        MovieId = movieId;
        RoomId = roomId;
        StartTime = startTime;
        IsActive = isActive != 0;
    }

    public TimetableModel(Int64 id, Int64 movieId, Int64 roomId, Int64 startTime)
        : this(id, movieId, roomId, startTime, 1)
    {
    }
}