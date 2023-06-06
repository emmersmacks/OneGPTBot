namespace VideoBot.Data.SSH;

public class TunnelConfig
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int LocalPort { get; set; }
    public string RemoteHost { get; set; }
    public int RemotePort { get; set; }
}