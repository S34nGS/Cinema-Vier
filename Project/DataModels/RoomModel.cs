public class RoomModel
{
    public Int64 Id {get; set;}
    public string ScreenType {get; set;}
    public string SoundType {get; set;}
    public Int64 Height {get; set;}
    public Int64 Width {get; set;}

    public RoomModel(Int64 id, string screenType, string soundType, Int64 height, Int64 width)
    {
        Id = id;
        ScreenType = screenType;
        SoundType = soundType;
        Height = height;
        Width = width;
    }
}