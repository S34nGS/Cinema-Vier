public class RoomModel
{
    public Int64 Id {get; set;}
    public string ScreenType {get; set;}
    public string SoundType {get; set;}

    public RoomModel(Int64 id, string screenType, string soundType)
    {
        Id = id;
        ScreenType = screenType;
        SoundType = soundType;
    }
}