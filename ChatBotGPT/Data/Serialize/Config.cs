namespace VideoBot;

[Serializable]
public class Config
{
    //Parameters
    public string Token;
    public int AwaitNewVideoDays;
    public DateTimeOffset SwitchTime; //2023-05-06T13:46:37.2358632+05:00
    
    //System
    public string FirstPhotoName;
    public string SecondPhotoName;
    
    //Id,s
    public long AdminId;
    public long ChanelId;

    public bool IsSwitchedToNewFormat;
}