using Renci.SshNet;

namespace VideoBot.Data.SSH;

public class SshTunnel : IDisposable
{

    private readonly SshClient _sshClient;

  public SshTunnel(TunnelConfig config)
  {
      _sshClient = new SshClient(config.Host, config.Port, config.Username, config.Password);
  }

  public void Start()
  {
      _sshClient.Connect();
  }

  public void Stop()
  {
      _sshClient.Disconnect();
  }

  public void Dispose()
  {
      Stop();
      _sshClient.Dispose();
  }
}